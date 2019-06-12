using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10CarsCustDlg
{
    public class Car
    {
        public int Id { get; set; }
        public string MakeModel { get; set; }
        public double EngineSizeL { get; set; }
        public FuelType FuelType { get; set; }
    }

    public enum FuelType { Gasoline, Diesel, Hybrid, Electric, Propane, Alcohol }
}
