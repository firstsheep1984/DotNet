using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13CarsAndOwnersEF
{
    class Program
    {
        static ParkingDbContext ctx;

        static int GetMenuChoice()
        {
            Console.Write(
@"1. List all cars and their owner
2. List all owners and their cars
3. Add a car (no owner)
4. Add an owner (no cars)
5. Assign car to an owner (or no owner)
6. Delete an owner with all cars they own
0. Quit
Enter your choice: ");
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

        static void Main(string[] args)
        {
            ctx = new ParkingDbContext(); // exceptions?
            int choice = 0;
            do
            {
                choice = GetMenuChoice();
                switch (choice)
                {
                    case 1: ListAllCarsAndTheirOwners(); break;
                    case 2: ListAllOwnersAndTheirCars(); break;
                    case 3: AddACar_NoOwner(); break;
                    case 4: AddAnOwner_NoCars(); break;
                    case 5: AssignCarToAnOwnerOrNoOwner(); break;
                    case 6: DeleteAnOwnerWithAllCarsTheyOwn(); break;
                    case 0: break; // Quit
                }
                Console.WriteLine();
            } while (choice != 0);
        }

        private static void DeleteAnOwnerWithAllCarsTheyOwn()
        {
            // 2. show all owners, choose an owner, empty means set owner to null
            ListAllOwnersAndTheirCars();
            Console.Write("Enter Id of an owner");
            int ownerId = int.Parse(Console.ReadLine()); // FIXME
            Owner owner = ctx.Owners.Find(ownerId);
            if (owner == null)
            {
                Console.WriteLine("Owner not found");
                return;
            }
            //
            foreach (Car c in owner.CarsCollection.ToList<Car>())
            {
                ctx.Cars.Remove(c); // schedule for deletion
            }
            ctx.Owners.Remove(owner);
            ctx.SaveChanges();
            Console.WriteLine("Owner and their cars deleted");
        }

        private static void AssignCarToAnOwnerOrNoOwner()
        {
            // 1. show all the cars with their Ids, choose a car
            ListAllCarsAndTheirOwners();
            Console.Write("Enter Id of a car: ");
            int carId = int.Parse(Console.ReadLine()); // FIXME
            // Car car = (from c in ctx.Cars where c.Id == carId select c).FirstOrDefault<Car>();
            Car car = ctx.Cars.Find(carId);
            if (car == null)
            {
                Console.WriteLine("Car not found");
                return;
            }
            // 2. show all owners, choose an owner, empty means set owner to null
            ListAllOwnersAndTheirCars();
            Console.Write("Enter Id of an owner, -1 to set null: ");
            int ownerId = int.Parse(Console.ReadLine()); // FIXME
            Owner owner = null;
            if (ownerId != -1)
            {
                owner = ctx.Owners.Find(ownerId);
                if (owner == null)
                {
                    Console.WriteLine("Owner not found");
                    return;
                }
            }
            //
            car.Owner = owner; // modify the entity
            ctx.SaveChanges();
            Console.WriteLine("owner change saved");
        }

        private static void AddAnOwner_NoCars()
        {
            Console.Write("Enter Owner name: ");
            string name = Console.ReadLine();
            Owner owner = new Owner() { Name = name };
            ctx.Owners.Add(owner);
            ctx.SaveChanges();
        }

        private static void AddACar_NoOwner()
        {
            Console.Write("Enter make/model: ");
            string makeModel = Console.ReadLine();
            Console.Write("Enter year of production: ");
            int yop = int.Parse(Console.ReadLine()); // FIXME
            Car car = new Car() { MakeModel = makeModel, YearOfProd = yop };
            ctx.Cars.Add(car);
            ctx.SaveChanges();
        }

        private static void ListAllOwnersAndTheirCars()
        {
            var ownersColl = from o in ctx.Owners select o;
            foreach (Owner o in ownersColl)
            {
                Console.WriteLine(o); // display owner information
                foreach (Car c in o.CarsCollection)
                {
                    Console.WriteLine(c);
                }
            }
        }

        private static void ListAllCarsAndTheirOwners()
        {
            var carsColl = from c in ctx.Cars select c;
            foreach (Car c in carsColl)
            {
                Console.WriteLine(c);
            }
        }
    }
}
