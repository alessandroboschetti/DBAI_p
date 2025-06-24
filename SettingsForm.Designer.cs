namespace AskDB_Desktop
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label lblDbType;
        private System.Windows.Forms.ComboBox cmbDbType;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblApiKey = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.lblDbType = new System.Windows.Forms.Label();
            this.cmbDbType = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.metroToggle1 = new MetroFramework.Controls.MetroToggle();
            this.metroToggle2 = new MetroFramework.Controls.MetroToggle();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(12, 15);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(45, 13);
            this.lblApiKey.TabIndex = 0;
            this.lblApiKey.Text = "API Key";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(120, 12);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(300, 20);
            this.txtApiKey.TabIndex = 1;
            // 
            // lblDbType
            // 
            this.lblDbType.AutoSize = true;
            this.lblDbType.Location = new System.Drawing.Point(12, 50);
            this.lblDbType.Name = "lblDbType";
            this.lblDbType.Size = new System.Drawing.Size(86, 13);
            this.lblDbType.TabIndex = 2;
            this.lblDbType.Text = "Default DB Type";
            // 
            // cmbDbType
            // 
            this.cmbDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDbType.FormattingEnabled = true;
            this.cmbDbType.Items.AddRange(new object[] {
            "SQL Server",
            "MySQL",
            "PostgreSQL"});
            this.cmbDbType.Location = new System.Drawing.Point(120, 47);
            this.cmbDbType.Name = "cmbDbType";
            this.cmbDbType.Size = new System.Drawing.Size(180, 21);
            this.cmbDbType.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 233);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(216, 233);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Send Sample Row when generating query";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // metroToggle1
            // 
            this.metroToggle1.AutoSize = true;
            this.metroToggle1.Location = new System.Drawing.Point(340, 94);
            this.metroToggle1.Name = "metroToggle1";
            this.metroToggle1.Size = new System.Drawing.Size(80, 17);
            this.metroToggle1.TabIndex = 7;
            this.metroToggle1.Text = "Off";
            this.metroToggle1.UseSelectable = true;
            // 
            // metroToggle2
            // 
            this.metroToggle2.AutoSize = true;
            this.metroToggle2.Location = new System.Drawing.Point(340, 131);
            this.metroToggle2.Name = "metroToggle2";
            this.metroToggle2.Size = new System.Drawing.Size(80, 17);
            this.metroToggle2.TabIndex = 9;
            this.metroToggle2.Text = "Off";
            this.metroToggle2.UseSelectable = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Run Select Queries automatically";
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(440, 275);
            this.Controls.Add(this.metroToggle2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.metroToggle1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbDbType);
            this.Controls.Add(this.lblDbType);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.lblApiKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroToggle metroToggle1;
        private MetroFramework.Controls.MetroToggle metroToggle2;
        private System.Windows.Forms.Label label2;
    }
}