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
            var dbContext = new DaycareContext();

            var maleHamsterQueue = new Queue<Hamster>();
            var femaleHamsterQueue = new Queue<Hamster>();

            foreach (var hamster in dbContext.Hamsters)
            {
                if (hamster.Gender == Gender.Female)
                {
                    femaleHamsterQueue.Enqueue(hamster);
                }
                else if (hamster.Gender == Gender.Male)
                {
                    maleHamsterQueue.Enqueue(hamster);
                }
            }
            foreach (var cage in dbContext.Cages)
            {
                if (maleHamsterQueue.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var hamster = maleHamsterQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var hamster = femaleHamsterQueue.Dequeue();
                        cage.Hamsters.Add(hamster);
                    }
                }
            }
            dbContext.SaveChanges();
            return true;
        }
        public static void CreateCage()
        {
            var dbContext = new DaycareContext();

            if (dbContext.Cages.Count() == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    dbContext.Cages.Add(new Cage());
                }
            }
            dbContext.SaveChanges();
        }
        public void CheckOutHamsters()
        {
            var dbContext = new DaycareContext();
            dbContext.Cages.ToList().ForEach(c => c.Hamsters.Clear());
            dbContext.SaveChanges();
        }
    }
}