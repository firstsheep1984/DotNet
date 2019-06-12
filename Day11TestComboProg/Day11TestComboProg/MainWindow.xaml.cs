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

namespace Day11TestComboProg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            

            InitializeComponent();
            comboHours.Items.Clear();
            for (int hour = 0; hour < 24; hour++)
            {
                comboHours.Items.Add($"{hour:00}:00");
                comboHours.Items.Add($"{hour:00}:30");
            }
            comboHours.SelectedValue = "00:00";
        }
    }
}
