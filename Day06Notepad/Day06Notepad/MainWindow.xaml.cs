using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day06Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string currOpenFile = "";

        bool isModified;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileExit_MenuClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FileNew_MenuClick(object sender, RoutedEventArgs e)
        {
            if (!isModified)
            {
                tbEditor.Text = "";
            }
            Window_Closing(sender, null);            
        }

        private void FileOpen_MenuClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    tbEditor.Text = File.ReadAllText(openFileDialog.FileName);
                    sbStatus.Text = openFileDialog.FileName;
                    currOpenFile = openFileDialog.FileName;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading file:\n" + ex.Message,
                    "SharpNotepad", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FileSave_MenuClick(object sender, RoutedEventArgs e)
        {
            // TODO: handle the special case when empty file is not modified and File->Save is used
            if (currOpenFile != "")
            {
                File.WriteAllText(currOpenFile, tbEditor.Text);
                isModified = false;
            }
            else
            {
                FileSaveAs_MenuClick(sender, e);
            }
        }

        private void FileSaveAs_MenuClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, tbEditor.Text);
                    isModified = false;
                    currOpenFile = saveFileDialog.FileName;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error writing to file:\n" + ex.Message,
                    "SharpNotepad", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            isModified = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isModified)
            {
                return;
            }
            MessageBoxResult result = MessageBox.Show("Would you like to save unsaved changes?", "Sharp Notepad", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.No:
                    if (e == null)
                    { // came from File->New
                        tbEditor.Text = "";
                        isModified = false;
                    }
                    break;
                case MessageBoxResult.Cancel:
                    if (e != null)
                    {
                        e.Cancel = true; // prevent window from closing
                    }
                    break;
                case MessageBoxResult.Yes:
                    FileSave_MenuClick(sender, null);
                    // TODO: If file was saved then we should tbEditor.Text = "";
                    if (!isModified && e == null)
                    {
                        tbEditor.Text = "";
                    }
                    break;
                default:
                    MessageBox.Show("Internal error - unknown choice");
                    break;
            }

        }
    }
}
