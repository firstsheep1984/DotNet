using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07FriendsList
{
    public class Friend
    {

        // TODO: implement getters and setters with verification
        public string Name;
        public int Age;

        public override string ToString()
        {
            return $"{Name} is {Age} y/o";
        }

        public static bool IsNameValid(string name)
        {
            // NEWBIE solution:
            // if (name.Length < 1 || name.Length > 50) return false; else return true;
            return name.Length >= 1 && name.Length <= 50 && !name.Contains(";");
        }

        public static bool IsAgeValid(int age)
        {
            return age >= 1 && age <= 150;
        }

        public static bool IsAgeValid(string ageStr)
        {
            bool result = int.TryParse(ageStr, out int age);
            if (!result) return false;
            return IsAgeValid(age);
        }

    }
}
