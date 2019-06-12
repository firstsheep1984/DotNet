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

namespace Day10CustomSandwich
{
    /// <summary>
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {
        MainWindow mw;

        public CustomDialog(MainWindow owner)
        {
            InitializeComponent();
            mw = owner;
            Owner = owner;
            // TODO: Load values from MainWindow - maybe
        }

        private void Save_ButtonClick(object sender, RoutedEventArgs e)
        {
            mw.lblBread.Content = comboBread.Text;
            // veggies
            List<string> veggiesList = new List<string>();
            if (cbVegLettuce.IsChecked == true)
            {
                veggiesList.Add("Lettuce");
            }
            if (cbVegTomatoes.IsChecked == true)
            {
                veggiesList.Add("Tomatoes");
            }
            if (cbVegCucumbers.IsChecked == true)
            {
                veggiesList.Add("Cucumbers");
            }
            mw.lblVeggies.Content = string.Join(", ", veggiesList);
            //
            mw.lblMeat.Content = rbMeatChicken.IsChecked == true ? "Chicken" : (rbMeatTurkey.IsChecked == true ? "Turkey" : "Tofu");
            //
            DialogResult = true; // close the window, success
        }
    }
}
