using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09TodoDBApp
{
    public class Todo
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public Status Status { get; set; }
        public DateTime DueDate { get; set; }

        public override string ToString()
        { // TODO: fix date formatting to only display date
            return $"{Id}: {Task} due by {DueDate} is {Status}";
        }
    }

    public enum Status { Pending, Done }

}
