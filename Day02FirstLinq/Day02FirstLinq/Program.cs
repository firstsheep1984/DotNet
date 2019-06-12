using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02FirstLinq
{
    class Program
    {
        static List<string> NamesList = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();
                    if (name == "")
                    {
                        break;
                    }
                    NamesList.Add(name);
                }
                //
                Console.Write("Enter search string: ");
                string search = Console.ReadLine().ToUpper();
                //
                // var foundList = from n in namesList where n.ToUpper().Contains(search) select n;
                var foundList = NamesList.Where(n => n.ToUpper().Contains(search));
                Console.WriteLine("Matching names:");
                foreach (string name in foundList)
                {
                    Console.WriteLine(name);
                }
                // TASK: sort names alphabetically using LINQ and print them out one per line
                // NamesList.Sort(); good but NOT LINQ
                var sortedList = from n in NamesList orderby n select n;
                // var sortedList = NamesList.OrderBy(item => item);
                Console.WriteLine("Sorted names:");
                foreach (string name in sortedList)
                {
                    Console.WriteLine(name);
                }
            } finally
            {
                Console.WriteLine("Press a key to finish");
                Console.ReadKey();
            }
        }
    }
}
