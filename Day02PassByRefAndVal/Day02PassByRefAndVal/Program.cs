using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02PassByRefAndVal
{
    public class Box
    {
        public int vvv;
    }

    class Program
    {

        static void ModifyIntAndBox(int m, Box b)
        {
            Console.WriteLine($"In1: m={m}, b.vvv={b.vvv}");
            m++;
            b.vvv++;
            b = new Box();
            b.vvv = 123;
            Console.WriteLine($"In2: m={m}, b.vvv={b.vvv}");
        }

        static void ModifyThisViaRef(ref int a)
        {
            Console.WriteLine("Btw. a is " + a);
            a++;           
        }

        static void WriteToOutVar(out int b)
        {
            //Console.WriteLine("Btw. b is " + b); // error, b is undefined
            b = 11;
            //Console.WriteLine("Btw. b is " + b); // okay after assignment
        }

        static void Main(string[] args)
        {
            int eee = 55;            
            Box box = new Box() { vvv = 77 };

            Console.WriteLine($"At0: eee={eee}, box.vvv={box.vvv}");
            ModifyIntAndBox(eee, box);
            Console.WriteLine($"At3: eee={eee}, box.vvv={box.vvv}");
            /*
            int x = 5;
            ModifyThisViaRef(ref x);
            Console.WriteLine("Result: " + x);

            Console.WriteLine("Enter an integer value");
            string input = Console.ReadLine();
            int number;
            if (!int.TryParse(input, out number))
            {
                Console.WriteLine("Parsing error");
            }*/

            Console.ReadKey();
        }
    }
}
