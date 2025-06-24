using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data;

namespace AskDB_Desktop
{
    public class ChatMessage : UserControl
    {
        public enum MessageType { User, AI, System }

        private readonly Panel _msgPanel;
        private readonly TextBox _msgText; // Changed from Label to TextBox
        private readonly Button _viewResultsBtn;
        private readonly DataTable _results;
        private readonly MessageType _type;

        public ChatMessage(string message, MessageType type = MessageType.AI, DataTable results = null)
        {
            _type = type;
            _results = results;

            // Basic setup
            this.AutoSize = true;
            this.Margin = new Padding(5, 8, 5, 8);
            this.MinimumSize = new Size(100, 50);
            this.Padding = new Padding(5);
            this.BackColor = Color.Transparent;
            this.Dock = DockStyle.Top;

            // Message panel
            _msgPanel = new Panel();
            _msgPanel.AutoSize = true;
            _msgPanel.MinimumSize = new Size(200, 40);
            _msgPanel.MaximumSize = new Size(type == MessageType.User ? 600 : 700, 0);
            _msgPanel.Padding = new Padding(15);
            _msgPanel.BackColor = GetBackColorForType(type);

            // Create TextBox instead of Label
            _msgText = new TextBox();
            _msgText.Text = message?.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
            _msgText.Font = new Font("Segoe UI", 11F);
            _msgText.Multiline = true;
            _msgText.ReadOnly = true;
            _msgText.BorderStyle = BorderStyle.None;
            _msgText.BackColor = _msgPanel.BackColor;
            _msgText.ForeColor = GetForeColorForType(type);
            _msgText.Width = _msgPanel.MaximumSize.Width - 40;
            _msgText.Height = CalculateTextHeight(message, _msgText);
            _msgText.Cursor = Cursors.IBeam; // Show text selection cursor
            _msgText.ScrollBars = ScrollBars.None;
            
            // Make the TextBox look more like a label
            _msgText.TabStop = false;

            // Add to layout
            _msgPanel.Controls.Add(_msgText);
            this.Controls.Add(_msgPanel);

            // Position message based on type
            if (type == MessageType.User)
            {
                _msgPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
            else
            {
                _msgPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }

            // Add view results button if needed
            if (results != null && results.Rows.Count > 0)
            {
                _viewResultsBtn = new Button();
                _viewResultsBtn.Text = $"View Results ({results.Rows.Count} rows)";
                _viewResultsBtn.FlatStyle = FlatStyle.Flat;
                _viewResultsBtn.BackColor = Color.FromArgb(0, 120, 215);
                _viewResultsBtn.ForeColor = Color.White;
                _viewResultsBtn.Size = new Size(200, 30);
                _viewResultsBtn.Click += (s, e) => ShowResults();

                this.Controls.Add(_viewResultsBtn);

                // Position the button based on message type
                if (type == MessageType.User)
                {
                    _viewResultsBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                }
                else
                {
                    _viewResultsBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                }

                // Adjust height to accommodate button
                this.Height = _msgPanel.Height + _viewResultsBtn.Height + 20;
            }
            else
            {
                this.Height = _msgPanel.Height + 15;
            }

            // Handle painting for rounded corners
            _msgPanel.Paint += MsgPanel_Paint;

            // Handle layout updates
            this.Layout += OnMessageLayout;
        }

        // Calculate appropriate height for the text
        private int CalculateTextHeight(string text, TextBox textBox)
        {
            using (Graphics g = textBox.CreateGraphics())
            {
                SizeF size = g.MeasureString(text, textBox.Font, textBox.Width - 10);
                return Math.Max(60, (int)Math.Ceiling(size.Height) + 10);
            }
        }

        private void OnMessageLayout(object sender, LayoutEventArgs e)
        {
            if (_type == MessageType.User)
            {
                _msgPanel.Location = new Point(this.Width - _msgPanel.Width - 15, 5);
                if (_viewResultsBtn != null)
                {
                    _viewResultsBtn.Location = new Point(this.Width - _viewResultsBtn.Width - 15, _msgPanel.Bottom + 5);
                }
            }
            else
            {
                _msgPanel.Location = new Point(15, 5);
                if (_viewResultsBtn != null)
                {
                    _viewResultsBtn.Location = new Point(15, _msgPanel.Bottom + 5);
                }
            }
        }

        private void MsgPanel_Paint(object sender, PaintEventArgs e)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 8;
                Rectangle rect = _msgPanel.ClientRectangle;
                rect.Inflate(-1, -1);

                // Create rounded rectangle
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseAllFigures();

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Draw border
                using (Pen pen = new Pen(Color.FromArgb(40, 0, 0, 0), 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private Color GetBackColorForType(MessageType type)
        {
            switch (type)
            {
                case MessageType.User: return Color.FromArgb(220, 240, 255);
                case MessageType.System: return Color.FromArgb(240, 240, 240);
                default: return Color.White;
            }
        }

        private Color GetForeColorForType(MessageType type)
        {
            switch (type)
            {
                case MessageType.User: return Color.DarkBlue;
                case MessageType.System: return Color.DarkGray;
                default: return Color.Black;
            }
        }

        private void ShowResults()
        {
            if (_results == null) return;

            var resultsForm = new ResultsForm(_results);
            resultsForm.Show();
        }

        public void AddExecuteQueryButton(string query, Action<string> executeAction)
        {
            var btnExecute = new Button
            {
                Text = "Execute This Query",
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 30),
                Margin = new Padding(10, 5, 10, 10)
            };
            
            btnExecute.FlatAppearance.BorderSize = 0;
            btnExecute.Click += (s, e) => executeAction?.Invoke(query);
            
            // Add the button to the message panel
            this.Controls.Add(btnExecute);
            btnExecute.Dock = DockStyle.Bottom;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            OnMessageLayout(this, new LayoutEventArgs(this, "Width"));
        }
    }
}