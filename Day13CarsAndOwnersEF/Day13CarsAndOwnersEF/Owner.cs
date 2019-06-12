using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13CarsAndOwnersEF
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // up to 100 characters
        public virtual ICollection<Car> CarsCollection { get; set; } // relation

        public override string ToString()
        {
            return $"Owner({Id}): {Name}"; // collection of cars???
        }
    }
}
