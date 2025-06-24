using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AskDB_Desktop
{
    public partial class ResultsForm : Form
    {
        public ResultsForm()
        {
            InitializeComponent();
        }

        public ResultsForm(DataTable results) : this()
        {
            dataGridViewResults.DataSource = results;
        }


        private void btnExport_Click_1(object sender, EventArgs e)
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
    }
}