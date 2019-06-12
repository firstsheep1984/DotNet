using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01PeopleListInFile
{
    public enum SocialRole
    {
        Parent, // 0
        Sibiling, // 1
        DistantRelative // 2
    }

    public class Person
    {
        private string _name;
        public string Name // Name 2-100 characters long, not containing semicolons
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length < 2 || value.Length > 100 || value.Contains(";"))
                {
                    throw new ArgumentException("Name must be 2-100 characters long, no semicolons");
                }
                _name = value;
            }
        }

        private int _age;
        public int Age // Age 0-150
        {
            get
            {
                return _age;
            }
            set
            {
                if (value < 0 || value > 150)
                {
                    throw new ArgumentException("Age must be 0-150");
                }
                _age = value;
            }
        }

        private string _city;
        public string City // City 2-100 characters long, not containing semicolons
        {
            get
            {
                return _city;
            }
            set
            {
                if (value.Length < 2 || value.Length > 100 || value.Contains(";"))
                {
                    throw new ArgumentException("City must be 2-100 characters long, no semicolons");
                }
                _city = value;
            }
        }

        public SocialRole Role;

        public override string ToString()
        {
            return $"{Name} is {Age} from {City}";
        }

        // serialization of data / marchalling
        public string ToDataString()
        {
            return $"{Name};{Age};{City}";
        }
    }

    class Program
    {
        const string DataFileName = @"..\..\people.txt";

        static List<Person> peopleList = new List<Person>();

        static void AddPersonInfo()
        {
            Console.WriteLine("Adding a person.");
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter age: ");
            string ageStr = Console.ReadLine();
            if (!int.TryParse(ageStr, out int age))
            {
                Console.WriteLine("Age must be a valid integer");
                return;
            }
            Console.Write("Enter city: ");
            string city = Console.ReadLine();
            //
            try
            {
                Person p = new Person() { Name = name, Age = age, City = city };
                peopleList.Add(p);
                Console.WriteLine("Person added.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Person data invalid: " + ex.Message);
            }
        }
        static void ListAllPersonsInfo()
        {
            foreach (Person p in peopleList)
            {
                Console.WriteLine(p);
            }
        }
        static void FindPersonByName() // IMPLEMENT USING LINQ
        {
            Console.WriteLine("Enter partial name to look for: ");
            string name = Console.ReadLine().ToUpper();
            var matchList = from p in peopleList where p.Name.ToUpper().Contains(name) select p;
            Console.WriteLine("Matching persons: ");
            foreach (var m in matchList)
            {
                Console.WriteLine(m);
            }
        }
        static void FindPersonYoungerThan() // IMPLEMENT USING LINQ
        {
            Console.WriteLine("Enter maxiumum age to look for: ");
            string maxAgeStr = Console.ReadLine();
            if (!int.TryParse(maxAgeStr, out int maxAge))
            {
                Console.WriteLine("Error: Invalid age.");
                return;
            }
            var matchList = from p in peopleList where p.Age <= maxAge select p;
            if (matchList.Count() == 0)
            {
                Console.WriteLine("No matching persons found");
            }
            else
            {
                Console.WriteLine("Matching persons: ");
                foreach (var m in matchList)
                {
                    Console.WriteLine(m);
                }
            }
        }

        static void ReadAllPeopleFromFile()
        {
            peopleList.Clear();
            /* if (!File.Exists(DataFileName))
            {
                return;
            } */
            try
            {
                string[] linesArray = File.ReadAllLines(DataFileName);
                foreach (string line in linesArray)
                {
                    string[] data = line.Split(';');
                    if (data.Length != 3)
                    {
                        Console.WriteLine("Error: invalid number of fields in line: " + line);
                        continue;
                    }
                    string name = data[0];
                    string ageStr = data[1];
                    if (!int.TryParse(ageStr, out int age))
                    {
                        Console.WriteLine("Error: failed to parse age in line: " + line);
                        continue;
                    }
                    string city = data[2];
                    // create Person, add to list
                    try
                    {
                        Person p = new Person() { Name = name, Age = age, City = city };
                        peopleList.Add(p);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Error: Person data invalid: " + ex.Message);
                        // continue;
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                // ignore
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading from file " + ex.Message);
            }
        }
        static void SaveAllPeopleToFile()
        {
            List<string> linesList = new List<string>();
            foreach (var p in peopleList)
            {
                linesList.Add(p.ToDataString());
            }
            try
            {
                File.WriteAllLines(DataFileName, linesList);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading from file " + ex.Message);
            }
        }

        static int GetMenuChoice()
        {
            Console.Write(
@"What do you want to do?
1. Add person info
2. List persons info
3. Find and list a person by name
4. Find and list persons younger than age
0. Exit
Choice: ");
            try
            {
                string choiceStr = Console.ReadLine();
                int choice = int.Parse(choiceStr);
                return choice;
            }
            catch (Exception ex)
            {
                if (ex is FormatException | ex is OverflowException)
                {
                    Console.WriteLine("Invalid value entered\n");
                }
                else throw ex; // if we don't handle an exception we MUST throw it!
            }
            return -1;
        }


        static TTT DoSomething<TTT>(TTT val)
        {
            TTT vvv = val;
            return vvv;
        }

        static void Main(string[] args)
        {

            Person p1 = new Person() { Role = SocialRole.DistantRelative };
            Console.WriteLine($"Role is: {p1.Role} or {(int)p1.Role}");

            ReadAllPeopleFromFile();
            int choice;
            do
            {
                choice = GetMenuChoice();
                switch (choice)
                {
                    case 1:
                        AddPersonInfo();
                        break;
                    case 2:
                        ListAllPersonsInfo();
                        break;
                    case 3:
                        FindPersonByName();
                        break;
                    case 4:
                        FindPersonYoungerThan();
                        break;
                    case 0: // exit
                        break;
                    default: // ALWAYS have a default handler in switch/case
                        Console.WriteLine("Invalid choice try again.");
                        break;
                }
                Console.WriteLine();
            } while (choice != 0);
            SaveAllPeopleToFile();
        }
    }
}
