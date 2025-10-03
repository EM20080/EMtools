using System;
using System.Windows.Forms;

namespace EMtools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "EMtools - Windows Utilities";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(600, 450);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
            InitializeControls();
        }

        private void InitializeControls()
        {
            Label titleLabel = new Label
            {
                Text = "EMtools - Collection of Useful Windows Tools",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(50, 20)
            };
            this.Controls.Add(titleLabel);

            Label descLabel = new Label
            {
                Text = "Select a tool to get started:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(50, 60)
            };
            this.Controls.Add(descLabel);

            // Recycle Bin Cleaner Button
            Button recycleBinButton = new Button
            {
                Text = "🗑️ Recycle Bin Cleaner",
                Size = new Size(480, 80),
                Location = new Point(50, 100),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(66, 135, 245),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            recycleBinButton.FlatAppearance.BorderSize = 0;
            recycleBinButton.Click += (s, e) => OpenRecycleBinCleaner();
            this.Controls.Add(recycleBinButton);

            Label recycleDesc = new Label
            {
                Text = "Instantly clean your recycle bin - faster than Windows default",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(70, 185),
                ForeColor = Color.Gray
            };
            this.Controls.Add(recycleDesc);

            // Batch Tools Button
            Button batchToolsButton = new Button
            {
                Text = "📦 Batch File Tools",
                Size = new Size(480, 80),
                Location = new Point(50, 220),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            batchToolsButton.FlatAppearance.BorderSize = 0;
            batchToolsButton.Click += (s, e) => OpenBatchTools();
            this.Controls.Add(batchToolsButton);

            Label batchDesc = new Label
            {
                Text = "Batch file operations: rename, move, copy multiple files at once",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(70, 305),
                ForeColor = Color.Gray
            };
            this.Controls.Add(batchDesc);

            // Exit Button
            Button exitButton = new Button
            {
                Text = "Exit",
                Size = new Size(100, 40),
                Location = new Point(430, 360),
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            exitButton.Click += (s, e) => Application.Exit();
            this.Controls.Add(exitButton);
        }

        private void OpenRecycleBinCleaner()
        {
            RecycleBinCleanerForm cleanerForm = new RecycleBinCleanerForm();
            cleanerForm.ShowDialog();
        }

        private void OpenBatchTools()
        {
            BatchToolsForm batchForm = new BatchToolsForm();
            batchForm.ShowDialog();
        }
    }
}
