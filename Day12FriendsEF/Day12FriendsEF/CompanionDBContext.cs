using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12FriendsEF
{
    public class CompanionDBContext : DbContext
    {
        virtual public DbSet<Friend> Friends { get; set; }
    }
}
