using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using Npgsql;

namespace AskDB_Desktop
{
    public partial class Form1 : MetroForm
    {
        // State fields
        private int? _userId;
        private string _connectionString;
        private string _dbType;
        private string _mode;
        private HashSet<string> _enabledTables = new HashSet<string>();

        // Add these properties to Form1 class
        private bool _runSqlAutomatically = false;
        private bool _includeSampleData = false;

        // Property accessors
        public bool RunSqlAutomatically 
        {
            get => _runSqlAutomatically;
            set => _runSqlAutomatically = value;
        }

        public bool IncludeSampleData
        {
            get => _includeSampleData;
            set => _includeSampleData = value;
        }

        // Use the value from the login tab's textbox for the user DB connection string
        private string UserDbConnectionString
        {
            get
            {
                return txtUserDbConnStr?.Text ?? "Server=localhost\\SQLEXPRESS;Database=AskDBinfo;Trusted_Connection=True;";
            }
        }

        private static Dictionary<string, string> TableMappings = new Dictionary<string, string>();
        private static Dictionary<string, Dictionary<string, string>> ColumnMappings = new Dictionary<string, Dictionary<string, string>>();

        public Form1()
        {
            InitializeComponent();

            // Set up ComboBox items and defaults
            if (ddlDbType.Items.Count == 0)
                ddlDbType.Items.AddRange(new object[] { "SQL Server", "MySQL", "PostgreSQL" });
            if (ddlMode.Items.Count == 0)
                ddlMode.Items.AddRange(new object[] { "read", "write" });  // Changed from "read only" and "read & write"

            // Set defaults and prevent empty selection
            ddlDbType.SelectedItem = "SQL Server";
            ddlMode.SelectedItem = "read";
            ddlDbType.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlMode.DropDownStyle = ComboBoxStyle.DropDownList;

            // Start on login tab, disable all others
            tabControl1.SelectedIndex = 0;
            for (int i = 1; i < tabControl1.TabCount; i++)
                tabControl1.TabPages[i].Enabled = false;

            tabControl1.Selecting += (s, e) =>
            {
                if (!tabControl1.TabPages[e.TabPageIndex].Enabled)
                    e.Cancel = true;
            };

            dataGridViewTables.CellContentClick += dataGridViewTables_CellContentClick;

            // Font and background
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.White;
            foreach (TabPage tab in tabControl1.TabPages)
                tab.BackColor = System.Drawing.Color.White;

            // Style DataGridViews
            void StyleGrid(DataGridView grid)
            {
                grid.EnableHeadersVisualStyles = false;
                grid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                grid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                grid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                grid.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                grid.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                grid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
                grid.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                grid.RowHeadersVisible = false;
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                grid.ScrollBars = ScrollBars.Both;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.MultiSelect = false;
            }
            StyleGrid(dataGridViewTables);
            StyleGrid(dataGridViewColumns);
            StyleGrid(dataGridViewResults);

            // Style buttons
            Action<Control.ControlCollection> styleButtons = null;
            styleButtons = (controls) =>
            {
                foreach (Control c in controls)
                {
                    if (c is Button btn)
                    {
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
                        btn.ForeColor = System.Drawing.Color.White;
                        btn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                        btn.FlatAppearance.BorderSize = 0;
                    }
                    else if (c.HasChildren)
                    {
                        styleButtons(c.Controls);
                    }
                }
            };
            styleButtons(this.Controls);

            // Setup query tab layout
            SetupQueryTabLayout();

            btnSettings.Enabled = false;
        }

        private System.Windows.Forms.TabPage tabQueryOptimizer;
        private System.Windows.Forms.TextBox txtOptimizerInput;
        private System.Windows.Forms.Button btnOptimizeQuery;
        private System.Windows.Forms.TextBox txtOptimizerResult;

        private void SetupQueryTabLayout()
        {
            // Clear existing controls to start clean
            tabQuery.Controls.Clear();

            // Create a proper container layout
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 1;
            mainLayout.RowCount = 3;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // Top controls row
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // Chat history
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // Input row

            // Create top controls panel (buttons, settings)
            Panel topPanel = new Panel();
            topPanel.Dock = DockStyle.Fill;
            topPanel.Height = 40;

            // Add Clear Chat button
            Button btnClearChat = new Button();
            btnClearChat.Text = "Clear Chat";
            btnClearChat.Size = new Size(100, 30);
            btnClearChat.Location = new Point(topPanel.Width - 110, 5);
            btnClearChat.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnClearChat.Click += (s, e) => chatHistoryPanel.Controls.Clear();
            topPanel.Controls.Add(btnClearChat);

            // Add Help With Query button
            Button btnQueryHelp = new Button();
            btnQueryHelp.Text = "Help With Query";
            btnQueryHelp.Size = new Size(140, 30);
            btnQueryHelp.Location = new Point(topPanel.Width - 260, 5); // Position it to the left of the Clear Chat button
            btnQueryHelp.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnQueryHelp.Click += btnQueryHelp_Click;
            topPanel.Controls.Add(btnQueryHelp);

            // Re-initialize chat panel with proper settings
            //chatHistoryPanel = new FlowLayoutPanel();
            chatHistoryPanel.Dock = DockStyle.Fill;
            chatHistoryPanel.AutoScroll = true;
            chatHistoryPanel.FlowDirection = FlowDirection.TopDown;
            chatHistoryPanel.WrapContents = false;
            chatHistoryPanel.Padding = new Padding(10);
            chatHistoryPanel.BackColor = Color.FromArgb(248, 248, 250);
            chatHistoryPanel.Width = tabQuery.Width; // Set explicit width

            // Set these properties to ensure proper layout
            chatHistoryPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            chatHistoryPanel.VerticalScroll.Visible = true;
            chatHistoryPanel.HorizontalScroll.Visible = false;
            chatHistoryPanel.HorizontalScroll.Enabled = false;

            // Create input panel
            Panel inputPanel = new Panel();
            inputPanel.Dock = DockStyle.Fill;
            inputPanel.Padding = new Padding(10, 5, 10, 10);

            // Configure text input
            txtUserInput.Dock = DockStyle.Fill;
            txtUserInput.Margin = new Padding(0, 0, 110, 0); // Leave space for button

            // Configure submit button
            btnSubmit.Dock = DockStyle.Right;
            btnSubmit.Width = 100;

            // Put controls in correct order (button last so it's on top)
            inputPanel.Controls.Add(txtUserInput);
            inputPanel.Controls.Add(btnSubmit);

            // Add all containers to main layout
            mainLayout.Controls.Add(topPanel, 0, 0);
            mainLayout.Controls.Add(chatHistoryPanel, 0, 1);
            mainLayout.Controls.Add(inputPanel, 0, 2);

            // Add the layout to the tab
            tabQuery.Controls.Add(mainLayout);

            // Add a test message to verify the chat is working
            var testMsg = new ChatMessage("Chat system initialized successfully", ChatMessage.MessageType.System);
            chatHistoryPanel.Controls.Add(testMsg);

            // Force layout update
            mainLayout.PerformLayout();

            // Update all ChatMessage controls to fill the width
            foreach (Control ctrl in chatHistoryPanel.Controls)
            {
                if (ctrl is ChatMessage)
                {
                    ctrl.Width = chatHistoryPanel.ClientSize.Width - 25;
                }
            }

            // Add event handlers for resizing
            tabQuery.Resize += (s, e) => {
                foreach (Control ctrl in chatHistoryPanel.Controls)
                {
                    if (ctrl is ChatMessage)
                    {
                        ctrl.Width = chatHistoryPanel.ClientSize.Width - 25;
                    }
                }
            };

            // Add Enter key handling for text input
            txtUserInput.KeyDown += txtUserInput_KeyDown;
        }

        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true; // Prevents the annoying "ding" sound
                btnSubmit.PerformClick(); // Trigger the submit button click
            }
        }

        // Add this method to create appropriate connection based on database type
        private DbConnection CreateConnection(string connectionString)
        {
            switch (_dbType?.ToLower())
            {
                case "mysql":
                    return new MySqlConnection(connectionString);
                case "postgresql":
                    return new NpgsqlConnection(connectionString);
                default:
                    return new SqlConnection(connectionString);
            }
        }
        private void ScrollToBottomOfChat()
        {
            if (chatHistoryPanel.Controls.Count > 0)
            {
                chatHistoryPanel.SuspendLayout();
                
                // This is the key fix - force the panel to process layout before scrolling
                chatHistoryPanel.PerformLayout();
                Application.DoEvents();
                
                // Get the last control and ensure it's visible
                Control lastControl = chatHistoryPanel.Controls[chatHistoryPanel.Controls.Count - 1];
                chatHistoryPanel.ScrollControlIntoView(lastControl);
                
                // Additional technique to ensure scrolling works
                chatHistoryPanel.AutoScrollPosition = new Point(0, chatHistoryPanel.DisplayRectangle.Height);
                
                chatHistoryPanel.ResumeLayout();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            using (var conn = new SqlConnection(UserDbConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    @"SELECT u.userid, r.RoleName 
              FROM Users u 
              LEFT JOIN Roles r ON u.RoleId = r.RoleId 
              WHERE u.username = @username AND u.password = @password", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _userId = reader.GetInt32(0);
                            string roleName = reader.IsDBNull(1) ? "User" : reader.GetString(1);
                            LoadUserMappings(_userId.Value);
                            tabControl1.TabPages[1].Enabled = true;
                            tabControl1.SelectedIndex = 1;
                            lblLoginError.Text = "";
                            lblCurrentUser.Text = $"Logged in as: {username} ({roleName})";
                            LoadUserSettings(_userId.Value);
                            btnSettings.Enabled = true;
                        }
                        else
                        {
                            lblLoginError.Text = "Invalid username or password.";
                        }
                    }
                }
            }
        }
        private void LoadUserSettings(int userId)
        {
            try
            {
                using (var conn = new SqlConnection(UserDbConnectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM UserSettings WHERE UserId=@UserId", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get settings from database
                                RunSqlAutomatically = reader.GetBoolean(reader.GetOrdinal("RunSqlAutomatically"));
                                IncludeSampleData = reader.GetBoolean(reader.GetOrdinal("IncludeSampleData"));
                            }
                            else
                            {
                                // Default settings if none exist
                                RunSqlAutomatically = false;
                                IncludeSampleData = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Just use defaults if error
                RunSqlAutomatically = false;
                IncludeSampleData = false;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_userId == null)
            {
                MessageBox.Show("You must log in first.");
                tabControl1.SelectedIndex = 0;
                return;
            }

            string connStr = txtConnStr.Text;
            string dbType = ddlDbType.SelectedItem?.ToString();
            string mode = ddlMode.SelectedItem?.ToString();
            _dbType = dbType;
            _mode = mode;
            try
            {
                using (var conn = CreateConnection(connStr))
                {
                    conn.Open();
                }
                _connectionString = connStr;
                lblConnStatus.Text = "Connection successful!";
                LoadTables(connStr);
                tabControl1.TabPages[2].Enabled = true;
                tabControl1.SelectedIndex = 2;
                tabControl1.TabPages[4].Enabled = true; // Adjust index as needed
            }
            catch (Exception ex)
            {
                lblConnStatus.Text = "Connection failed: " + ex.Message;
            }
        }

        private void LoadTables(string connStr)
        {
            var dt = new DataTable();

            if (_dbType?.ToLower() == "mysql")
            {
                // MySQL-specific code to get tables
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    // Get tables for MySQL
                    string query = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
                            WHERE TABLE_SCHEMA = DATABASE() AND TABLE_TYPE = 'BASE TABLE'";
                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            else if (_dbType?.ToLower() == "postgresql")
            {
                // PostgreSQL-specific code to get tables
                using (var conn = new NpgsqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"SELECT table_name AS TABLE_NAME 
                                    FROM information_schema.tables 
                                    WHERE table_schema = 'public' 
                                    AND table_type = 'BASE TABLE'";
                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            else
            {
                // Existing SQL Server code
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    dt = conn.GetSchema("Tables");
                }
            }

            // Load enabled tables for this user
            _enabledTables.Clear();
            if (_userId != null)
            {
                using (var conn = new SqlConnection(UserDbConnectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT ActualTableName FROM UserEnabledTables WHERE UserId=@UserId", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", _userId.Value);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _enabledTables.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }

            var aliasDict = new Dictionary<string, List<string>>();
            using (var conn = new SqlConnection(UserDbConnectionString))
            {
                conn.Open();
                using (var aliasCmd = new SqlCommand("SELECT ActualTableName, Alias FROM UserTableAliases WHERE UserId=@UserId", conn))
                {
                    aliasCmd.Parameters.AddWithValue("@UserId", _userId ?? 0);
                    using (var reader = aliasCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var table = reader.GetString(0);
                            var alias = reader.GetString(1);
                            if (!aliasDict.ContainsKey(table)) aliasDict[table] = new List<string>();
                            aliasDict[table].Add(alias);
                        }
                    }
                }
            }

            var tableList = new List<TableMapRow>();
            foreach (DataRow row in dt.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                string aliases = aliasDict.ContainsKey(tableName) ? string.Join(", ", aliasDict[tableName]) : "";
                tableList.Add(new TableMapRow
                {
                    ActualTableName = tableName,
                    FriendlyTableAliases = aliases,
                    Enabled = _enabledTables.Contains(tableName)
                });
            }
            dataGridViewTables.DataSource = tableList.OrderBy(t => t.ActualTableName).ToList();

            // Set checkbox values
            foreach (DataGridViewRow row in dataGridViewTables.Rows)
            {
                string tableName = row.Cells["ActualTableName"].Value?.ToString();
                row.Cells["Enabled"].Value = _enabledTables.Contains(tableName);
            }

            // Add "Map Columns" button column if not already present
            if (dataGridViewTables.Columns["MapColumns"] == null)
            {
                var mapButton = new DataGridViewButtonColumn
                {
                    Name = "MapColumns",
                    HeaderText = "",
                    Text = "Map Columns",
                    UseColumnTextForButtonValue = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dataGridViewTables.Columns.Add(mapButton);
            }

            // Set default column widths for better visibility
            if (dataGridViewTables.Columns["ActualTableName"] != null)
                dataGridViewTables.Columns["ActualTableName"].Width = 220;
            if (dataGridViewTables.Columns["FriendlyTableAliases"] != null)
                dataGridViewTables.Columns["FriendlyTableAliases"].Width = 220;
            if (dataGridViewTables.Columns["Enabled"] != null)
                dataGridViewTables.Columns["Enabled"].Width = 80;
            if (dataGridViewTables.Columns["MapColumns"] != null)
                dataGridViewTables.Columns["MapColumns"].Width = 120;

            // Handle checkbox changes
            dataGridViewTables.CellValueChanged -= DataGridViewTables_CellValueChanged;
            dataGridViewTables.CellValueChanged += DataGridViewTables_CellValueChanged;
            dataGridViewTables.CurrentCellDirtyStateChanged -= DataGridViewTables_CurrentCellDirtyStateChanged;
            dataGridViewTables.CurrentCellDirtyStateChanged += DataGridViewTables_CurrentCellDirtyStateChanged;
        }

        // Event handler for checkbox changes
        private void DataGridViewTables_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTables.Columns["Enabled"].Index && e.RowIndex >= 0)
            {
                var row = dataGridViewTables.Rows[e.RowIndex];
                string tableName = row.Cells["ActualTableName"].Value?.ToString();
                bool enabled = Convert.ToBoolean(row.Cells["Enabled"].Value);

                if (_userId == null || string.IsNullOrEmpty(tableName))
                    return;

                using (var conn = new SqlConnection(UserDbConnectionString))
                {
                    conn.Open();
                    if (enabled)
                    {
                        // Add to enabled tables
                        var cmd = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM UserEnabledTables WHERE UserId=@UserId AND ActualTableName=@Table) INSERT INTO UserEnabledTables (UserId, ActualTableName) VALUES (@UserId, @Table)", conn);
                        cmd.Parameters.AddWithValue("@UserId", _userId.Value);
                        cmd.Parameters.AddWithValue("@Table", tableName);
                        cmd.ExecuteNonQuery();
                        _enabledTables.Add(tableName);
                    }
                    else
                    {
                        // Remove from enabled tables
                        var cmd = new SqlCommand("DELETE FROM UserEnabledTables WHERE UserId=@UserId AND ActualTableName=@Table", conn);
                        cmd.Parameters.AddWithValue("@UserId", _userId.Value);
                        cmd.Parameters.AddWithValue("@Table", tableName);
                        cmd.ExecuteNonQuery();
                        _enabledTables.Remove(tableName);
                    }
                }
            }
        }

        // Commit checkbox changes immediately
        private void DataGridViewTables_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewTables.IsCurrentCellDirty)
                dataGridViewTables.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridViewTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewTables.Columns[e.ColumnIndex].Name == "MapColumns")
            {
                string tableName = dataGridViewTables.Rows[e.RowIndex].Cells["ActualTableName"].Value?.ToString();
                lblSelectedTable.Text = tableName;
                LoadColumns(_connectionString, tableName);
                tabControl1.TabPages[3].Enabled = true;
                tabControl1.SelectedIndex = 3;
            }
        }

        private void LoadColumns(string connStr, string tableName)
        {
            var dt = new DataTable();

            if (_dbType?.ToLower() == "mysql")
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                           WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @TableName";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TableName", tableName);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            else if (_dbType?.ToLower() == "postgresql")
            {
                using (var conn = new NpgsqlConnection(connStr))
                {
                    conn.Open();
                    string query = @"SELECT column_name AS COLUMN_NAME 
                           FROM information_schema.columns 
                           WHERE table_schema = 'public' 
                           AND table_name = @TableName";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TableName", tableName);
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            else
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    dt = conn.GetSchema("Columns", new[] { null, null, tableName, null });
                }
            }

            var colList = new List<ColumnMapRow>();
            foreach (DataRow row in dt.Rows)
            {
                string colName = row["COLUMN_NAME"].ToString();
                string friendly = (ColumnMappings.ContainsKey(tableName) && ColumnMappings[tableName].ContainsKey(colName))
                    ? ColumnMappings[tableName][colName]
                    : colName;
                colList.Add(new ColumnMapRow { ActualColumnName = colName, FriendlyColumnAliases = friendly });
            }
            dataGridViewColumns.DataSource = colList;

            // Set default column widths for better visibility
            if (dataGridViewColumns.Columns["ActualColumnName"] != null)
                dataGridViewColumns.Columns["ActualColumnName"].Width = 220;
            if (dataGridViewColumns.Columns["FriendlyColumnAliases"] != null)
                dataGridViewColumns.Columns["FriendlyColumnAliases"].Width = 220;
        }

        private void btnSaveTableMappings_Click(object sender, EventArgs e)
        {
            if (_userId == null) return;
            using (var conn = new SqlConnection(UserDbConnectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridViewTables.Rows)
                {
                    if (row.IsNewRow) continue;
                    string actual = row.Cells["ActualTableName"].Value?.ToString();
                    string aliasesRaw = row.Cells["FriendlyTableAliases"].Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(actual) && !string.IsNullOrWhiteSpace(aliasesRaw))
                    {
                        var aliases = aliasesRaw.Split(',').Select(a => a.Trim()).Where(a => !string.IsNullOrEmpty(a)).ToList();
                        var delCmd = new SqlCommand("DELETE FROM UserTableAliases WHERE UserId=@UserId AND ActualTableName=@Actual", conn);
                        delCmd.Parameters.AddWithValue("@UserId", _userId.Value);
                        delCmd.Parameters.AddWithValue("@Actual", actual);
                        delCmd.ExecuteNonQuery();
                        foreach (var alias in aliases)
                        {
                            var insCmd = new SqlCommand("INSERT INTO UserTableAliases (UserId, ActualTableName, Alias) VALUES (@UserId, @Actual, @Alias)", conn);
                            insCmd.Parameters.AddWithValue("@UserId", _userId.Value);
                            insCmd.Parameters.AddWithValue("@Actual", actual);
                            insCmd.Parameters.AddWithValue("@Alias", alias);
                            insCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(_connectionString))
                LoadTables(_connectionString);
            // Enable both Column Mapping and Query tabs
            tabControl1.TabPages[4].Enabled = true; // Query
            tabControl1.SelectedIndex = 2; // Stay on Table Mapping or go to next as you wish
        }

        private void btnSaveColumnMappings_Click(object sender, EventArgs e)
        {
            if (_userId == null) return;
            string tableName = lblSelectedTable.Text;
            if (!ColumnMappings.ContainsKey(tableName))
                ColumnMappings[tableName] = new Dictionary<string, string>();

            using (var conn = new SqlConnection(UserDbConnectionString))
            {
                conn.Open();
                foreach (DataGridViewRow row in dataGridViewColumns.Rows)
                {
                    if (row.IsNewRow) continue;
                    string actual = row.Cells["ActualColumnName"].Value?.ToString();
                    string friendly = row.Cells["FriendlyColumnAliases"].Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(actual) && !string.IsNullOrWhiteSpace(friendly))
                    {
                        ColumnMappings[tableName][actual] = friendly;
                        var cmd = new SqlCommand(@"
                        IF EXISTS (SELECT 1 FROM UserColumnMappings WHERE UserId=@UserId AND ActualTableName=@Table AND ActualColumnName=@Actual)
                            UPDATE UserColumnMappings SET FriendlyColumnName=@Friendly WHERE UserId=@UserId AND ActualTableName=@Table AND ActualColumnName=@Actual
                        ELSE
                            INSERT INTO UserColumnMappings (UserId, ActualTableName, ActualColumnName, FriendlyColumnName) VALUES (@UserId, @Table, @Actual, @Friendly)
                        ", conn);
                        cmd.Parameters.AddWithValue("@UserId", _userId.Value);
                        cmd.Parameters.AddWithValue("@Table", tableName);
                        cmd.Parameters.AddWithValue("@Actual", actual);
                        cmd.Parameters.AddWithValue("@Friendly", friendly);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            if (!string.IsNullOrEmpty(_connectionString))
                LoadColumns(_connectionString, tableName);
            tabControl1.TabPages[4].Enabled = true;
            tabControl1.SelectedIndex = 3;
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }
        private void btnBackToTables_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages[3].Enabled = false;
            tabControl1.SelectedIndex = 2;
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_userId == null || string.IsNullOrEmpty(_connectionString))
            {
                MessageBox.Show("You must complete all previous steps first.");
                tabControl1.SelectedIndex = 0;
                return;
            }

            string userInput = txtUserInput.Text;
            string apiKey = "";
            string connectionString = _connectionString;

            if (string.IsNullOrWhiteSpace(userInput))
                return;

            // Add user message to chat
            var userMessage = new ChatMessage(userInput, ChatMessage.MessageType.User);
            chatHistoryPanel.Controls.Add(userMessage);
            chatHistoryPanel.ScrollControlIntoView(userMessage);

            // Clear input field
            txtUserInput.Text = "";

            try
            {
                if (RunSqlAutomatically)
                {
                    // Show thinking indicator
                    var thinkingMsg = new ChatMessage("Thinking...", ChatMessage.MessageType.System);
                    chatHistoryPanel.Controls.Add(thinkingMsg);
                    chatHistoryPanel.ScrollControlIntoView(thinkingMsg);

                    var (debugInfo, results, rawResponse) = await TranslateAndExecuteSql(userInput, apiKey, connectionString, IncludeSampleData);

                    // Remove thinking indicator
                    chatHistoryPanel.Controls.Remove(thinkingMsg);

                    // Add AI response to chat
                    var aiResponseMessage = new ChatMessage(debugInfo, ChatMessage.MessageType.AI, results); // Renamed variable
                    chatHistoryPanel.Controls.Add(aiResponseMessage);
                    chatHistoryPanel.ScrollControlIntoView(aiResponseMessage);

                    // Also update hidden result text for compatibility
                    txtResult.Text = debugInfo;

                    // Update data grid if we have results
                    dataGridViewResults.DataSource = results;
                    if (results != null)
                    {
                        foreach (DataGridViewColumn col in dataGridViewResults.Columns)
                        {
                            col.Width = 150;
                        }
                    }
                }
                else
                {
                    // Only generate, don't execute
                    var thinkingMsg = new ChatMessage("Generating SQL...", ChatMessage.MessageType.System);
                    chatHistoryPanel.Controls.Add(thinkingMsg);

                    // Generate the query logic (same as before)
                    string dbType = _dbType?.ToLower() ?? "sqlserver";
                    string dbTypeText = dbType == "sqlserver" ? "SQL Server" :
                                        dbType == "mysql" ? "MySQL" :
                                        dbType == "postgresql" ? "PostgreSQL" : dbType;
                    string connStr = _connectionString;
                    string schemaInfo = BuildSchemaInfo(connStr, IncludeSampleData);
                    string mode = _mode ?? "read";
                    string allowedCommands = mode == "read" ? "SELECT" : "SELECT, INSERT, UPDATE, DELETE";
                    string prompt = $@"{schemaInfo}
                        Translate the following natural language request into a {dbTypeText} compatible SQL query WITHOUT backticks (`) AND WITHOUT markdown, just simple text.
                        First, output only the SQL query. Then, on a new line, output '---' and then a brief comment (max 1-2 sentences) explaining what the query does.
                        If the request cannot be answered exactly with the provided tables and columns, suggest a similar or possible query that can be answered, and explain your suggestion after the '---'.
                        Only use these commands: {allowedCommands}.

                        Request: ""{userInput}""
                        ";

                    string openAiResponse = await CallOpenAI(prompt, apiKey);

                    // Remove thinking indicator
                    chatHistoryPanel.Controls.Remove(thinkingMsg);

                    var responseJson = Newtonsoft.Json.Linq.JObject.Parse(openAiResponse);
                    var sqlQueryRaw = responseJson["choices"]?[0]?["message"]?["content"]?.ToString();

                    if (string.IsNullOrWhiteSpace(sqlQueryRaw))
                    {
                        var errorMsg = $"Unable to extract SQL query. Raw OpenAI response:\n\n{openAiResponse}";
                        var aiErrorMessage = new ChatMessage(errorMsg, ChatMessage.MessageType.AI); // Renamed variable
                        chatHistoryPanel.Controls.Add(aiErrorMessage);
                        chatHistoryPanel.ScrollControlIntoView(aiErrorMessage);
                        txtResult.Text = errorMsg;
                        return;
                    }

                    // Split the response into SQL and comment using the separator '---'
                    string sqlQuery = "";
                    string aiComment = "";
                    var parts = sqlQueryRaw.Split(new[] { "---" }, StringSplitOptions.None);
                    if (parts.Length > 0)
                        sqlQuery = parts[0].Trim();
                    if (parts.Length > 1)
                        aiComment = parts[1].Trim();

                    string chatResponse = $"Generated SQL Query:\n{sqlQuery}\n\nAI Comment/Suggestion:\n{aiComment}\n";

                    // Add AI response to chat
                    var aiResponseMessage = new ChatMessage(chatResponse, ChatMessage.MessageType.AI); // Renamed variable
                    chatHistoryPanel.Controls.Add(aiResponseMessage);
                    chatHistoryPanel.ScrollControlIntoView(aiResponseMessage);
                    txtResult.Text = chatResponse;
                    dataGridViewResults.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                var errorMsg = "Error: " + ex.Message;
                var errorMessage = new ChatMessage(errorMsg, ChatMessage.MessageType.System);
                chatHistoryPanel.Controls.Add(errorMessage);
                chatHistoryPanel.ScrollControlIntoView(errorMessage);
                txtResult.Text = errorMsg;
                dataGridViewResults.DataSource = null;
            }

            ScrollToBottomOfChat();
        }


        private void LoadUserMappings(int userId)
        {
            TableMappings.Clear();
            ColumnMappings.Clear();

            using (var conn = new SqlConnection(UserDbConnectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand("SELECT ActualTableName, FriendlyTableName FROM UserTableMappings WHERE UserId=@UserId", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableMappings[reader.GetString(0)] = reader.GetString(1);
                        }
                    }
                }

                using (var cmd = new SqlCommand("SELECT ActualTableName, ActualColumnName, FriendlyColumnName FROM UserColumnMappings WHERE UserId=@UserId", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string table = reader.GetString(0);
                            string col = reader.GetString(1);
                            string friendly = reader.GetString(2);
                            if (!ColumnMappings.ContainsKey(table))
                                ColumnMappings[table] = new Dictionary<string, string>();
                            ColumnMappings[table][col] = friendly;
                        }
                    }
                }
            }
        }

        public async Task<string> CallOpenAI(string prompt, string apiKey)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-4o",
                messages = new[] { new { role = "user", content = prompt } }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private string BuildSchemaInfo(string connStr, bool includeSampleData = false)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Here is the database schema:");

            var tableColumns = new Dictionary<string, List<string>>();
            var relationships = new List<string>();
            var userId = Convert.ToInt32(_userId);

            // Get table and column aliases
            var tableAliasDict = new Dictionary<string, List<string>>();
            using (var conn = new SqlConnection(UserDbConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT ActualTableName, Alias FROM UserTableAliases WHERE UserId=@UserId", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var table = reader.GetString(0);
                            var alias = reader.GetString(1);
                            if (!tableAliasDict.ContainsKey(table)) tableAliasDict[table] = new List<string>();
                            tableAliasDict[table].Add(alias);
                        }
                    }
                }
            }

            var columnAliasDict = new Dictionary<string, Dictionary<string, List<string>>>();
            using (var conn = new SqlConnection(UserDbConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT ActualTableName, ActualColumnName, Alias FROM UserColumnAliases WHERE UserId=@UserId", conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var table = reader.GetString(0);
                            var col = reader.GetString(1);
                            var alias = reader.GetString(2);
                            if (!columnAliasDict.ContainsKey(table)) columnAliasDict[table] = new Dictionary<string, List<string>>();
                            if (!columnAliasDict[table].ContainsKey(col)) columnAliasDict[table][col] = new List<string>();
                            columnAliasDict[table][col].Add(alias);
                        }
                    }
                }
            }

            // Branch for different database types
            if (_dbType?.ToLower() == "mysql")
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // Get MySQL tables
                    string tableQuery = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
                                WHERE TABLE_SCHEMA = DATABASE() AND TABLE_TYPE = 'BASE TABLE'";
                    using (var cmd = new MySqlCommand(tableQuery, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tableName = reader.GetString(0);
                            tableColumns[tableName] = new List<string>();
                        }
                    }

                    // Get columns for each table
                    foreach (var tableName in tableColumns.Keys.ToList())
                    {
                        string colQuery = @"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                                  WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @TableName";
                        using (var cmd = new MySqlCommand(colQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@TableName", tableName);
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    tableColumns[tableName].Add(reader.GetString(0));
                                }
                            }
                        }
                    }

                    // Get relationships (foreign keys)
                    string fkQuery = @"SELECT
                              TABLE_NAME AS ParentTable,
                              COLUMN_NAME AS ParentColumn,
                              REFERENCED_TABLE_NAME AS RefTable,
                              REFERENCED_COLUMN_NAME AS RefColumn
                            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                            WHERE REFERENCED_TABLE_SCHEMA = DATABASE()
                            AND REFERENCED_TABLE_NAME IS NOT NULL";

                    using (var cmd = new MySqlCommand(fkQuery, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string parentTable = reader["ParentTable"].ToString();
                            string parentColumn = reader["ParentColumn"].ToString();
                            string refTable = reader["RefTable"].ToString();
                            string refColumn = reader["RefColumn"].ToString();
                            relationships.Add($"{parentTable}.{parentColumn} → {refTable}.{refColumn}");
                        }
                    }
                }
            }
            else if (_dbType?.ToLower() == "postgresql")
            {
                using (var conn = new NpgsqlConnection(connStr))
                {
                    conn.Open();

                    // Get PostgreSQL tables
                    string tableQuery = @"SELECT table_name AS TABLE_NAME 
                           FROM information_schema.tables 
                           WHERE table_schema = 'public' 
                           AND table_type = 'BASE TABLE'";
                    using (var cmd = new NpgsqlCommand(tableQuery, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tableName = reader.GetString(0);
                            tableColumns[tableName] = new List<string>();
                        }
                    }

                    // Get columns for each table
                    foreach (var tableName in tableColumns.Keys.ToList())
                    {
                        string colQuery = @"SELECT column_name 
                             FROM information_schema.columns 
                             WHERE table_schema = 'public' 
                             AND table_name = @TableName";
                        using (var cmd = new NpgsqlCommand(colQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@TableName", tableName);
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    tableColumns[tableName].Add(reader.GetString(0));
                                }
                            }
                        }
                    }

                    // Get relationships (foreign keys)
                    string fkQuery = @"SELECT
                        tc.table_name AS ParentTable,
                        kcu.column_name AS ParentColumn,
                        ccu.table_name AS RefTable,
                        ccu.column_name AS RefColumn
                    FROM information_schema.table_constraints AS tc
                    JOIN information_schema.key_column_usage AS kcu
                        ON tc.constraint_name = kcu.constraint_name
                        AND tc.table_schema = kcu.table_schema
                    JOIN information_schema.constraint_column_usage AS ccu
                        ON ccu.constraint_name = tc.constraint_name
                        AND ccu.table_schema = tc.table_schema
                    WHERE tc.constraint_type = 'FOREIGN KEY'
                    AND tc.table_schema = 'public'";

                    using (var cmd = new NpgsqlCommand(fkQuery, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string parentTable = reader["ParentTable"].ToString();
                            string parentColumn = reader["ParentColumn"].ToString();
                            string refTable = reader["RefTable"].ToString();
                            string refColumn = reader["RefColumn"].ToString();
                            relationships.Add($"{parentTable}.{parentColumn} → {refTable}.{refColumn}");
                        }
                    }
                }
            }
            else
            {
                // SQL Server schema extraction
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Get tables and columns
                    var tables = conn.GetSchema("Tables");
                    foreach (DataRow tableRow in tables.Rows)
                    {
                        string tableName = tableRow["TABLE_NAME"].ToString();
                        var columns = conn.GetSchema("Columns", new[] { null, null, tableName, null });
                        var colNames = columns.Rows.Cast<DataRow>()
                            .Select(r => r["COLUMN_NAME"].ToString())
                            .ToList();
                        tableColumns[tableName] = colNames;
                    }

                    // Get relationships
                    var fkCmd = @"SELECT 
                                fk.name AS FK_Name,
                                tp.name AS ParentTable,
                                cp.name AS ParentColumn,
                                tr.name AS RefTable,
                                cr.name AS RefColumn
                            FROM sys.foreign_keys fk
                            INNER JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
                            INNER JOIN sys.tables tp ON fkc.parent_object_id = tp.object_id
                            INNER JOIN sys.columns cp ON fkc.parent_object_id = cp.object_id AND fkc.parent_column_id = cp.column_id
                            INNER JOIN sys.tables tr ON fkc.referenced_object_id = tr.object_id
                            INNER JOIN sys.columns cr ON fkc.referenced_object_id = cr.object_id AND fkc.referenced_column_id = cr.column_id
                            ORDER BY tp.name, cp.name";
                    using (var cmd = new SqlCommand(fkCmd, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string parentTable = reader["ParentTable"].ToString();
                            string parentColumn = reader["ParentColumn"].ToString();
                            string refTable = reader["RefTable"].ToString();
                            string refColumn = reader["RefColumn"].ToString();
                            relationships.Add($"{parentTable}.{parentColumn} → {refTable}.{refColumn}");
                        }
                    }
                }
            }

            // Build schema info string
            foreach (var kvp in tableColumns)
            {
                var tableName = kvp.Key;
                // Only include enabled tables
                if (_enabledTables.Count > 0 && !_enabledTables.Contains(tableName))
                    continue;

                var tableAliases = tableAliasDict.ContainsKey(tableName) ? $" (aliases: {string.Join(", ", tableAliasDict[tableName])})" : "";
                sb.AppendLine($"- {tableName}{tableAliases}");

                foreach (var col in kvp.Value)
                {
                    string colAliases = (columnAliasDict.ContainsKey(tableName) && columnAliasDict[tableName].ContainsKey(col))
                        ? $" (aliases: {string.Join(", ", columnAliasDict[tableName][col])})"
                        : "";
                    sb.AppendLine($"    - {col}{colAliases}");
                }

                // Add sample data if requested
                if (includeSampleData)
                {
                    try
                    {
                        // Fetch one sample row
                        using (var sampleConn = CreateConnection(connStr))
                        {
                            sampleConn.Open();
                            string sampleQuery = _dbType?.ToLower() == "mysql"
                                ? $"SELECT * FROM `{tableName}` LIMIT 1"
                                : _dbType?.ToLower() == "postgresql"
                                    ? $"SELECT * FROM \"{tableName}\" LIMIT 1"
                                    : $"SELECT TOP 1 * FROM [{tableName}]";

                            using (var cmd =
                                sampleConn is MySqlConnection
                                    ? (DbCommand)new MySqlCommand(sampleQuery, sampleConn as MySqlConnection)
                                    : sampleConn is NpgsqlConnection
                                        ? (DbCommand)new NpgsqlCommand(sampleQuery, sampleConn as NpgsqlConnection)
                                        : new SqlCommand(sampleQuery, sampleConn as SqlConnection))
                            {
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        sb.AppendLine($"    Sample row:");
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            string colName = reader.GetName(i);
                                            string safeValue = reader.IsDBNull(i)
                                                ? "NULL"
                                                : reader.GetValue(i).ToString()?.Length > 50
                                                    ? reader.GetValue(i).ToString().Substring(0, 47) + "..."
                                                    : reader.GetValue(i).ToString();

                                            // Escape any special characters
                                            safeValue = safeValue.Replace("\r", "").Replace("\n", " ");

                                            sb.AppendLine($"      - {colName}: {safeValue}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine($"    (Could not fetch sample data: {ex.Message})");
                    }
                }
            }

            if (relationships.Count > 0)
            {
                sb.AppendLine("Relationships:");
                foreach (var rel in relationships.Distinct())
                {
                    sb.AppendLine($"- {rel}");
                }
            }

            sb.AppendLine("Do not invent table or column names. Only use the tables and columns listed above.");
            return sb.ToString();
        }

        public async Task<(string debugInfo, DataTable results, string rawResponse)> TranslateAndExecuteSql(string userInput, string apiKey, string connectionString, bool includeSampleData = false)
        {
            string dbType = _dbType?.ToLower() ?? "sqlserver";
            string dbTypeText = dbType == "sqlserver" ? "SQL Server" :
                                dbType == "mysql" ? "MySQL" :
                                dbType == "postgresql" ? "PostgreSQL" : dbType;
            string connStr = _connectionString;

            // Pass includeSampleData parameter
            string schemaInfo = BuildSchemaInfo(connStr, includeSampleData);

            // Rest of method remains the same
            string mode = _mode ?? "read";
            // Simplified check since we now have exact "read" and "write" options
            string allowedCommands = mode == "read" ? "SELECT" : "SELECT, INSERT, UPDATE, DELETE";

            // Updated prompt: ask for a suggestion if the request can't be answered
            string prompt = $@"{schemaInfo}
                        Translate the following natural language request into a {dbTypeText} compatible SQL query WITHOUT backticks (`) AND WITHOUT markdown, just simple text.
                        First, output only the SQL query. Then, on a new line, output '---' and then a brief comment (max 1-2 sentences) explaining what the query does.
                        If the request cannot be answered exactly with the provided tables and columns, suggest a similar or possible query that can be answered, and explain your suggestion after the '---'.
                        Only use these commands: {allowedCommands}.

                        Request: ""{userInput}""
                        ";

            string openAiResponse = await CallOpenAI(prompt, apiKey);

            try
            {
                var responseJson = Newtonsoft.Json.Linq.JObject.Parse(openAiResponse);
                var sqlQueryRaw = responseJson["choices"]?[0]?["message"]?["content"]?.ToString();

                if (string.IsNullOrWhiteSpace(sqlQueryRaw))
                {
                    // Show the raw OpenAI response for debugging
                    return ($"Unable to extract SQL query. Raw OpenAI response:\n\n{openAiResponse}", null, openAiResponse);
                }
                System.Diagnostics.Debug.WriteLine("OpenAI raw content: " + sqlQueryRaw);

                // Split the response into SQL and comment using the separator '---'
                string sqlQuery = "";
                string aiComment = "";
                var parts = sqlQueryRaw.Split(new[] { "---" }, StringSplitOptions.None);
                if (parts.Length > 0)
                    sqlQuery = parts[0].Trim();
                if (parts.Length > 1)
                    aiComment = parts[1].Trim();

                string[] validStarts = mode == "read"
                    ? new[] { "SELECT" }
                    : new[] { "SELECT", "INSERT", "UPDATE", "DELETE" };

                // If the AI could not generate a valid query, just show the suggestion/comment
                if (!validStarts.Any(cmd => sqlQuery.StartsWith(cmd, StringComparison.OrdinalIgnoreCase)))
                {
                    var debugInfo = $"No exact match for your request.\n\nAI Suggestion:\n{aiComment}\n";
                    return (debugInfo, null, openAiResponse);
                }

                var debugInfoFull = $"Generated SQL Query:\n{sqlQuery}\n\nAI Comment/Suggestion:\n{aiComment}\n";

                // Use the correct connection type based on database
                if (dbType == "postgresql")
                {
                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = new NpgsqlCommand(sqlQuery, connection))
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                var dt = new DataTable();
                                dt.Load(reader);
                                return (debugInfoFull, dt, openAiResponse);
                            }
                        }
                    }
                }
                else if (dbType == "mysql")
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = new MySqlCommand(sqlQuery, connection))
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                var dt = new DataTable();
                                dt.Load(reader);
                                return (debugInfoFull, dt, openAiResponse);
                            }
                        }
                    }
                }
                else
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = new SqlCommand(sqlQuery, connection))
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                var dt = new DataTable();
                                dt.Load(reader);
                                return (debugInfoFull, dt, openAiResponse);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Different exception handling for different database types
                if (dbType == "postgresql" && ex is PostgresException)
                {
                    var pgEx = (PostgresException)ex;
                    schemaInfo = BuildSchemaInfo(connStr);
                    string suggestionPrompt = $@"The user asked: ""{userInput}""
                                The following error occurred when running the generated PostgreSQL query: ""{pgEx.Message}""
                                {schemaInfo}

                                Suggest up to 10 likely intended table or column names (if any are misspelled or missing), and provide a corrected PostgreSQL query if possible.
                                Format your answer as:
                                - Suggestions: [list, max 10]
                                - Corrected SQL: [if possible]
                                - Explanation: [short explanation]
                                ";
                    string suggestionResponse = await CallOpenAI(suggestionPrompt, apiKey);
                    return ($"AI Suggestion:\n{suggestionResponse}", null, suggestionResponse);
                }
                else if (dbType == "mysql" && ex is MySqlException)
                {
                    var mysqlEx = (MySqlException)ex;
                    schemaInfo = BuildSchemaInfo(connStr);
                    string suggestionPrompt = $@"The user asked: ""{userInput}""
                                The following error occurred when running the generated MySQL query: ""{mysqlEx.Message}""
                                {schemaInfo}

                                Suggest up to 10 likely intended table or column names (if any are misspelled or missing), and provide a corrected MySQL query if possible.
                                Format your answer as:
                                - Suggestions: [list, max 10]
                                - Corrected SQL: [if possible]
                                - Explanation: [short explanation]
                                ";
                    string suggestionResponse = await CallOpenAI(suggestionPrompt, apiKey);
                    return ($"AI Suggestion:\n{suggestionResponse}", null, suggestionResponse);
                }
                else if (ex is SqlException)
                {
                    var sqlEx = (SqlException)ex;
                    // Original SQL Server error handling
                    schemaInfo = BuildSchemaInfo(connStr);
                    string suggestionPrompt = $@"The user asked: ""{userInput}""
                                The following error occurred when running the generated SQL Server query: ""{sqlEx.Message}""
                                {schemaInfo}

                                Suggest up to 10 likely intended table or column names (if any are misspelled or missing), and provide a corrected SQL query if possible.
                                Format your answer as:
                                - Suggestions: [list, max 10]
                                - Corrected SQL: [if possible]
                                - Explanation: [short explanation]
                                ";
                    string suggestionResponse = await CallOpenAI(suggestionPrompt, apiKey);
                    return ($"AI Suggestion:\n{suggestionResponse}", null, suggestionResponse);
                }
                else
                {
                    return ("Error: " + ex.Message, null, openAiResponse);
                }
            }
        }

        public class TableMapRow
        {
            public string ActualTableName { get; set; }
            public string FriendlyTableAliases { get; set; }
            public bool Enabled { get; set; } // <-- Add this
        }
        public class ColumnMapRow
        {
            public string ActualColumnName { get; set; }
            public string FriendlyColumnAliases { get; set; }
        }

        private void tabLogin_Click(object sender, EventArgs e)
        {

        }

        private void exportExcelMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                using (var sfd = new SaveFileDialog { Filter = "Excel files (*.csv)|*.csv", FileName = "results.csv" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var sb = new StringBuilder();
                        var columnNames = dt.Columns.Cast<DataColumn>().Select(col => "\"" + col.ColumnName.Replace("\"", "\"\"") + "\"");
                        sb.AppendLine(string.Join(",", columnNames));
                        foreach (DataRow row in dt.Rows)
                        {
                            var fields = row.ItemArray.Select(field => "\"" + field?.ToString().Replace("\"", "\"\"") + "\"");
                            sb.AppendLine(string.Join(",", fields));
                        }
                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Exported to Excel (CSV) successfully.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No data to export.");
            }
        }

        private void exportCsvMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                using (var sfd = new SaveFileDialog { Filter = "CSV files (*.csv)|*.csv", FileName = "results.csv" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var sb = new StringBuilder();
                        // Write headers
                        var columnNames = dt.Columns.Cast<DataColumn>().Select(col => "\"" + col.ColumnName.Replace("\"", "\"\"") + "\"");
                        sb.AppendLine(string.Join(",", columnNames));
                        // Write rows
                        foreach (DataRow row in dt.Rows)
                        {
                            var fields = row.ItemArray.Select(field => "\"" + field?.ToString().Replace("\"", "\"\"") + "\"");
                            sb.AppendLine(string.Join(",", fields));
                        }
                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Exported to CSV successfully.");
                    }
                }
            }
            else
            {
                MessageBox.Show("No data to export.");
            }
        }


        private void tabQuery_Click(object sender, EventArgs e)
        {

        }

        private void btnAnalyzeQuery_Click(object sender, EventArgs e)
        {
            // Make sure we have an API key (using the same one as in your other API calls)
            string apiKey = "";

            // Create and show the form, passing the callback to process identified tables
            var form = new QueryAnalysisForm(apiKey, _dbType, EnableIdentifiedTables);
            form.ShowDialog(this);
        }
        private void EnableIdentifiedTables(List<string> tableNames)
        {
            if (tableNames == null || tableNames.Count == 0)
            {
                MessageBox.Show("No tables were identified in the query.", "Table Analysis",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Ask if user wants to reset all first and enable only the identified tables
            DialogResult dr = MessageBox.Show(
                $"Found {tableNames.Count} tables in the query:\n\n" +
                string.Join(", ", tableNames) +
                "\n\nDo you want to enable only these tables?",
                "Table Analysis",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Cancel)
                return;

            if (dr == DialogResult.Yes)
            {
                // First disable all tables
                foreach (DataGridViewRow row in dataGridViewTables.Rows)
                {
                    string tableName = row.Cells["ActualTableName"].Value?.ToString();
                    if (!string.IsNullOrEmpty(tableName))
                    {
                        row.Cells["Enabled"].Value = false;
                        // Also remove from _enabledTables (this will be updated properly by the event handler)
                        _enabledTables.Remove(tableName);
                    }
                }
            }

            // Enable the identified tables
            foreach (DataGridViewRow row in dataGridViewTables.Rows)
            {
                string tableName = row.Cells["ActualTableName"].Value?.ToString();
                if (!string.IsNullOrEmpty(tableName) && tableNames.Any(t =>
                    string.Equals(t, tableName, StringComparison.OrdinalIgnoreCase) ||
                    t.EndsWith("." + tableName, StringComparison.OrdinalIgnoreCase)))
                {
                    row.Cells["Enabled"].Value = true;
                    if (!_enabledTables.Contains(tableName))
                    {
                        _enabledTables.Add(tableName);

                        // Note: Your actual logic to add to the database would be triggered
                        // by the DataGridViewTables_CellValueChanged event handler
                    }
                }
            }

            // Notify the user
            MessageBox.Show($"Enabled {tableNames.Count} tables from the query.",
                "Table Analysis", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblLoginError.Text = "Username and password are required.";
                return;
            }

            using (var conn = new SqlConnection(UserDbConnectionString))
            {
                conn.Open();
                // Check if user already exists
                using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE username = @username", conn))
                {
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        lblLoginError.Text = "Username already exists.";
                        return;
                    }
                }

                // Get the RoleId for 'User'
                int roleId;
                using (var roleCmd = new SqlCommand("SELECT RoleId FROM Roles WHERE RoleName = @role", conn))
                {
                    roleCmd.Parameters.AddWithValue("@role", "User");
                    roleId = (int)roleCmd.ExecuteScalar();
                }

                // Register new user with default role
                using (var regCmd = new SqlCommand("INSERT INTO Users (username, password, RoleId) VALUES (@username, @password, @roleId)", conn))
                {
                    regCmd.Parameters.AddWithValue("@username", username);
                    regCmd.Parameters.AddWithValue("@password", password);
                    regCmd.Parameters.AddWithValue("@roleId", roleId);
                    regCmd.ExecuteNonQuery();
                }

                // Log in the new user and get role name
                using (var loginCmd = new SqlCommand(
                    @"SELECT u.userid, r.RoleName 
              FROM Users u 
              LEFT JOIN Roles r ON u.RoleId = r.RoleId 
              WHERE u.username = @username AND u.password = @password", conn))
                {
                    loginCmd.Parameters.AddWithValue("@username", username);
                    loginCmd.Parameters.AddWithValue("@password", password);
                    using (var reader = loginCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _userId = reader.GetInt32(0);
                            string roleName = reader.IsDBNull(1) ? "User" : reader.GetString(1);
                            LoadUserMappings(_userId.Value);
                            tabControl1.TabPages[1].Enabled = true;
                            tabControl1.SelectedIndex = 1;
                            lblLoginError.Text = "";
                            lblCurrentUser.Text = $"Logged in as: {username} ({roleName})";
                            btnSettings.Enabled = true;
                        }
                        else
                        {
                            lblLoginError.Text = "Registration failed. Please try again.";
                        }
                    }
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Create settings form instance
            var settingsForm = new SettingsForm();

            // Pass current API key to settings form
            settingsForm.ApiKey = "";
            settingsForm.DefaultDbType = _dbType ?? "SQL Server";

            // Pass current toggle states from properties
            settingsForm.RunSqlAutomatically = RunSqlAutomatically;
            settingsForm.IncludeSampleData = IncludeSampleData;

            // Set UserId for saving settings
            settingsForm.UserId = _userId;

            // Load existing settings from database
            if (_userId.HasValue)
            {
                settingsForm.LoadSettingsFromDb(UserDbConnectionString);
            }

            // Show form and handle result
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // Apply settings to properties
                RunSqlAutomatically = settingsForm.RunSqlAutomatically;
                IncludeSampleData = settingsForm.IncludeSampleData;

                // Save settings to database if user is logged in
                if (_userId.HasValue)
                {
                    SaveUserSettings(_userId.Value, settingsForm);
                }
            }
        }
        private void SaveUserSettings(int userId, SettingsForm settings)
        {
            try
            {
                using (var conn = new SqlConnection(UserDbConnectionString))
                {
                    conn.Open();

                    // Check if settings exist for the user
                    bool hasSettings = false;
                    using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM UserSettings WHERE UserId=@UserId", conn))
                    {
                        checkCmd.Parameters.AddWithValue("@UserId", userId);
                        hasSettings = ((int)checkCmd.ExecuteScalar()) > 0;
                    }

                    // Insert or update settings
                    string cmdText = hasSettings
                        ? "UPDATE UserSettings SET RunSqlAutomatically=@RunSql, IncludeSampleData=@IncludeSample, ApiKey=@ApiKey, DefaultDbType=@DbType WHERE UserId=@UserId"
                        : "INSERT INTO UserSettings (UserId, RunSqlAutomatically, IncludeSampleData, ApiKey, DefaultDbType) VALUES (@UserId, @RunSql, @IncludeSample, @ApiKey, @DbType)";

                    using (var cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@RunSql", settings.RunSqlAutomatically);
                        cmd.Parameters.AddWithValue("@IncludeSample", settings.IncludeSampleData);
                        cmd.Parameters.AddWithValue("@ApiKey", settings.ApiKey);
                        cmd.Parameters.AddWithValue("@DbType", settings.DefaultDbType);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add this method to handle query help requests
        private async void btnQueryHelp_Click(object sender, EventArgs e)
        {
            if (_userId == null || string.IsNullOrEmpty(_connectionString))
            {
                MessageBox.Show("You must complete all previous steps first.");
                tabControl1.SelectedIndex = 0;
                return;
            }

            // Create a form to get both the existing query and the help request
            using (var queryHelpForm = new QueryHelpForm())
            {
                if (queryHelpForm.ShowDialog() == DialogResult.OK)
                {
                    string existingQuery = queryHelpForm.ExistingQuery;
                    string helpRequest = queryHelpForm.HelpRequest;
                    
                    if (string.IsNullOrWhiteSpace(existingQuery) || string.IsNullOrWhiteSpace(helpRequest))
                    {
                        MessageBox.Show("Please provide both the existing query and your help request.");
                        return;
                    }

                    // Add user message to chat
                    var userMessage = new ChatMessage($"Help with query: {helpRequest}\n\nExisting query:\n{existingQuery}", 
                        ChatMessage.MessageType.User);
                    chatHistoryPanel.Controls.Add(userMessage);
                    chatHistoryPanel.ScrollControlIntoView(userMessage);

                    // Show thinking indicator
                    var thinkingMsg = new ChatMessage("Analyzing query...", ChatMessage.MessageType.System);
                    chatHistoryPanel.Controls.Add(thinkingMsg);
                    chatHistoryPanel.ScrollControlIntoView(thinkingMsg);

                    try
                    {
                        string apiKey = "";
                        string dbType = _dbType?.ToLower() ?? "sqlserver";
                        string dbTypeText = dbType == "sqlserver" ? "SQL Server" :
                                           dbType == "mysql" ? "MySQL" :
                                           dbType == "postgresql" ? "PostgreSQL" : dbType;
                        string schemaInfo = BuildSchemaInfo(_connectionString, IncludeSampleData);
                        
                        string prompt = $@"{schemaInfo}
                            You are a SQL expert for {dbTypeText}. Please help the user with their query.
                            
                            User's existing query:
                            {existingQuery}
                            
                            User's request:
                            {helpRequest}
                            
                            Provide the following in your response:
                            1. A brief analysis of the existing query
                            2. A modified version of the query that addresses the user's request
                            3. An explanation of the changes made
                            
                            Format your response as:
                            ANALYSIS: [Your analysis of the existing query]
                            
                            MODIFIED QUERY:
                            [The modified SQL query]
                            
                            EXPLANATION: [Explanation of what was changed and why]
                            ";

                        string openAiResponse = await CallOpenAI(prompt, apiKey);
                        
                        // Remove thinking indicator
                        chatHistoryPanel.Controls.Remove(thinkingMsg);
                        
                        var responseJson = Newtonsoft.Json.Linq.JObject.Parse(openAiResponse);
                        var aiContent = responseJson["choices"]?[0]?["message"]?["content"]?.ToString();
                        
                        if (string.IsNullOrWhiteSpace(aiContent))
                        {
                            var errorMsg = $"Unable to generate help for the query. Raw OpenAI response:\n\n{openAiResponse}";
                            var aiErrorMessage = new ChatMessage(errorMsg, ChatMessage.MessageType.AI);
                            chatHistoryPanel.Controls.Add(aiErrorMessage);
                            chatHistoryPanel.ScrollControlIntoView(aiErrorMessage);
                            return;
                        }
                        
                        // Add AI response to chat
                        var aiResponseMessage = new ChatMessage(aiContent, ChatMessage.MessageType.AI);
                        chatHistoryPanel.Controls.Add(aiResponseMessage);
                        chatHistoryPanel.ScrollControlIntoView(aiResponseMessage);
                        
                        // Extract the modified query to make it executable
                        string modifiedQuery = ExtractModifiedQuery(aiContent);
                        if (!string.IsNullOrWhiteSpace(modifiedQuery))
                        {
                            // Add "Run this query" button to the chat message
                            aiResponseMessage.AddExecuteQueryButton(modifiedQuery, (query) => {
                                ExecuteModifiedQuery(query);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        // Remove thinking indicator
                        chatHistoryPanel.Controls.Remove(thinkingMsg);
                        
                        var errorMsg = "Error: " + ex.Message;
                        var errorMessage = new ChatMessage(errorMsg, ChatMessage.MessageType.System);
                        chatHistoryPanel.Controls.Add(errorMessage);
                        chatHistoryPanel.ScrollControlIntoView(errorMessage);
                    }
                    
                    ScrollToBottomOfChat();
                }
            }
        }

        // Helper method to extract the modified query from the AI response
        private string ExtractModifiedQuery(string aiResponse)
        {
            if (string.IsNullOrWhiteSpace(aiResponse))
                return null;
                
            // Look for the modified query section
            int start = aiResponse.IndexOf("MODIFIED QUERY:", StringComparison.OrdinalIgnoreCase);
            if (start < 0)
                return null;
                
            start += "MODIFIED QUERY:".Length;
            
            // Find the end of the query section
            int end = aiResponse.IndexOf("EXPLANATION:", start, StringComparison.OrdinalIgnoreCase);
            if (end < 0)
                end = aiResponse.Length;
                
            string query = aiResponse.Substring(start, end - start).Trim();
            return query;
        }

        // Method to execute the modified query
        private async void ExecuteModifiedQuery(string query)
        {
            try
            {
                // Show executing message
                var executingMsg = new ChatMessage("Executing query...", ChatMessage.MessageType.System);
                chatHistoryPanel.Controls.Add(executingMsg);
                chatHistoryPanel.ScrollControlIntoView(executingMsg);
                
                DataTable results = null;
                
                // Use the correct connection type based on database
                string dbType = _dbType?.ToLower() ?? "sqlserver";
                if (dbType == "postgresql")
                {
                    using (var connection = new NpgsqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                results = new DataTable();
                                results.Load(reader);
                            }
                        }
                    }
                }
                else if (dbType == "mysql")
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = new MySqlCommand(query, connection))
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                results = new DataTable();
                                results.Load(reader);
                            }
                        }
                    }
                }
                else
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = new SqlCommand(query, connection))
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                results = new DataTable();
                                results.Load(reader);
                            }
                        }
                    }
                }
                
                // Remove executing message
                chatHistoryPanel.Controls.Remove(executingMsg);
                
                // Add results message
                string resultMessage = "Query executed successfully!";
                var resultMsg = new ChatMessage(resultMessage, ChatMessage.MessageType.AI, results);
                chatHistoryPanel.Controls.Add(resultMsg);
                chatHistoryPanel.ScrollControlIntoView(resultMsg);
                
                // Update data grid
                dataGridViewResults.DataSource = results;
                if (results != null)
                {
                    foreach (DataGridViewColumn col in dataGridViewResults.Columns)
                    {
                        col.Width = 150;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMsg = "Error executing query: " + ex.Message;
                var errorMessage = new ChatMessage(errorMsg, ChatMessage.MessageType.System);
                chatHistoryPanel.Controls.Add(errorMessage);
                chatHistoryPanel.ScrollControlIntoView(errorMessage);
            }
            
            ScrollToBottomOfChat();
        }
    }
}
