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

namespace Day09CustomDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Globals.FriendsList.Add(new Friend() { Name = "Jerry", Age = 33 });
            Globals.FriendsList.Add(new Friend() { Name = "Maria", Age = 44 });
            Globals.FriendsList.Add(new Friend() { Name = "Timothy", Age = 21 });
            lvFriends.ItemsSource = Globals.FriendsList;
        }

        private void AddFriend_MenuClick(object sender, RoutedEventArgs e)
        {
            AddEditDialog dialog = new AddEditDialog(this);
            if (dialog.ShowDialog() == true)
            { // only refresh if data changed
                lvFriends.Items.Refresh();
            }
        }

        private void LvFriends_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Friend friend = lvFriends.SelectedItem as Friend;
            if (friend == null) return;
            AddEditDialog dialog = new AddEditDialog(this, friend);
            if (dialog.ShowDialog() == true)
            { // only refresh if data changed
                lvFriends.Items.Refresh();
            }
        }
    }
}
