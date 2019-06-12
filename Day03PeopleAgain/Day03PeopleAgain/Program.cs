using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03PeopleAgain
{

    public class InvalidParameterException : Exception
    {
        public InvalidParameterException() { }
        public InvalidParameterException(string msg) : base(msg) { }
        public InvalidParameterException(string msg, Exception orig) : base(msg, orig) { }
    }

    public class Person
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            // 1-50 characters, no semicolons
            set
            {
                if (value.Length < 1 || value.Length > 50 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Name must be 1-50 characters long, no semicolons");
                }
                _name = value;
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            // 0-150
            set
            {
                if (value < 0 || value > 150)
                {
                    throw new InvalidParameterException("Age must be 0-150");
                }
                _age = value;
            }
        }
        public Person(string dataLine)
        {
            string[] data = dataLine.Split(';');
            if (data.Length != 3)
            {
                throw new InvalidParameterException("Line for Person must have exactly 3 values");
            }
            if (data[0] != "Person")
            { // this check is redundant and we still want to have it
                throw new InvalidParameterException("Line must be for Person type");
            }

            Name = data[1];
            string ageStr = data[2];
            try
            {
                Age = int.Parse(ageStr);
            }
            catch (Exception ex)
            {
                if (ex is FormatException | ex is OverflowException)
                { // exception chaining (translate one exception into another)
                    throw new InvalidParameterException("Integer value expected", ex);
                }
                else throw ex;
            }
        }
        public Person(string name, int age) : base()
        {
            Name = name;
            Age = age;
        }

        protected Person() { }

        public virtual string ToDataString()
        {
            return $"Person;{Name};{Age}";
        }

        public override string ToString()
        {
            return $"Person {Name} is {Age}";
        }
    }

    public class Teacher : Person
    {
        private string _subject;
        public string Subject
        {
            get { return _subject; }
            // 1-50 characters, no semicolons
            set
            {
                if (value.Length < 1 || value.Length > 50 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Subject must be 1-50 characters long, no semicolons");
                }
                _subject = value;
            }
        }
        private int _yoe;
        public int YearsOfExperience
        {
            get { return _yoe; }
            // 0-100
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new InvalidParameterException("Years of Experience must be 0-100");
                }
                _yoe = value;
            }
        }

        public Teacher(string dataLine) : base()
        {

            string[] data = dataLine.Split(';');
            if (data.Length != 5)
            {
                throw new InvalidParameterException("Line for Person must have exactly 3 values");
            }
            if (data[0] != "Teacher")
            { // this check is redundant and we still want to have it
                throw new InvalidParameterException("Line must be for Person type");
            }

            try
            {
                Name = data[1];
                Age = int.Parse(data[2]);
                Subject = data[3];
                YearsOfExperience = int.Parse(data[4]);
            }
            catch (Exception ex)
            {
                if (ex is FormatException | ex is OverflowException)
                { // exception chaining (translate one exception into another)
                    throw new InvalidParameterException("Integer value expected", ex);
                }
                else throw ex;
            }

        }
        public Teacher(string name, int age, string subject, int yoe) : base(name, age)
        {
            Subject = subject;
            YearsOfExperience = yoe;
        }
        public override string ToDataString()
        {
            return $"Teacher;{Name};{Age};{Subject};{YearsOfExperience}";
        }

        public override string ToString()
        {
            return $"Teacher {Name} is {Age}, teaches {Subject} since {YearsOfExperience} years";
        }
    }

    public class Student : Person
    {
        private string _program;
        public string Program
        {
            get { return _program; }
            // 1-50 characters, no semicolons
            set
            {
                if (value.Length < 1 || value.Length > 50 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Program must be 1-50 characters long, no semicolons");
                }
                _program = value;
            }
        }
        private double _gpa;
        public double GPA
        {
            get { return _gpa; }
            // 0-100
            set
            {
                if (value < 0 || value > 4.3)
                {
                    throw new InvalidParameterException("GPA must be 0-4.3");
                }
                _gpa = value;
            }
        }

        public Student(string dataLine)
        {

            string[] data = dataLine.Split(';');
            if (data.Length != 5)
            {
                throw new InvalidParameterException("Line for Person must have exactly 3 values");
            }
            if (data[0] != "Student")
            { // this check is redundant and we still want to have it
                throw new InvalidParameterException("Line must be for Person type");
            }
            try
            {
                Name = data[1];
                Age = int.Parse(data[2]);
                Program = data[3];
                GPA = double.Parse(data[4]);
            }
            catch (Exception ex)
            {
                if (ex is FormatException | ex is OverflowException)
                { // exception chaining (translate one exception into another)
                    throw new InvalidParameterException("Integer value expected", ex);
                }
                else throw ex;
            }
        }

        public Student(string name, int age, string program, double gpa) : base(name, age)
        {
            Program = program;
            GPA = gpa;
        }
        public override string ToDataString()
        {
            return $"Student;{Name};{Age};{Program};{GPA}";
        }

        public override string ToString()
        {
            return $"Student {Name} is {Age}, studies {Program} with {GPA:0.00} GPA";
        }
    }


    class Program
    {
        static List<Person> peopleList = new List<Person>();

        static void ReadDataFromFile()
        {
            // open file and parse items, add to list
            string[] linesArray = File.ReadAllLines(@"..\..\people.txt");
            foreach (string line in linesArray)
            {
                try
                {
                    string typeName = line.Split(';')[0];
                    switch (typeName)
                    {
                        case "Person":
                            Person person = new Person(line);
                            peopleList.Add(person);
                            break;
                        case "Teacher":
                            Teacher teacher = new Teacher(line);
                            peopleList.Add(teacher);
                            break;
                        case "Student":
                            Student student = new Student(line);
                            peopleList.Add(student);
                            break;
                        default:
                            Console.WriteLine("Error in data line: don't know how to make " + typeName);
                            break;
                    }
                }
                catch (InvalidParameterException ex)
                {
                    Console.WriteLine("Error parsing line: " + ex.Message);
                }
            }
        }

        private static void ProcessingDisplayTasks()
        {
            // display all as is
            Console.WriteLine("\nAll items:");
            foreach (Person p in peopleList)
            {
                Console.WriteLine(p);
            }
            // display only students
            Console.WriteLine("\nOnly Students items:");
            foreach (Person p in peopleList)
            {
                if (p is Student)
                {
                    Console.WriteLine(p);
                }
            }
            // display only teacher
            Console.WriteLine("\nOnly Teachers items:");
            foreach (Person p in peopleList)
            {
                if (p is Teacher)
                {
                    Console.WriteLine(p);
                }
            }
            // display only students
            Console.WriteLine("\nOnly Person items:");
            foreach (Person p in peopleList)
            {
                if (p.GetType() == typeof(Person))
                {
                    Console.WriteLine(p);
                }
            }
        }

        static void ProcessingAveragesTasks()
        {
            var studentsCollection = from p in peopleList where p is Student select p as Student;
            if (studentsCollection.Count() == 0)
            {
                Console.WriteLine("No Students found to compute averages on");
                return;
            }
            // compute average
            double avg = studentsCollection.Average(s => s.GPA);
            Console.WriteLine($"Average GPA is: {avg:0.00}");
            // compute median
            List<double> GPASortedList = (from s in studentsCollection orderby s.GPA select s.GPA)
                                    .ToList<double>();
            double medianGPA;
            if (GPASortedList.Count() % 2 == 0)
            { // even
                double v1 = GPASortedList[GPASortedList.Count() / 2 - 1];
                double v2 = GPASortedList[GPASortedList.Count() / 2];
                medianGPA = (v1 + v2) / 2;
            } else
            { // odd
                medianGPA = GPASortedList[GPASortedList.Count() / 2];
            }
            Console.WriteLine($"Median is: {medianGPA:0.00}");
            // compute stdandard deviation
            double sumOfSquares = 0;
            foreach (double gpa in GPASortedList)
            {
                sumOfSquares += (gpa - avg) * (gpa - avg);
            }
            double stddev = Math.Sqrt(sumOfSquares / GPASortedList.Count());
            Console.WriteLine($"Standard deviation is {stddev:0.00}");
        }

        static void Main(string[] args)
        {
            try
            {
                ReadDataFromFile();
                // insert some fake data so we can test it
                /*
                peopleList.Add(new Person(...));
                peopleList.Add(new Person(...));
                peopleList.Add(new Person(...));
                peopleList.Add(new Person(...)); */
                ProcessingDisplayTasks();
                ProcessingAveragesTasks();

                /*
                Person p = new Person("");
                Object o = p;

                Student s = (Student)p; // causes Casting exception

                if (p is Student) // check the type
                {
                    Student s2 = (Student)p; // this cast is always safe after if-is
                }

                Student s3 = p as Student; // if cast is impossible s3 is null
                if (s3 != null) { ... }
                */



            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }




        }

        
    }
}
