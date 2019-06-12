using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13CarsAndOwnersEF
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string MakeModel { get; set; } // up to 100 characters
        public int YearOfProd { get; set; }
        public virtual Owner Owner { get; set; } // relation, may be null

        public override string ToString()
        {
            string ownerStr = Owner == null ? "null" : Owner.ToString();
            return $"Car({Id}): {MakeModel} in {YearOfProd} owned by {ownerStr}";
        }
    }
}
