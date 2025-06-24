using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AskDB_Desktop
{
    public partial class QueryAnalysisForm : Form
    {
        private readonly string _apiKey;
        private readonly string _dbType;
        private readonly Action<List<string>> _onTablesIdentified;

        public QueryAnalysisForm(string apiKey, string dbType, Action<List<string>> onTablesIdentified)
        {
            InitializeComponent();
            _apiKey = apiKey;
            _dbType = dbType;
            _onTablesIdentified = onTablesIdentified;
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuery.Text))
            {
                MessageBox.Show("Please enter a SQL query to analyze.");
                return;
            }

            btnAnalyze.Enabled = false;
            lblStatus.Text = "Analyzing query...";
            Application.DoEvents();

            try
            {
                var tables = await ExtractTablesFromQuery(txtQuery.Text);
                _onTablesIdentified(tables);
                Close();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
                MessageBox.Show("Error analyzing query: " + ex.Message);
            }
            finally
            {
                btnAnalyze.Enabled = true;
            }
        }

        private async Task<List<string>> ExtractTablesFromQuery(string query)
        {
            string dbTypeText = _dbType?.ToLower() == "sqlserver" ? "SQL Server" :
                               _dbType?.ToLower() == "mysql" ? "MySQL" :
                               _dbType?.ToLower() == "postgresql" ? "PostgreSQL" : "SQL";

            string prompt = $@"
                Analyze the following {dbTypeText} query and extract ALL table names referenced in it.
                Return ONLY a comma-separated list of table names, with no additional text or explanation.
                If there are no tables referenced or you can't identify any tables, return 'NONE'.
                
                Query:
                {query}
            ";

            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

                var requestBody = new
                {
                    model = "gpt-4o",
                    messages = new[] { new { role = "user", content = prompt } }
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new System.Net.Http.StringContent(
                    json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    "https://api.openai.com/v1/chat/completions", content);
                var responseString = await response.Content.ReadAsStringAsync();

                var responseJson = JObject.Parse(responseString);
                var tableList = responseJson["choices"]?[0]?["message"]?["content"]?.ToString();

                if (string.IsNullOrWhiteSpace(tableList) || tableList.Trim().ToUpper() == "NONE")
                {
                    return new List<string>();
                }

                return new List<string>(tableList.Split(
                    new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim())
                    .Where(t => !string.IsNullOrWhiteSpace(t)));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}