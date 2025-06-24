namespace AskDB_Desktop
{
    partial class QueryHelpForm
    {
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblExistingQuery = new System.Windows.Forms.Label();
            this.txtExistingQuery = new System.Windows.Forms.TextBox();
            this.lblHelpRequest = new System.Windows.Forms.Label();
            this.txtHelpRequest = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblExistingQuery
            // 
            this.lblExistingQuery.AutoSize = true;
            this.lblExistingQuery.Location = new System.Drawing.Point(12, 9);
            this.lblExistingQuery.Name = "lblExistingQuery";
            this.lblExistingQuery.Size = new System.Drawing.Size(155, 17);
            this.lblExistingQuery.TabIndex = 0;
            this.lblExistingQuery.Text = "Paste your SQL query:";
            // 
            // txtExistingQuery
            // 
            this.txtExistingQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExistingQuery.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExistingQuery.Location = new System.Drawing.Point(12, 29);
            this.txtExistingQuery.Multiline = true;
            this.txtExistingQuery.Name = "txtExistingQuery";
            this.txtExistingQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtExistingQuery.Size = new System.Drawing.Size(560, 200);
            this.txtExistingQuery.TabIndex = 1;
            // 
            // lblHelpRequest
            // 
            this.lblHelpRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHelpRequest.AutoSize = true;
            this.lblHelpRequest.Location = new System.Drawing.Point(12, 242);
            this.lblHelpRequest.Name = "lblHelpRequest";
            this.lblHelpRequest.Size = new System.Drawing.Size(245, 17);
            this.lblHelpRequest.TabIndex = 2;
            this.lblHelpRequest.Text = "What help do you need with this query?";
            // 
            // txtHelpRequest
            // 
            this.txtHelpRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHelpRequest.Location = new System.Drawing.Point(12, 262);
            this.txtHelpRequest.Multiline = true;
            this.txtHelpRequest.Name = "txtHelpRequest";
            this.txtHelpRequest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHelpRequest.Size = new System.Drawing.Size(560, 100);
            this.txtHelpRequest.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(375, 375);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(95, 35);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(476, 375);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // QueryHelpForm
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 422);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtHelpRequest);
            this.Controls.Add(this.lblHelpRequest);
            this.Controls.Add(this.txtExistingQuery);
            this.Controls.Add(this.lblExistingQuery);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 460);
            this.Name = "QueryHelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Get Help With Query";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblExistingQuery;
        private System.Windows.Forms.TextBox txtExistingQuery;
        private System.Windows.Forms.Label lblHelpRequest;
        private System.Windows.Forms.TextBox txtHelpRequest;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
    }
}