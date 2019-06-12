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

namespace Day10CarsCustDlg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.Db = new Database();
                lvCars.ItemsSource = Globals.Db.GetAllCars();
            }
            catch (SystemException ex) //(SqlException ex)
            { // TODO: make message box nicer
                MessageBox.Show("Database error:\n" + ex.Message);
                Close(); // Fatal error - exit application
            }
        }

        private void AddCar_MenuClick(object sender, RoutedEventArgs e)
        {
            AddEditCarDialog dialog = new AddEditCarDialog(this);
            if (dialog.ShowDialog() == true)
            {
                lvCars.ItemsSource = Globals.Db.GetAllCars();
            }
        }
    }
}
