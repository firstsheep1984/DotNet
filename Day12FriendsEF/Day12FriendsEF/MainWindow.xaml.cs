using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Day12FriendsEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Globals.ctx = new CompanionDBContext();
                lvFriends.ItemsSource = (from f in Globals.ctx.Friends select f).ToList<Friend>();
            } catch (Exception ex)
            {
                MessageBox.Show("Fatal error: " + ex.Message);
                Close();
            }
        }
        private void AddUpdateFriend_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Boolean isUpdating = (button.Name == "btUpdateFriend");

            string name = tbName.Text;
            string ageStr = tbAge.Text;
            if (!int.TryParse(ageStr, out int age))
            {
                MessageBox.Show("Age must be an integer");
                return;
            }
            try
            {
                if (isUpdating)
                {
                    Friend friend = lvFriends.SelectedItem as Friend;
                    if (friend == null) return; // should never happen - internal error
                    friend.Name = name;
                    friend.Age = age;
                    // save changes made in the previous 2 lines to a managed object
                    Globals.ctx.SaveChanges();
                }
                else
                { // adding
                    Friend friend = new Friend() { Name = name, Age = age };
                    Globals.ctx.Friends.Add(friend);
                    Globals.ctx.SaveChanges(); // can throw Data/System Exception
                }
                lblId.Content = "-";
                tbName.Text = "";
                tbAge.Text = "";
                lvFriends.ItemsSource = (from f in Globals.ctx.Friends select f).ToList<Friend>();
            }
            catch (DataException ex)
            { // TODO: make message box nicer
                MessageBox.Show("Database error:\n" + ex.Message);
            }
            catch (SystemException ex)
            { // TODO: make message box nicer
                MessageBox.Show("Database error:\n" + ex.Message);
            }
        }
        private void DeleteFriend_ButtonClick(object sender, RoutedEventArgs e)
        {
            Friend friend = lvFriends.SelectedItem as Friend;
            if (friend == null) return; // should never happen
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?\n" + friend, Globals.AppName, MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    Globals.ctx.Friends.Remove(friend); // schedule for deletion
                    Globals.ctx.SaveChanges();
                    lvFriends.ItemsSource = (from f in Globals.ctx.Friends select f).ToList<Friend>();
                }
                catch (DataException ex)
                { // TODO: make message box nicer
                    MessageBox.Show("Database error:\n" + ex.Message);
                }
                catch (SystemException ex)
                { // TODO: make message box nicer
                    MessageBox.Show("Database error:\n" + ex.Message);
                }
            }
        }

        private void LvFriends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Friend friend = lvFriends.SelectedItem as Friend;
            if (friend == null)
            {
                // disable update and delete buttons
                btUpdateFriend.IsEnabled = false;
                btDeleteFriend.IsEnabled = false;
                return;
            }
            // enable update and delete buttons, load data
            btUpdateFriend.IsEnabled = true;
            btDeleteFriend.IsEnabled = true;
            lblId.Content = friend.Id;
            tbName.Text = friend.Name;
            tbAge.Text = friend.Age + "";
        }
        private void FileExportSelected_MenuClick(object sender, RoutedEventArgs e)
        {
            var selectedItemsCollection = lvFriends.SelectedItems;
            if (selectedItemsCollection.Count == 0)
            { // TODO: make MB nicer
                MessageBox.Show("Select some records first");
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text file (*.txt)|*.txt|Any file (*.*)|*.*";
            sfd.ShowDialog();
            if (sfd.FileName != "")
            {
                List<string> linesList = new List<string>();
                foreach (var item in selectedItemsCollection)
                {
                    Friend f = item as Friend;
                    linesList.Add($"{f.Id};{f.Name};{f.Age}");
                }
                try
                {
                    File.WriteAllLines(sfd.FileName, linesList);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error saving to file:\n" + ex.Message);
                }
            }

        }
    }
}