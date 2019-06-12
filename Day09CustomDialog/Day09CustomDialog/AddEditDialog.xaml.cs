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
using System.Windows.Shapes;

namespace Day09CustomDialog
{
    /// <summary>
    /// Interaction logic for AddEditDialog.xaml
    /// </summary>
    public partial class AddEditDialog : Window
    {
        Friend currentlyEditedFriend;

        public AddEditDialog(Window owner, Friend friend = null)
        {
            Owner = owner;
            currentlyEditedFriend = friend;
            InitializeComponent();
            btAddUpdate.Content = friend == null ? "Add friend" : "Update friend";
            if (friend != null)
            {
                tbName.Text = friend.Name;
                tbAge.Text = friend.Age + "";
            }
        }

        private void AddUpdate_ButtonClick(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            int age = int.Parse(tbAge.Text); // FIXME: tryParse instead
            if (currentlyEditedFriend != null)
            { // update
                currentlyEditedFriend.Name = name;
                currentlyEditedFriend.Age = age;
            }
            else
            { // add
                Friend friend = new Friend() { Name = name, Age = age };
                Globals.FriendsList.Add(friend);
            }
            DialogResult = true; // Close dialog with success result
        }
    }
}
