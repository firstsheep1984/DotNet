using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12FirstEF
{
    class SocietyDBContext : DbContext
    {
        virtual public DbSet<Person> People { get; set; }
    }
}
