using System;
using System.Windows.Forms;

namespace AskDB_Desktop
{
    public partial class QueryHelpForm : Form
    {
        public string ExistingQuery { get; private set; }
        public string HelpRequest { get; private set; }

        public QueryHelpForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ExistingQuery = txtExistingQuery.Text;
            HelpRequest = txtHelpRequest.Text;

            if (string.IsNullOrWhiteSpace(ExistingQuery))
            {
                MessageBox.Show("Please enter your existing SQL query.");
                return;
            }

            if (string.IsNullOrWhiteSpace(HelpRequest))
            {
                MessageBox.Show("Please describe what help you need with the query.");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}