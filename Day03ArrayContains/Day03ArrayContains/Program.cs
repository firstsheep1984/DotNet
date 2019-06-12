using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03ArrayContains
{
    class Program
    {

        public static void PrintDups(int[,] a1, int[,] a2) {
            Console.WriteLine("Duplicates: ");
            List<int> uniqueList = new List<int>();
            foreach (int v1 in a1)
            {
                foreach(int v2 in a2)
                {
                    if (v1 == v2)
                    {
                        // Console.Write($"{v1}, ");
                        // only add it to the list if it's not in it already
                        if (!uniqueList.Contains(v1))
                        {
                            uniqueList.Add(v1);
                        } else
                        {
                            Console.WriteLine("Rejected dup: " + v1);
                        }
                    }
                }
            }
            //
            foreach (int v in uniqueList)
            {
                Console.Write($"{v}, ");
            }
        }

        static void PrintTree(int size)
        {
            for (int line = 0; line < size; line++)
            {

                for (int n = 0; n < size - line - 1; n++)
                {
                    Console.Write(" ");
                }
                for (int n = 0; n < line; n++)
                {
                    Console.Write("*");
                }
                for (int n = 0; n < line+1; n++ )
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int[,] data1 = { { 2, 3 }, { 7, 8 }, { 3, 17 } };
            int[,] data2 = { { 64, 3 , 7}, { 1, 8 , 11} };
            PrintDups(data1, data2);
            Console.WriteLine();
            Console.WriteLine();
            PrintTree(4);

            Console.ReadKey();
        }
    }
}
