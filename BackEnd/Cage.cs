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
        public virtual ICollection<Hamster> Hamsters { get; set; }

        public bool AddHamster(Hamster hamster)
        {
            if (Hamsters == null)
            {
                Hamsters.Add(hamster);
                return true;
            }
            else if (Hamsters.Count < 3)
            {
                Hamsters.Add(hamster);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CreateCage()
        {
            var dbContext = new DaycareContext();

            if (dbContext.Cages.Count() == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    dbContext.Cages.Add(new Cage());
                }
            }
        }
    }
}