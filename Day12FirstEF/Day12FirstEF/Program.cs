using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12FirstEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            try
            {
                SocietyDBContext ctx = new SocietyDBContext();
                // adding record to database (INSERT)
                Person p1 = new Person { Name = "Tom", Age = random.Next(100), Gender = Gender.Male };
                ctx.People.Add(p1); // this is NOT insert
                ctx.SaveChanges();
                Console.WriteLine("Record addedd");

                // update - fetch then update
                Person p2 = (from p in ctx.People where p.Id == 3 select p).FirstOrDefault<Person>();
                if (p2 != null)
                {
                    p2.Name = "Alibaba";
                    ctx.SaveChanges();
                    Console.WriteLine("Record update");
                } else
                {
                    Console.WriteLine("record to update not found");
                }

                // delete - fetch then delete
                Person p3 = (from p in ctx.People where p.Id == 4 select p).FirstOrDefault<Person>();
                if (p3 != null)
                {
                    ctx.People.Remove(p3); // schedule for deletion from database
                    ctx.SaveChanges();
                    Console.WriteLine("Record deleted");
                }
                else
                {
                    Console.WriteLine("record to delete not found");
                }

                // print all records
                var peopleCol = from p in ctx.People select p;
                foreach (Person p in peopleCol)
                {
                    Console.WriteLine($"{p.Id}: {p.Name} is {p.Age} y/o, {p.Gender}");
                }

            } finally {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }
        }
    }
}
