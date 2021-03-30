using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Cage
    {
        public int Id { get; set; }
        protected virtual ICollection<Hamster> Hamsters { get; set; }
    }
}
