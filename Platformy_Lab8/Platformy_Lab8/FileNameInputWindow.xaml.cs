using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Platformy_Lab8
{
    public partial class FileNameInputWindow : Window
    {
        public string FileName { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsArchive { get; set; }
        public bool IsSystem { get; set; }
        public bool IsHidden { get; set; }
        public bool IsFilePath { get; set; }

        private string selectedPath;

        private TreeViewItem parentItem;

        public FileNameInputWindow(string selectedPath, TreeViewItem parentItem)
        {
            InitializeComponent();
            this.selectedPath = selectedPath;
            this.parentItem = parentItem;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = fileNameTextBox.Text;
            bool isReadOnly = readOnlyCheckBox.IsChecked ?? false;
            bool isArchive = archiveCheckBox.IsChecked ?? false;
            bool isSystem = systemCheckBox.IsChecked ?? false;
            bool isHidden = hiddenCheckBox.IsChecked ?? false;
            bool isFilePath = pathRadioButton.IsChecked ?? false;

            if (!ValidateFileName(fileName) && !isFilePath)
            {
                System.Windows.Forms.MessageBox.Show("Invalid file name!");
                return;
            }

            if (isFilePath)
            {
                string path = Path.Combine(selectedPath, fileName);
                try
                {
                    Directory.CreateDirectory(path);
                    System.Windows.Forms.MessageBox.Show("Directory created successfully.");
                    (System.Windows.Application.Current.MainWindow as MainWindow).UpdateTreeView(path, parentItem);
                    Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"Error occured!");
                }
            }
            else
            {
                string filePath = Path.Combine(selectedPath, fileName);
                try
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        if (isReadOnly)
                            File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.ReadOnly);
                        if (isArchive)
                            File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Archive);
                        if (isSystem)
                            File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.System);
                        if (isHidden)
                            File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
                    }

                    System.Windows.Forms.MessageBox.Show("File created successfully.");
                    (System.Windows.Application.Current.MainWindow as MainWindow).UpdateTreeView(filePath, parentItem);
                    Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"Error occured!");
                }
            }
        }

            private bool ValidateFileName(string fileName)
        {
            string pattern = @"^[a-zA-Z0-9_~.-]{1,8}\.(txt|php|html)$";
            return Regex.IsMatch(fileName, pattern);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
