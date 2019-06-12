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

namespace Day07IceCreamSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FlavAdd_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (lvFlavAvailable.SelectedIndex == -1)
            {
                lblErrorAvailable.Visibility = Visibility.Visible;
                return;
            }
            var selectedItemsCollection = lvFlavAvailable.SelectedItems;
            foreach (object item in selectedItemsCollection)
            {
                ListViewItem lvi = item as ListViewItem;
                lvFlavSelected.Items.Add(lvi.Content); // add string to right list
            }

            /* // SINGLE SELECTION HANDLING:
            ListViewItem item = lvFlavAvailable.SelectedItem as ListViewItem;
            Console.WriteLine("Item selected is: " + item);
            lvFlavSelected.Items.Add(item.Content); // add string to right list
            */

            /* // create new ListViewItem and put string into it, then add to right list
            ListViewItem newItem = new ListViewItem();
            newItem.Content = item.Content;
            lvFlavSelected.Items.Add(newItem); */
        }

        private void RemoveSelected_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (lvFlavSelected.SelectedIndex == -1)
            {
                lblErrorSelected.Visibility = Visibility.Visible;
                return;
            }
            lvFlavSelected.Items.RemoveAt(lvFlavSelected.SelectedIndex);
        }

        private void RemoveAll_ButtonClick(object sender, RoutedEventArgs e)
        {
            lvFlavSelected.Items.Clear();
            lblErrorSelected.Visibility = Visibility.Hidden;
        }

        private void LvFlavAvailable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblErrorAvailable.Visibility = Visibility.Hidden;
        }

        private void LvFlavSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblErrorSelected.Visibility = Visibility.Hidden;
        }
    }
}
