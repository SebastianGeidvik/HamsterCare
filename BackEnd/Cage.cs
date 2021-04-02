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

        public static bool FillCages()
        {
            if (true)
            {
                var dbContext = new DaycareContext();
                var hamsterQueue = new Queue<Hamster>();
                foreach (var hamster in dbContext.Hamsters)
                {
                    hamsterQueue.Enqueue(hamster);
                }

                foreach (var cage in dbContext.Cages)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        cage.Hamsters.Add(hamsterQueue.Dequeue());
                    }
                }
                dbContext.SaveChanges();
                return true;
            }

            //if (Hamsters == null)
            //{
            //    Hamsters.Add(hamster);
            //    return true;
            //}
            //else if (Hamsters.Count < 3)
            //{
            //    Hamsters.Add(hamster);
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
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