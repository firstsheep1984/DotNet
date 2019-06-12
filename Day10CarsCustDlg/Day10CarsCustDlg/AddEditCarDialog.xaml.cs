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

namespace Day10CarsCustDlg
{
    /// <summary>
    /// Interaction logic for AddEditCarDialog.xaml
    /// </summary>
    public partial class AddEditCarDialog : Window
    {
        public AddEditCarDialog(MainWindow owner)
        {
            InitializeComponent();
            Owner = owner;
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {   // add/update database records
                string makeModel = tbMakeModel.Text;                
                // make sure we save the rounded value: 3.478263874623873 => 3.5
                double esRaw = sliderEngineSize.Value;
                double engineSize = double.Parse($"{esRaw:0.0}");
                // double engineSize = sliderEngineSize.Value;
                //
                FuelType fuelType = (FuelType)Enum.Parse(typeof(FuelType), comboFuelType.Text);
                Globals.Db.AddCar(new Car() { MakeModel = makeModel, EngineSizeL = engineSize, FuelType = fuelType });
                DialogResult = true; // close dialog
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Internal error: unknown fuel type value");
                return;
            }

        }
    }
}
