using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EMtools
{
    public partial class RecycleBinCleanerForm : Form
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        [Flags]
        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
        }

        private Label statusLabel;
        private Button cleanButton;
        private Button refreshButton;
        private Label infoLabel;
        private ProgressBar progressBar;

        public RecycleBinCleanerForm()
        {
            InitializeComponent();
            this.Text = "Recycle Bin Cleaner";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(500, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            InitializeControls();
            RefreshRecycleBinInfo();
        }

        private void InitializeControls()
        {
            Label titleLabel = new Label
            {
                Text = "Recycle Bin Cleaner",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            this.Controls.Add(titleLabel);

            infoLabel = new Label
            {
                Text = "Checking recycle bin...",
                Font = new Font("Segoe UI", 10),
                AutoSize = false,
                Size = new Size(420, 80),
                Location = new Point(30, 60)
            };
            this.Controls.Add(infoLabel);

            cleanButton = new Button
            {
                Text = "🗑️ Empty Recycle Bin (Instant)",
                Size = new Size(420, 50),
                Location = new Point(30, 150),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            cleanButton.FlatAppearance.BorderSize = 0;
            cleanButton.Click += CleanButton_Click;
            this.Controls.Add(cleanButton);

            refreshButton = new Button
            {
                Text = "🔄 Refresh Info",
                Size = new Size(150, 40),
                Location = new Point(30, 210),
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            refreshButton.Click += (s, e) => RefreshRecycleBinInfo();
            this.Controls.Add(refreshButton);

            statusLabel = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9),
                AutoSize = false,
                Size = new Size(420, 20),
                Location = new Point(30, 255),
                ForeColor = Color.Green
            };
            this.Controls.Add(statusLabel);

            Button closeButton = new Button
            {
                Text = "Close",
                Size = new Size(100, 40),
                Location = new Point(350, 260),
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            closeButton.Click += (s, e) => this.Close();
            this.Controls.Add(closeButton);
        }

        private void RefreshRecycleBinInfo()
        {
            try
            {
                string recycleBinPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\$Recycle.Bin";
                long totalSize = 0;
                int fileCount = 0;

                if (Directory.Exists(recycleBinPath))
                {
                    try
                    {
                        var directories = Directory.GetDirectories(recycleBinPath);
                        foreach (var dir in directories)
                        {
                            var files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
                            fileCount += files.Length;
                            foreach (var file in files)
                            {
                                try
                                {
                                    FileInfo fi = new FileInfo(file);
                                    totalSize += fi.Length;
                                }
                                catch { }
                            }
                        }
                    }
                    catch { }
                }

                string sizeStr = FormatBytes(totalSize);
                infoLabel.Text = $"Recycle Bin Status:\n\nFiles: {fileCount}\nTotal Size: {sizeStr}\n\nClick 'Empty Recycle Bin' to clean instantly!";
                
                if (fileCount == 0)
                {
                    cleanButton.Enabled = false;
                    cleanButton.Text = "✓ Recycle Bin is Empty";
                }
                else
                {
                    cleanButton.Enabled = true;
                    cleanButton.Text = "🗑️ Empty Recycle Bin (Instant)";
                }
            }
            catch (Exception ex)
            {
                infoLabel.Text = $"Error checking recycle bin:\n{ex.Message}";
            }
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to permanently empty the Recycle Bin?\n\nThis action cannot be undone!",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    statusLabel.Text = "Emptying recycle bin...";
                    statusLabel.ForeColor = Color.Orange;
                    Application.DoEvents();

                    uint success = SHEmptyRecycleBin(
                        IntPtr.Zero,
                        null,
                        RecycleFlags.SHERB_NOCONFIRMATION | RecycleFlags.SHERB_NOPROGRESSUI | RecycleFlags.SHERB_NOSOUND);

                    if (success == 0)
                    {
                        statusLabel.Text = "✓ Recycle Bin emptied successfully!";
                        statusLabel.ForeColor = Color.Green;
                        MessageBox.Show("Recycle Bin has been emptied successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        statusLabel.Text = "⚠ Operation completed with warnings";
                        statusLabel.ForeColor = Color.Orange;
                    }

                    RefreshRecycleBinInfo();
                }
                catch (Exception ex)
                {
                    statusLabel.Text = "✗ Error emptying recycle bin";
                    statusLabel.ForeColor = Color.Red;
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
