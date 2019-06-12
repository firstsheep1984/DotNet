using System;
using System.Collections.Generic;
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

namespace Day07FriendsList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Friend> friendsList = new List<Friend>();

        public MainWindow()
        {
            InitializeComponent();
            lvFriends.ItemsSource = friendsList;
            LoadDataFromFile();
        }

        private void AddFriend_ButtonClick(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            int age = int.Parse(tbAge.Text);
            // FIXME: handle exception in setters, show message box on error
            Friend friend = new Friend() { Name = name, Age = age };            
            friendsList.Add(friend);
            lvFriends.Items.Refresh();
        }

        private void SaveDataToFile()
        {
            // TODO
        }

        private void LoadDataFromFile()
        {
            // TODO
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveDataToFile();
        }

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblErrorName.Visibility = Friend.IsNameValid(tbName.Text) ? Visibility.Hidden : Visibility.Visible;

            /*
            if (Friend.IsNameValid(tbName.Text))
            {
                lblErrorName.Visibility = Visibility.Visible;
            } else
            {
                lblErrorName.Visibility = Visibility.Hidden;
            }*/

        }

        private void TbAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblErrorAge.Visibility = Friend.IsAgeValid(tbAge.Text) ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
