using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AskDB_Desktop
{
    public partial class SettingsForm : Form
    {
        public int? UserId { get; set; }

        public string ApiKey
        {
            get => txtApiKey.Text;
            set => txtApiKey.Text = value;
        }

        public string DefaultDbType
        {
            get => cmbDbType.SelectedItem?.ToString();
            set => cmbDbType.SelectedItem = value;
        }

        public bool RunSqlAutomatically
        {
            get => metroToggle2.Checked;
            set => metroToggle2.Checked = value;
        }

        public bool IncludeSampleData
        {
            get => metroToggle1.Checked;
            set => metroToggle1.Checked = value;
        }

        public SettingsForm()
        {
            InitializeComponent();
        }

        public void LoadSettingsFromDb(string connectionString)
        {
            if (!UserId.HasValue) return;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT * FROM UserSettings WHERE UserId=@UserId", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", UserId.Value);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get settings from database
                                RunSqlAutomatically = reader.GetBoolean(reader.GetOrdinal("RunSqlAutomatically"));
                                IncludeSampleData = reader.GetBoolean(reader.GetOrdinal("IncludeSampleData"));

                                if (!reader.IsDBNull(reader.GetOrdinal("ApiKey")))
                                    ApiKey = reader.GetString(reader.GetOrdinal("ApiKey"));

                                if (!reader.IsDBNull(reader.GetOrdinal("DefaultDbType")))
                                    DefaultDbType = reader.GetString(reader.GetOrdinal("DefaultDbType"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // You can add validation or saving logic here
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Toggle when clicking on label
            metroToggle1.Checked = !metroToggle1.Checked;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Toggle when clicking on label
            metroToggle2.Checked = !metroToggle2.Checked;
        }
    }
}