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

namespace Day07ConvertAgain
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

        private void CalculateResult()
        {
            if (!double.TryParse(tbInputVal.Text, out double inputVal))
            { // show error message / label
                lblError.Visibility = Visibility.Visible;
            }
            else
            { // hide error message / label
                lblError.Visibility = Visibility.Hidden;
            }
            //
            double inputCel;
            if (rbFromCel.IsChecked == true)
            {
                inputCel = inputVal;
            }
            else if (rbFromFah.IsChecked == true)
            {
                inputCel = (inputVal - 32) * 5 / 9;
            }
            else if (rbFromKel.IsChecked == true)
            {
                inputCel = inputVal - 273.15;
            }
            else
            {
                MessageBox.Show("Internal error: Unknown from scale");
                return;
            }
            //
            double outputVal;
            string unit;
            if (rbToCel.IsChecked == true)
            {
                unit = "C";
                outputVal = inputCel;
            }
            else if (rbToFah.IsChecked == true)
            {
                unit = "F";
                outputVal = inputCel * 9 / 5 + 32;
            }
            else if (rbToKel.IsChecked == true)
            {
                unit = "K";
                outputVal = inputCel + 273.15;
            }
            else
            {
                MessageBox.Show("Internal error: Unknown to scale");
                return;
            }
            //
            tbOutputVal.Text = $"{outputVal:0.###}°{unit}";
        }

        private void TbInputVal_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateResult();
        }

        private void SelectionChanged_Click(object sender, RoutedEventArgs e)
        {
            CalculateResult();
        }
    }
}
