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

namespace Day15TravelsEF
{
    /// <summary>
    /// Interaction logic for AddEditTrain.xaml
    /// </summary>
    public partial class AddEditTrain : Window
    {
        Train editingTrain;

        public AddEditTrain(MainWindow owner, Train train)
        {
            InitializeComponent();
            Owner = owner;
            editingTrain = train;
        }
    }
}
