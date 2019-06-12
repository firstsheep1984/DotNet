using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04Generics
{

    class Box<T>
    {
        public T value;
    }

    class BoxTwoCompartments<M,N>
    {
        public M compA;
        public N compB;

        public M getCompA() {
            M val = compA;
            return val;
        }

        public void setCompA(M val)
        {
            compA = val;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Box<string> boxOfString = new Box<string>();
            boxOfString.value = "abc";
            Console.WriteLine("Value is: " + boxOfString.value);

            BoxTwoCompartments<double, string> twoComp = new BoxTwoCompartments<double, string>();
            twoComp.compA = 234.343;
            twoComp.compB = "Jimmy";

        }
    }
}
