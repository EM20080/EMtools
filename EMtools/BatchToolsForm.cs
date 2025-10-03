using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EMtools
{
    public partial class BatchToolsForm : Form
    {
        private ListBox fileListBox;
        private Button addFilesButton;
        private Button addFolderButton;
        private Button clearButton;
        private TabControl tabControl;
        private Label statusLabel;

        public BatchToolsForm()
        {
            InitializeComponent();
            this.Text = "Batch File Tools";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(700, 550);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            InitializeControls();
        }

        private void InitializeControls()
        {
            Label titleLabel = new Label
            {
                Text = "Batch File Tools",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            this.Controls.Add(titleLabel);

            // File list section
            Label fileListLabel = new Label
            {
                Text = "Selected Files:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 60)
            };
            this.Controls.Add(fileListLabel);

            fileListBox = new ListBox
            {
                Size = new Size(620, 150),
                Location = new Point(30, 85),
                Font = new Font("Consolas", 9)
            };
            this.Controls.Add(fileListBox);

            // Buttons for file selection
            addFilesButton = new Button
            {
                Text = "➕ Add Files",
                Size = new Size(120, 35),
                Location = new Point(30, 245),
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            addFilesButton.Click += AddFilesButton_Click;
            this.Controls.Add(addFilesButton);

            addFolderButton = new Button
            {
                Text = "📁 Add Folder",
                Size = new Size(120, 35),
                Location = new Point(160, 245),
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            addFolderButton.Click += AddFolderButton_Click;
            this.Controls.Add(addFolderButton);

            clearButton = new Button
            {
                Text = "🗑️ Clear List",
                Size = new Size(120, 35),
                Location = new Point(290, 245),
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            clearButton.Click += (s, e) => fileListBox.Items.Clear();
            this.Controls.Add(clearButton);

            // Tab control for different operations
            tabControl = new TabControl
            {
                Size = new Size(620, 170),
                Location = new Point(30, 290),
                Font = new Font("Segoe UI", 9)
            };
            
            InitializeRenameTab();
            InitializeCopyMoveTab();
            InitializeDeleteTab();
            
            this.Controls.Add(tabControl);

            // Status label
            statusLabel = new Label
            {
                Text = "Ready",
                Font = new Font("Segoe UI", 9),
                AutoSize = false,
                Size = new Size(500, 20),
                Location = new Point(30, 470),
                ForeColor = Color.Green
            };
            this.Controls.Add(statusLabel);

            Button closeButton = new Button
            {
                Text = "Close",
                Size = new Size(100, 35),
                Location = new Point(550, 465),
                Font = new Font("Segoe UI", 10),
                FlatStyle = FlatStyle.Flat
            };
            closeButton.Click += (s, e) => this.Close();
            this.Controls.Add(closeButton);
        }

        private void InitializeRenameTab()
        {
            TabPage renamePage = new TabPage("Batch Rename");
            
            Label prefixLabel = new Label
            {
                Text = "Prefix:",
                Location = new Point(10, 15),
                AutoSize = true
            };
            renamePage.Controls.Add(prefixLabel);

            TextBox prefixBox = new TextBox
            {
                Location = new Point(80, 12),
                Size = new Size(150, 25)
            };
            renamePage.Controls.Add(prefixBox);

            Label suffixLabel = new Label
            {
                Text = "Suffix:",
                Location = new Point(250, 15),
                AutoSize = true
            };
            renamePage.Controls.Add(suffixLabel);

            TextBox suffixBox = new TextBox
            {
                Location = new Point(310, 12),
                Size = new Size(150, 25)
            };
            renamePage.Controls.Add(suffixBox);

            CheckBox numberCheckBox = new CheckBox
            {
                Text = "Add numbering (001, 002, ...)",
                Location = new Point(10, 45),
                AutoSize = true
            };
            renamePage.Controls.Add(numberCheckBox);

            Button renameButton = new Button
            {
                Text = "Rename Files",
                Size = new Size(150, 40),
                Location = new Point(10, 75),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            renameButton.FlatAppearance.BorderSize = 0;
            renameButton.Click += (s, e) => BatchRename(prefixBox.Text, suffixBox.Text, numberCheckBox.Checked);
            renamePage.Controls.Add(renameButton);

            tabControl.TabPages.Add(renamePage);
        }

        private void InitializeCopyMoveTab()
        {
            TabPage copyMovePage = new TabPage("Copy/Move");
            
            Label destLabel = new Label
            {
                Text = "Destination Folder:",
                Location = new Point(10, 15),
                AutoSize = true
            };
            copyMovePage.Controls.Add(destLabel);

            TextBox destBox = new TextBox
            {
                Location = new Point(10, 40),
                Size = new Size(450, 25),
                ReadOnly = true
            };
            copyMovePage.Controls.Add(destBox);

            Button browseButton = new Button
            {
                Text = "Browse...",
                Size = new Size(100, 25),
                Location = new Point(470, 40)
            };
            browseButton.Click += (s, e) =>
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        destBox.Text = dialog.SelectedPath;
                    }
                }
            };
            copyMovePage.Controls.Add(browseButton);

            Button copyButton = new Button
            {
                Text = "Copy Files",
                Size = new Size(120, 40),
                Location = new Point(10, 75),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            copyButton.FlatAppearance.BorderSize = 0;
            copyButton.Click += (s, e) => BatchCopyMove(destBox.Text, false);
            copyMovePage.Controls.Add(copyButton);

            Button moveButton = new Button
            {
                Text = "Move Files",
                Size = new Size(120, 40),
                Location = new Point(140, 75),
                BackColor = Color.FromArgb(156, 39, 176),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            moveButton.FlatAppearance.BorderSize = 0;
            moveButton.Click += (s, e) => BatchCopyMove(destBox.Text, true);
            copyMovePage.Controls.Add(moveButton);

            tabControl.TabPages.Add(copyMovePage);
        }

        private void InitializeDeleteTab()
        {
            TabPage deletePage = new TabPage("Delete");
            
            Label warningLabel = new Label
            {
                Text = "⚠️ Warning: This will permanently delete all selected files!",
                Location = new Point(10, 15),
                AutoSize = true,
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            deletePage.Controls.Add(warningLabel);

            Button deleteButton = new Button
            {
                Text = "Delete Files",
                Size = new Size(150, 40),
                Location = new Point(10, 50),
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.Click += (s, e) => BatchDelete();
            deletePage.Controls.Add(deleteButton);

            tabControl.TabPages.Add(deletePage);
        }

        private void AddFilesButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                dialog.Title = "Select Files";
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in dialog.FileNames)
                    {
                        if (!fileListBox.Items.Contains(file))
                        {
                            fileListBox.Items.Add(file);
                        }
                    }
                    statusLabel.Text = $"Added {dialog.FileNames.Length} file(s). Total: {fileListBox.Items.Count}";
                }
            }
        }

        private void AddFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select Folder";
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] files = Directory.GetFiles(dialog.SelectedPath, "*.*", SearchOption.AllDirectories);
                        int addedCount = 0;
                        foreach (string file in files)
                        {
                            if (!fileListBox.Items.Contains(file))
                            {
                                fileListBox.Items.Add(file);
                                addedCount++;
                            }
                        }
                        statusLabel.Text = $"Added {addedCount} file(s) from folder. Total: {fileListBox.Items.Count}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BatchRename(string prefix, string suffix, bool addNumbers)
        {
            if (fileListBox.Items.Count == 0)
            {
                MessageBox.Show("Please add files first!", "No Files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Rename {fileListBox.Items.Count} file(s)?\n\nPrefix: {prefix}\nSuffix: {suffix}\nNumbering: {addNumbers}",
                "Confirm Rename",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int successCount = 0;
                int failCount = 0;
                int index = 1;

                List<string> files = fileListBox.Items.Cast<string>().ToList();

                foreach (string file in files)
                {
                    try
                    {
                        string directory = Path.GetDirectoryName(file);
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string extension = Path.GetExtension(file);
                        
                        string newName = prefix + fileName + suffix;
                        if (addNumbers)
                        {
                            newName += $"_{index:D3}";
                        }
                        newName += extension;

                        string newPath = Path.Combine(directory, newName);
                        
                        if (!File.Exists(newPath))
                        {
                            File.Move(file, newPath);
                            successCount++;
                        }
                        else
                        {
                            failCount++;
                        }
                        
                        index++;
                    }
                    catch
                    {
                        failCount++;
                    }
                }

                statusLabel.Text = $"Rename completed: {successCount} success, {failCount} failed";
                statusLabel.ForeColor = failCount > 0 ? Color.Orange : Color.Green;
                MessageBox.Show($"Rename completed!\n\nSuccess: {successCount}\nFailed: {failCount}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                fileListBox.Items.Clear();
            }
        }

        private void BatchCopyMove(string destination, bool move)
        {
            if (fileListBox.Items.Count == 0)
            {
                MessageBox.Show("Please add files first!", "No Files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(destination))
            {
                MessageBox.Show("Please select a destination folder!", "No Destination", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string operation = move ? "Move" : "Copy";
            DialogResult result = MessageBox.Show(
                $"{operation} {fileListBox.Items.Count} file(s) to:\n{destination}?",
                $"Confirm {operation}",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int successCount = 0;
                int failCount = 0;

                foreach (string file in fileListBox.Items)
                {
                    try
                    {
                        string fileName = Path.GetFileName(file);
                        string destPath = Path.Combine(destination, fileName);

                        if (move)
                        {
                            File.Move(file, destPath);
                        }
                        else
                        {
                            File.Copy(file, destPath, true);
                        }
                        successCount++;
                    }
                    catch
                    {
                        failCount++;
                    }
                }

                statusLabel.Text = $"{operation} completed: {successCount} success, {failCount} failed";
                statusLabel.ForeColor = failCount > 0 ? Color.Orange : Color.Green;
                MessageBox.Show($"{operation} completed!\n\nSuccess: {successCount}\nFailed: {failCount}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (move)
                {
                    fileListBox.Items.Clear();
                }
            }
        }

        private void BatchDelete()
        {
            if (fileListBox.Items.Count == 0)
            {
                MessageBox.Show("Please add files first!", "No Files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to DELETE {fileListBox.Items.Count} file(s)?\n\nThis action cannot be undone!",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int successCount = 0;
                int failCount = 0;

                foreach (string file in fileListBox.Items)
                {
                    try
                    {
                        File.Delete(file);
                        successCount++;
                    }
                    catch
                    {
                        failCount++;
                    }
                }

                statusLabel.Text = $"Delete completed: {successCount} success, {failCount} failed";
                statusLabel.ForeColor = failCount > 0 ? Color.Orange : Color.Green;
                MessageBox.Show($"Delete completed!\n\nSuccess: {successCount}\nFailed: {failCount}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                fileListBox.Items.Clear();
            }
        }
    }
}
