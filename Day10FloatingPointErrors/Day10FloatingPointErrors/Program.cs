using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10FloatingPointErrors
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.0 - 0.1*10
            double val = 1.0;
            for (int i=0; i<10; i++)
            {
                val -= 0.1;
            }
            Console.WriteLine(val);
            Console.ReadKey();
        }
    }
}
