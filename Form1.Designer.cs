using System.Windows.Forms;

namespace AskDB_Desktop
{
    partial class Form1
    {
        private MetroFramework.Controls.MetroTabControl tabControl1;
        private MetroFramework.Controls.MetroTabPage tabLogin;
        private MetroFramework.Controls.MetroTabPage tabConnect;
        private MetroFramework.Controls.MetroTabPage tabTableMapping;
        private MetroFramework.Controls.MetroTabPage tabColumnMapping;
        private MetroFramework.Controls.MetroTabPage tabQuery;

        private MetroFramework.Controls.MetroLabel lblUserDbConnStr;
        private MetroFramework.Controls.MetroTextBox txtUserDbConnStr;
        private MetroFramework.Controls.MetroTextBox txtUsername;
        private MetroFramework.Controls.MetroTextBox txtPassword;
        private MetroFramework.Controls.MetroButton btnLogin;
        private MetroFramework.Controls.MetroLabel lblLoginError;

        private MetroFramework.Controls.MetroTextBox txtConnStr;
        private MetroFramework.Controls.MetroComboBox ddlDbType;
        private MetroFramework.Controls.MetroComboBox ddlMode;
        private MetroFramework.Controls.MetroButton btnConnect;
        private MetroFramework.Controls.MetroLabel lblConnStatus;

        private MetroFramework.Controls.MetroGrid dataGridViewTables;
        private MetroFramework.Controls.MetroButton btnSaveTableMappings;
        private MetroFramework.Controls.MetroButton btnBackToLogin;

        private MetroFramework.Controls.MetroLabel lblSelectedTable;
        private MetroFramework.Controls.MetroGrid dataGridViewColumns;
        private MetroFramework.Controls.MetroButton btnSaveColumnMappings;
        private MetroFramework.Controls.MetroButton btnBackToTables;

        private MetroFramework.Controls.MetroTextBox txtUserInput;
        private MetroFramework.Controls.MetroButton btnSubmit;
        private MetroFramework.Controls.MetroTextBox txtResult;
        private MetroFramework.Controls.MetroGrid dataGridViewResults;
        private System.Windows.Forms.ContextMenuStrip exportMenu;
        private System.Windows.Forms.ToolStripMenuItem exportCsvMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportExcelMenuItem;


        private System.Windows.Forms.FlowLayoutPanel chatHistoryPanel;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.tabLogin = new MetroFramework.Controls.MetroTabPage();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.lblUserDbConnStr = new MetroFramework.Controls.MetroLabel();
            this.txtUserDbConnStr = new MetroFramework.Controls.MetroTextBox();
            this.txtUsername = new MetroFramework.Controls.MetroTextBox();
            this.txtPassword = new MetroFramework.Controls.MetroTextBox();
            this.btnLogin = new MetroFramework.Controls.MetroButton();
            this.lblLoginError = new MetroFramework.Controls.MetroLabel();
            this.tabConnect = new MetroFramework.Controls.MetroTabPage();
            this.txtConnStr = new MetroFramework.Controls.MetroTextBox();
            this.ddlDbType = new MetroFramework.Controls.MetroComboBox();
            this.ddlMode = new MetroFramework.Controls.MetroComboBox();
            this.btnConnect = new MetroFramework.Controls.MetroButton();
            this.lblConnStatus = new MetroFramework.Controls.MetroLabel();
            this.tabTableMapping = new MetroFramework.Controls.MetroTabPage();
            this.btnAnalyzeQuery = new MetroFramework.Controls.MetroButton();
            this.dataGridViewTables = new MetroFramework.Controls.MetroGrid();
            this.btnSaveTableMappings = new MetroFramework.Controls.MetroButton();
            this.btnBackToLogin = new MetroFramework.Controls.MetroButton();
            this.tabColumnMapping = new MetroFramework.Controls.MetroTabPage();
            this.lblSelectedTable = new MetroFramework.Controls.MetroLabel();
            this.dataGridViewColumns = new MetroFramework.Controls.MetroGrid();
            this.btnSaveColumnMappings = new MetroFramework.Controls.MetroButton();
            this.btnBackToTables = new MetroFramework.Controls.MetroButton();
            this.tabQuery = new MetroFramework.Controls.MetroTabPage();
            this.txtUserInput = new MetroFramework.Controls.MetroTextBox();
            this.btnSubmit = new MetroFramework.Controls.MetroButton();
            this.chatHistoryPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.txtResult = new MetroFramework.Controls.MetroTextBox();
            this.dataGridViewResults = new MetroFramework.Controls.MetroGrid();
            this.tabQueryOptimizer = new System.Windows.Forms.TabPage();
            this.txtOptimizerInput = new System.Windows.Forms.TextBox();
            this.btnOptimizeQuery = new System.Windows.Forms.Button();
            this.txtOptimizerResult = new System.Windows.Forms.TextBox();
            this.exportMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportCsvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportExcelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCurrentUser = new MetroFramework.Drawing.Html.HtmlLabel();
            this.btnSettings = new MetroFramework.Controls.MetroButton();
            this.tabControl1.SuspendLayout();
            this.tabLogin.SuspendLayout();
            this.tabConnect.SuspendLayout();
            this.tabTableMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).BeginInit();
            this.tabColumnMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).BeginInit();
            this.tabQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.exportMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLogin);
            this.tabControl1.Controls.Add(this.tabConnect);
            this.tabControl1.Controls.Add(this.tabTableMapping);
            this.tabControl1.Controls.Add(this.tabColumnMapping);
            this.tabControl1.Controls.Add(this.tabQuery);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(20, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 2;
            this.tabControl1.Size = new System.Drawing.Size(860, 520);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.UseSelectable = true;
            // 
            // tabLogin
            // 
            this.tabLogin.Controls.Add(this.metroButton1);
            this.tabLogin.Controls.Add(this.lblUserDbConnStr);
            this.tabLogin.Controls.Add(this.txtUserDbConnStr);
            this.tabLogin.Controls.Add(this.txtUsername);
            this.tabLogin.Controls.Add(this.txtPassword);
            this.tabLogin.Controls.Add(this.btnLogin);
            this.tabLogin.Controls.Add(this.lblLoginError);
            this.tabLogin.HorizontalScrollbarBarColor = true;
            this.tabLogin.HorizontalScrollbarHighlightOnWheel = false;
            this.tabLogin.HorizontalScrollbarSize = 10;
            this.tabLogin.Location = new System.Drawing.Point(4, 38);
            this.tabLogin.Name = "tabLogin";
            this.tabLogin.Size = new System.Drawing.Size(852, 478);
            this.tabLogin.TabIndex = 0;
            this.tabLogin.Text = "Login";
            this.tabLogin.VerticalScrollbarBarColor = true;
            this.tabLogin.VerticalScrollbarHighlightOnWheel = false;
            this.tabLogin.VerticalScrollbarSize = 10;
            this.tabLogin.Click += new System.EventHandler(this.tabLogin_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(220, 231);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(130, 63);
            this.metroButton1.TabIndex = 6;
            this.metroButton1.Text = "Register";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblUserDbConnStr
            // 
            this.lblUserDbConnStr.Location = new System.Drawing.Point(40, 10);
            this.lblUserDbConnStr.Name = "lblUserDbConnStr";
            this.lblUserDbConnStr.Size = new System.Drawing.Size(200, 23);
            this.lblUserDbConnStr.TabIndex = 0;
            this.lblUserDbConnStr.Text = "User DB Connection String:";
            // 
            // txtUserDbConnStr
            // 
            // 
            // 
            // 
            this.txtUserDbConnStr.CustomButton.Image = null;
            this.txtUserDbConnStr.CustomButton.Location = new System.Drawing.Point(578, 1);
            this.txtUserDbConnStr.CustomButton.Name = "";
            this.txtUserDbConnStr.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtUserDbConnStr.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUserDbConnStr.CustomButton.TabIndex = 1;
            this.txtUserDbConnStr.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUserDbConnStr.CustomButton.UseSelectable = true;
            this.txtUserDbConnStr.CustomButton.Visible = false;
            this.txtUserDbConnStr.Lines = new string[] {
        "Server=localhost\\SQLEXPRESS;Database=AskDBinfo;Trusted_Connection=True;"};
            this.txtUserDbConnStr.Location = new System.Drawing.Point(40, 36);
            this.txtUserDbConnStr.MaxLength = 32767;
            this.txtUserDbConnStr.Name = "txtUserDbConnStr";
            this.txtUserDbConnStr.PasswordChar = '\0';
            this.txtUserDbConnStr.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserDbConnStr.SelectedText = "";
            this.txtUserDbConnStr.SelectionLength = 0;
            this.txtUserDbConnStr.SelectionStart = 0;
            this.txtUserDbConnStr.ShortcutsEnabled = true;
            this.txtUserDbConnStr.Size = new System.Drawing.Size(600, 23);
            this.txtUserDbConnStr.TabIndex = 1;
            this.txtUserDbConnStr.Text = "Server=localhost\\SQLEXPRESS;Database=AskDBinfo;Trusted_Connection=True;";
            this.txtUserDbConnStr.UseSelectable = true;
            this.txtUserDbConnStr.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUserDbConnStr.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtUsername
            // 
            // 
            // 
            // 
            this.txtUsername.CustomButton.Image = null;
            this.txtUsername.CustomButton.Location = new System.Drawing.Point(288, 1);
            this.txtUsername.CustomButton.Name = "";
            this.txtUsername.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtUsername.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUsername.CustomButton.TabIndex = 1;
            this.txtUsername.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUsername.CustomButton.UseSelectable = true;
            this.txtUsername.CustomButton.Visible = false;
            this.txtUsername.Lines = new string[] {
        "user"};
            this.txtUsername.Location = new System.Drawing.Point(40, 110);
            this.txtUsername.MaxLength = 32767;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PasswordChar = '\0';
            this.txtUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUsername.SelectedText = "";
            this.txtUsername.SelectionLength = 0;
            this.txtUsername.SelectionStart = 0;
            this.txtUsername.ShortcutsEnabled = true;
            this.txtUsername.Size = new System.Drawing.Size(310, 23);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.Text = "user";
            this.txtUsername.UseSelectable = true;
            this.txtUsername.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUsername.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtPassword
            // 
            // 
            // 
            // 
            this.txtPassword.CustomButton.Image = null;
            this.txtPassword.CustomButton.Location = new System.Drawing.Point(288, 1);
            this.txtPassword.CustomButton.Name = "";
            this.txtPassword.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPassword.CustomButton.TabIndex = 1;
            this.txtPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPassword.CustomButton.UseSelectable = true;
            this.txtPassword.CustomButton.Visible = false;
            this.txtPassword.Lines = new string[] {
        "password"};
            this.txtPassword.Location = new System.Drawing.Point(40, 154);
            this.txtPassword.MaxLength = 32767;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.ShortcutsEnabled = true;
            this.txtPassword.Size = new System.Drawing.Size(310, 23);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "password";
            this.txtPassword.UseSelectable = true;
            this.txtPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(40, 231);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(121, 63);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseSelectable = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblLoginError
            // 
            this.lblLoginError.ForeColor = System.Drawing.Color.Red;
            this.lblLoginError.Location = new System.Drawing.Point(40, 205);
            this.lblLoginError.Name = "lblLoginError";
            this.lblLoginError.Size = new System.Drawing.Size(200, 23);
            this.lblLoginError.TabIndex = 5;
            // 
            // tabConnect
            // 
            this.tabConnect.Controls.Add(this.txtConnStr);
            this.tabConnect.Controls.Add(this.ddlDbType);
            this.tabConnect.Controls.Add(this.ddlMode);
            this.tabConnect.Controls.Add(this.btnConnect);
            this.tabConnect.Controls.Add(this.lblConnStatus);
            this.tabConnect.HorizontalScrollbarBarColor = true;
            this.tabConnect.HorizontalScrollbarHighlightOnWheel = false;
            this.tabConnect.HorizontalScrollbarSize = 10;
            this.tabConnect.Location = new System.Drawing.Point(4, 35);
            this.tabConnect.Name = "tabConnect";
            this.tabConnect.Size = new System.Drawing.Size(852, 481);
            this.tabConnect.TabIndex = 1;
            this.tabConnect.Text = "Connect";
            this.tabConnect.VerticalScrollbarBarColor = true;
            this.tabConnect.VerticalScrollbarHighlightOnWheel = false;
            this.tabConnect.VerticalScrollbarSize = 10;
            // 
            // txtConnStr
            // 
            // 
            // 
            // 
            this.txtConnStr.CustomButton.Image = null;
            this.txtConnStr.CustomButton.Location = new System.Drawing.Point(578, 1);
            this.txtConnStr.CustomButton.Name = "";
            this.txtConnStr.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtConnStr.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtConnStr.CustomButton.TabIndex = 1;
            this.txtConnStr.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtConnStr.CustomButton.UseSelectable = true;
            this.txtConnStr.CustomButton.Visible = false;
            this.txtConnStr.Lines = new string[] {
        "Server=localhost\\SQLEXPRESS;Database=AskDBinfo;Trusted_Connection=True;"};
            this.txtConnStr.Location = new System.Drawing.Point(40, 40);
            this.txtConnStr.MaxLength = 32767;
            this.txtConnStr.Name = "txtConnStr";
            this.txtConnStr.PasswordChar = '\0';
            this.txtConnStr.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtConnStr.SelectedText = "";
            this.txtConnStr.SelectionLength = 0;
            this.txtConnStr.SelectionStart = 0;
            this.txtConnStr.ShortcutsEnabled = true;
            this.txtConnStr.Size = new System.Drawing.Size(600, 23);
            this.txtConnStr.TabIndex = 0;
            this.txtConnStr.Text = "Server=localhost\\SQLEXPRESS;Database=AskDBinfo;Trusted_Connection=True;";
            this.txtConnStr.UseSelectable = true;
            this.txtConnStr.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtConnStr.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // ddlDbType
            // 
            this.ddlDbType.ItemHeight = 23;
            this.ddlDbType.Location = new System.Drawing.Point(40, 90);
            this.ddlDbType.Name = "ddlDbType";
            this.ddlDbType.Size = new System.Drawing.Size(200, 29);
            this.ddlDbType.TabIndex = 1;
            this.ddlDbType.UseSelectable = true;
            // 
            // ddlMode
            // 
            this.ddlMode.ItemHeight = 23;
            this.ddlMode.Location = new System.Drawing.Point(260, 90);
            this.ddlMode.Name = "ddlMode";
            this.ddlMode.Size = new System.Drawing.Size(200, 29);
            this.ddlMode.TabIndex = 2;
            this.ddlMode.UseSelectable = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(40, 140);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(121, 63);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseSelectable = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblConnStatus
            // 
            this.lblConnStatus.Location = new System.Drawing.Point(40, 224);
            this.lblConnStatus.Name = "lblConnStatus";
            this.lblConnStatus.Size = new System.Drawing.Size(600, 119);
            this.lblConnStatus.TabIndex = 4;
            // 
            // tabTableMapping
            // 
            this.tabTableMapping.Controls.Add(this.btnAnalyzeQuery);
            this.tabTableMapping.Controls.Add(this.dataGridViewTables);
            this.tabTableMapping.Controls.Add(this.btnSaveTableMappings);
            this.tabTableMapping.Controls.Add(this.btnBackToLogin);
            this.tabTableMapping.HorizontalScrollbarBarColor = true;
            this.tabTableMapping.HorizontalScrollbarHighlightOnWheel = false;
            this.tabTableMapping.HorizontalScrollbarSize = 10;
            this.tabTableMapping.Location = new System.Drawing.Point(4, 38);
            this.tabTableMapping.Name = "tabTableMapping";
            this.tabTableMapping.Size = new System.Drawing.Size(852, 478);
            this.tabTableMapping.TabIndex = 2;
            this.tabTableMapping.Text = "Table Mapping";
            this.tabTableMapping.VerticalScrollbarBarColor = true;
            this.tabTableMapping.VerticalScrollbarHighlightOnWheel = false;
            this.tabTableMapping.VerticalScrollbarSize = 10;
            // 
            // btnAnalyzeQuery
            // 
            this.btnAnalyzeQuery.Location = new System.Drawing.Point(0, 0);
            this.btnAnalyzeQuery.Name = "btnAnalyzeQuery";
            this.btnAnalyzeQuery.Size = new System.Drawing.Size(151, 23);
            this.btnAnalyzeQuery.TabIndex = 3;
            this.btnAnalyzeQuery.Text = "Analyse Tables From Query";
            this.btnAnalyzeQuery.UseSelectable = true;
            this.btnAnalyzeQuery.Click += new System.EventHandler(this.btnAnalyzeQuery_Click);
            // 
            // dataGridViewTables
            // 
            this.dataGridViewTables.AllowUserToResizeRows = false;
            this.dataGridViewTables.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridViewTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTables.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewTables.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTables.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTables.EnableHeadersVisualStyles = false;
            this.dataGridViewTables.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataGridViewTables.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridViewTables.Location = new System.Drawing.Point(3, 40);
            this.dataGridViewTables.Name = "dataGridViewTables";
            this.dataGridViewTables.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTables.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTables.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTables.Size = new System.Drawing.Size(837, 300);
            this.dataGridViewTables.TabIndex = 0;
            // 
            // btnSaveTableMappings
            // 
            this.btnSaveTableMappings.Location = new System.Drawing.Point(204, 360);
            this.btnSaveTableMappings.Name = "btnSaveTableMappings";
            this.btnSaveTableMappings.Size = new System.Drawing.Size(132, 53);
            this.btnSaveTableMappings.TabIndex = 1;
            this.btnSaveTableMappings.Text = "Save Table Mappings";
            this.btnSaveTableMappings.UseSelectable = true;
            this.btnSaveTableMappings.Click += new System.EventHandler(this.btnSaveTableMappings_Click);
            // 
            // btnBackToLogin
            // 
            this.btnBackToLogin.Location = new System.Drawing.Point(3, 360);
            this.btnBackToLogin.Name = "btnBackToLogin";
            this.btnBackToLogin.Size = new System.Drawing.Size(131, 53);
            this.btnBackToLogin.TabIndex = 2;
            this.btnBackToLogin.Text = "Back to Login";
            this.btnBackToLogin.UseSelectable = true;
            this.btnBackToLogin.Click += new System.EventHandler(this.btnBackToLogin_Click);
            // 
            // tabColumnMapping
            // 
            this.tabColumnMapping.Controls.Add(this.lblSelectedTable);
            this.tabColumnMapping.Controls.Add(this.dataGridViewColumns);
            this.tabColumnMapping.Controls.Add(this.btnSaveColumnMappings);
            this.tabColumnMapping.Controls.Add(this.btnBackToTables);
            this.tabColumnMapping.HorizontalScrollbarBarColor = true;
            this.tabColumnMapping.HorizontalScrollbarHighlightOnWheel = false;
            this.tabColumnMapping.HorizontalScrollbarSize = 10;
            this.tabColumnMapping.Location = new System.Drawing.Point(4, 35);
            this.tabColumnMapping.Name = "tabColumnMapping";
            this.tabColumnMapping.Size = new System.Drawing.Size(852, 481);
            this.tabColumnMapping.TabIndex = 3;
            this.tabColumnMapping.Text = "Column Mapping";
            this.tabColumnMapping.VerticalScrollbarBarColor = true;
            this.tabColumnMapping.VerticalScrollbarHighlightOnWheel = false;
            this.tabColumnMapping.VerticalScrollbarSize = 10;
            // 
            // lblSelectedTable
            // 
            this.lblSelectedTable.Location = new System.Drawing.Point(3, 14);
            this.lblSelectedTable.Name = "lblSelectedTable";
            this.lblSelectedTable.Size = new System.Drawing.Size(200, 23);
            this.lblSelectedTable.TabIndex = 0;
            this.lblSelectedTable.Text = "Selected Table:";
            // 
            // dataGridViewColumns
            // 
            this.dataGridViewColumns.AllowUserToResizeRows = false;
            this.dataGridViewColumns.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridViewColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewColumns.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewColumns.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewColumns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewColumns.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewColumns.EnableHeadersVisualStyles = false;
            this.dataGridViewColumns.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataGridViewColumns.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridViewColumns.Location = new System.Drawing.Point(3, 40);
            this.dataGridViewColumns.Name = "dataGridViewColumns";
            this.dataGridViewColumns.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewColumns.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewColumns.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewColumns.Size = new System.Drawing.Size(837, 300);
            this.dataGridViewColumns.TabIndex = 1;
            // 
            // btnSaveColumnMappings
            // 
            this.btnSaveColumnMappings.Location = new System.Drawing.Point(221, 360);
            this.btnSaveColumnMappings.Name = "btnSaveColumnMappings";
            this.btnSaveColumnMappings.Size = new System.Drawing.Size(141, 71);
            this.btnSaveColumnMappings.TabIndex = 2;
            this.btnSaveColumnMappings.Text = "Save Column Mappings";
            this.btnSaveColumnMappings.UseSelectable = true;
            this.btnSaveColumnMappings.Click += new System.EventHandler(this.btnSaveColumnMappings_Click);
            // 
            // btnBackToTables
            // 
            this.btnBackToTables.Location = new System.Drawing.Point(0, 360);
            this.btnBackToTables.Name = "btnBackToTables";
            this.btnBackToTables.Size = new System.Drawing.Size(133, 71);
            this.btnBackToTables.TabIndex = 3;
            this.btnBackToTables.Text = "Back to Tables";
            this.btnBackToTables.UseSelectable = true;
            this.btnBackToTables.Click += new System.EventHandler(this.btnBackToTables_Click);
            // 
            // tabQuery
            // 
            this.tabQuery.Controls.Add(this.txtUserInput);
            this.tabQuery.Controls.Add(this.btnSubmit);
            this.tabQuery.Controls.Add(this.chatHistoryPanel);
            this.tabQuery.Controls.Add(this.txtResult);
            this.tabQuery.Controls.Add(this.dataGridViewResults);
            this.tabQuery.HorizontalScrollbarBarColor = true;
            this.tabQuery.HorizontalScrollbarHighlightOnWheel = false;
            this.tabQuery.HorizontalScrollbarSize = 10;
            this.tabQuery.Location = new System.Drawing.Point(4, 38);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Size = new System.Drawing.Size(852, 478);
            this.tabQuery.TabIndex = 4;
            this.tabQuery.Text = "Query";
            this.tabQuery.VerticalScrollbarBarColor = true;
            this.tabQuery.VerticalScrollbarHighlightOnWheel = false;
            this.tabQuery.VerticalScrollbarSize = 10;
            this.tabQuery.Click += new System.EventHandler(this.tabQuery_Click);
            // 
            // txtUserInput
            // 
            // 
            // 
            // 
            this.txtUserInput.CustomButton.Image = null;
            this.txtUserInput.CustomButton.Location = new System.Drawing.Point(688, 2);
            this.txtUserInput.CustomButton.Name = "";
            this.txtUserInput.CustomButton.Size = new System.Drawing.Size(65, 65);
            this.txtUserInput.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUserInput.CustomButton.TabIndex = 1;
            this.txtUserInput.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUserInput.CustomButton.UseSelectable = true;
            this.txtUserInput.CustomButton.Visible = false;
            this.txtUserInput.Lines = new string[0];
            this.txtUserInput.Location = new System.Drawing.Point(3, 37);
            this.txtUserInput.MaxLength = 32767;
            this.txtUserInput.Multiline = true;
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.PasswordChar = '\0';
            this.txtUserInput.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserInput.SelectedText = "";
            this.txtUserInput.SelectionLength = 0;
            this.txtUserInput.SelectionStart = 0;
            this.txtUserInput.ShortcutsEnabled = true;
            this.txtUserInput.Size = new System.Drawing.Size(756, 70);
            this.txtUserInput.TabIndex = 0;
            this.txtUserInput.UseSelectable = true;
            this.txtUserInput.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUserInput.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(761, 37);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(88, 70);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseSelectable = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // chatHistoryPanel
            // 
            this.chatHistoryPanel.AutoScroll = true;
            this.chatHistoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatHistoryPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.chatHistoryPanel.Location = new System.Drawing.Point(0, 0);
            this.chatHistoryPanel.Name = "chatHistoryPanel";
            this.chatHistoryPanel.Padding = new System.Windows.Forms.Padding(10);
            this.chatHistoryPanel.Size = new System.Drawing.Size(852, 478);
            this.chatHistoryPanel.TabIndex = 10;
            this.chatHistoryPanel.WrapContents = false;
            // 
            // txtResult
            // 
            // 
            // 
            // 
            this.txtResult.CustomButton.Image = null;
            this.txtResult.CustomButton.Location = new System.Drawing.Point(-20, 2);
            this.txtResult.CustomButton.Name = "";
            this.txtResult.CustomButton.Size = new System.Drawing.Size(17, 17);
            this.txtResult.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtResult.CustomButton.TabIndex = 1;
            this.txtResult.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtResult.CustomButton.UseSelectable = true;
            this.txtResult.CustomButton.Visible = false;
            this.txtResult.Lines = new string[0];
            this.txtResult.Location = new System.Drawing.Point(0, 0);
            this.txtResult.MaxLength = 32767;
            this.txtResult.Name = "txtResult";
            this.txtResult.PasswordChar = '\0';
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtResult.SelectedText = "";
            this.txtResult.SelectionLength = 0;
            this.txtResult.SelectionStart = 0;
            this.txtResult.ShortcutsEnabled = true;
            this.txtResult.Size = new System.Drawing.Size(0, 22);
            this.txtResult.TabIndex = 11;
            this.txtResult.UseSelectable = true;
            this.txtResult.Visible = false;
            this.txtResult.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtResult.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.AllowUserToResizeRows = false;
            this.dataGridViewResults.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridViewResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewResults.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewResults.EnableHeadersVisualStyles = false;
            this.dataGridViewResults.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataGridViewResults.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridViewResults.Location = new System.Drawing.Point(0, 241);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewResults.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResults.Size = new System.Drawing.Size(853, 206);
            this.dataGridViewResults.TabIndex = 3;
            // 
            // tabQueryOptimizer
            // 
            this.tabQueryOptimizer.Location = new System.Drawing.Point(0, 0);
            this.tabQueryOptimizer.Name = "tabQueryOptimizer";
            this.tabQueryOptimizer.Size = new System.Drawing.Size(200, 100);
            this.tabQueryOptimizer.TabIndex = 0;
            // 
            // txtOptimizerInput
            // 
            this.txtOptimizerInput.Location = new System.Drawing.Point(0, 0);
            this.txtOptimizerInput.Name = "txtOptimizerInput";
            this.txtOptimizerInput.Size = new System.Drawing.Size(100, 20);
            this.txtOptimizerInput.TabIndex = 0;
            // 
            // btnOptimizeQuery
            // 
            this.btnOptimizeQuery.Location = new System.Drawing.Point(0, 0);
            this.btnOptimizeQuery.Name = "btnOptimizeQuery";
            this.btnOptimizeQuery.Size = new System.Drawing.Size(75, 23);
            this.btnOptimizeQuery.TabIndex = 0;
            // 
            // txtOptimizerResult
            // 
            this.txtOptimizerResult.Location = new System.Drawing.Point(0, 0);
            this.txtOptimizerResult.Name = "txtOptimizerResult";
            this.txtOptimizerResult.Size = new System.Drawing.Size(100, 20);
            this.txtOptimizerResult.TabIndex = 0;
            // 
            // exportMenu
            // 
            this.exportMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCsvMenuItem,
            this.exportExcelMenuItem});
            this.exportMenu.Name = "exportMenu";
            this.exportMenu.Size = new System.Drawing.Size(151, 48);
            // 
            // exportCsvMenuItem
            // 
            this.exportCsvMenuItem.Name = "exportCsvMenuItem";
            this.exportCsvMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exportCsvMenuItem.Text = "Export to CSV";
            this.exportCsvMenuItem.Click += new System.EventHandler(this.exportCsvMenuItem_Click);
            // 
            // exportExcelMenuItem
            // 
            this.exportExcelMenuItem.Name = "exportExcelMenuItem";
            this.exportExcelMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exportExcelMenuItem.Text = "Export to Excel";
            this.exportExcelMenuItem.Click += new System.EventHandler(this.exportExcelMenuItem_Click);
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoScroll = true;
            this.lblCurrentUser.AutoScrollMinSize = new System.Drawing.Size(10, 0);
            this.lblCurrentUser.AutoSize = false;
            this.lblCurrentUser.BackColor = System.Drawing.SystemColors.Window;
            this.lblCurrentUser.Location = new System.Drawing.Point(538, 31);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(165, 45);
            this.lblCurrentUser.TabIndex = 1;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(710, 31);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseSelectable = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "DBAI";
            this.tabControl1.ResumeLayout(false);
            this.tabLogin.ResumeLayout(false);
            this.tabConnect.ResumeLayout(false);
            this.tabTableMapping.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).EndInit();
            this.tabColumnMapping.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).EndInit();
            this.tabQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.exportMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton btnAnalyzeQuery;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Drawing.Html.HtmlLabel lblCurrentUser;
        private MetroFramework.Controls.MetroButton btnSettings;
    }
}