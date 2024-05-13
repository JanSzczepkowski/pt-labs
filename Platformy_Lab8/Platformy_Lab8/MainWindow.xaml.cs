using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Platformy_Lab8
{
    public partial class MainWindow : Window
    {

        private string mainPath;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void UpdateTreeView(string path, TreeViewItem parentItem)
        {
            AddPathToTreeView(path, parentItem);
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select directory to open";

            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string selectedPath = folderBrowserDialog.SelectedPath;

                treeView.Items.Clear();

                mainPath = selectedPath;

                AddPathToTreeView(selectedPath, null);
            }
        }

        private void AddPathToTreeView(string path, TreeViewItem parentItem)
        {
            bool isFile = File.Exists(path);
            bool isDirectory = Directory.Exists(path);

            if (isFile || isDirectory)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = System.IO.Path.GetFileName(path);
                textBlock.Tag = path; 
                textBlock.MouseDown += TextBlock_MouseDown;

                if(isDirectory)
                {
                    textBlock.ContextMenu = CreateContextMenuWithAdd(path);
                } else
                {
                    textBlock.ContextMenu = CreateContextMenu(path);
                }

                Grid grid = new Grid();
                grid.Children.Add(textBlock);
                TreeViewItem item = new TreeViewItem();
                item.Header = grid;
                item.Tag = path;

                if (parentItem == null)
                {
                    treeView.Items.Add(item);
                }
                else
                {
                    parentItem.Items.Add(item);
                }
                if (isDirectory)
                {
                    string[] directories = Directory.GetDirectories(path);
                    foreach (string directory in directories)
                    {
                        AddPathToTreeView(directory, item);
                    }
                    string[] files = Directory.GetFiles(path);
                    foreach (string file in files)
                    {
                        AddPathToTreeView(file, item);
                    }
                }
            }
        }

        private ContextMenu CreateContextMenuWithAdd(string path)
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem addMenuItem = new MenuItem();

            MenuItem deleteMenuItem = new MenuItem();
            deleteMenuItem.Header = "Delete";
            deleteMenuItem.Click += DeleteMenuItem_Click;
            deleteMenuItem.Tag = path;
            contextMenu.Items.Add(deleteMenuItem);

            addMenuItem.Header = "Create";
            addMenuItem.Click += AddMenuItem_Click;
            addMenuItem.Tag = path;
            contextMenu.Items.Add(addMenuItem);

            return contextMenu;
        }
        private ContextMenu CreateContextMenu(string path)
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem deleteMenuItem = new MenuItem();
            deleteMenuItem.Header = "Delete";
            deleteMenuItem.Click += DeleteMenuItem_Click;
            deleteMenuItem.Tag = path; 
            contextMenu.Items.Add(deleteMenuItem);

            MenuItem openMenuItem = new MenuItem();
            openMenuItem.Header = "Open";
            openMenuItem.Click += OpenItemInWindow;
            openMenuItem.Tag = path;
            contextMenu.Items.Add(openMenuItem);
            return contextMenu;
        }

        private void OpenItemInWindow(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                string path = menuItem.Tag as string;
                if (!string.IsNullOrEmpty(path))
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            string fileContent = File.ReadAllText(path);
                            fileContentTextBox.Text = fileContent;
                            fileContentTextBox.Tag = path;
                            fileContentTextBox.IsReadOnly = false;
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show($"Error opening file: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void SaveFileContent(object sender, RoutedEventArgs e)
        {
            string path = fileContentTextBox.Tag as string;
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    File.WriteAllText(path, fileContentTextBox.Text);
                    System.Windows.Forms.MessageBox.Show("File saved successfully.");
                }
                catch (Exception ex)
                {
                    // Obsługa błędu zapisu pliku
                    System.Windows.Forms.MessageBox.Show($"Error saving file: {ex.Message}");
                }
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null)
            {
                string path = textBlock.Tag as string;
                if (!string.IsNullOrEmpty(path))
                {
                    UpdateStatusBar(path);
                }
            }
        }

        private void UpdateStatusBar(string path)
        {
            try
            {
                string statusText = "";

                if (File.Exists(path))
                {
                    FileAttributes fileAttributes = File.GetAttributes(path);

                    if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        statusText += "r";
                    else
                        statusText += "-";

                    if ((fileAttributes & FileAttributes.Archive) == FileAttributes.Archive)
                        statusText += "a";
                    else
                        statusText += "-";

                    if ((fileAttributes & FileAttributes.System) == FileAttributes.System)
                        statusText += "s";
                    else
                        statusText += "-";

                    if ((fileAttributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        statusText += "h";
                    else
                        statusText += "-";
                }
                else if (Directory.Exists(path))
                {
                    FileAttributes directoryAttributes = File.GetAttributes(path);

                    if ((directoryAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        statusText += "r";
                    else
                        statusText += "-";

                    if ((directoryAttributes & FileAttributes.Archive) == FileAttributes.Archive)
                        statusText += "a";
                    else
                        statusText += "-";

                    if ((directoryAttributes & FileAttributes.System) == FileAttributes.System)
                        statusText += "s";
                    else
                        statusText += "-";

                    if ((directoryAttributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        statusText += "h";
                    else
                        statusText += "-";
                }

                statusTextBlock.Text = statusText;
            }
            catch (Exception ex)
            {
                statusTextBlock.Text = "Error: " + ex.Message;
            }
        }


        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedTreeViewItem = treeView.SelectedItem as TreeViewItem;
            string path = selectedTreeViewItem.Tag as string;
            FileNameInputWindow fileNameWindow = new FileNameInputWindow(path, selectedTreeViewItem);
            fileNameWindow.ShowDialog();
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                TreeViewItem selectedTreeViewItem = treeView.SelectedItem as TreeViewItem;
                string path = selectedTreeViewItem.Tag as string;

                try
                {
                    // Usuń pliki i katalogi rekurencyjnie
                    DeleteRecursive(path);

                    // Usuń element z drzewa
                    if (selectedTreeViewItem.Parent is TreeViewItem parentItem)
                    {
                        parentItem.Items.Remove(selectedTreeViewItem);
                    }
                    else
                    {
                        treeView.Items.Remove(selectedTreeViewItem);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Error deleting item: {ex.Message}");
                }
            }
        }

        private void DeleteRecursive(string path)
        {
            if (File.Exists(path))
            {
                // Jeśli plik ma atrybut ReadOnly, usuń go
                if ((File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    FileAttributes attributes = File.GetAttributes(path);
                    attributes &= ~FileAttributes.ReadOnly;
                    File.SetAttributes(path, attributes);
                }
                File.Delete(path);
            }
            else if (Directory.Exists(path))
            {
                // Usuń atrybut ReadOnly dla samej ścieżki
                File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReadOnly);

                // Usuń pliki w katalogu
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    DeleteRecursive(file);
                }

                // Usuń katalogi w katalogu
                string[] subdirectories = Directory.GetDirectories(path);
                foreach (string subdirectory in subdirectories)
                {
                    DeleteRecursive(subdirectory);
                }

                // Na koniec usuń sam katalog
                Directory.Delete(path);
            }
        }




        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
