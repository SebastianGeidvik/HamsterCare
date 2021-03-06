using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public enum Gender { Male, Female, Unspecified };
    public class Hamster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string Owner { get; set; }
        public DateTime? CheckedIn { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
