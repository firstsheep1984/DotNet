using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12FirstEF
{
    public enum Gender { Male = 1, Female = 2, NA = 3 }

    // [Table("Personalities")]
    public class Person
    {
        // [Key] // primary key
        public int Id { get; set; }

        // [Column("PersonName")]
        [Required] // means not-null
        [StringLength(50)] // nvarchar(50)
        public string Name { get; set; }

        // [Index]
        public int Age { get; set; }


        // [NotMapped] // in memory only, not in database
        // public string Description { get; set; }
        // [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

    }
}
