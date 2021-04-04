using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ExerciseCage
    {
        public int Id { get; set; }
        public virtual ICollection<Hamster> Hamsters { get; set; }

        public static void CreateExerciseCage()
        {
            var dbContext = new DaycareContext();
            dbContext.ExerciseCages.Add(new ExerciseCage());
            dbContext.SaveChanges();
        }

        public static void Exercise()
        {
            var dbContext = new DaycareContext();
            var exerciseCage = dbContext.ExerciseCages.First();
            Gender gender = Gender.Male;
            int counter = 0;
            foreach (var cage in dbContext.Cages)
            {
                foreach (var hamster in cage.Hamsters)
                {
                    if (hamster.Gender == gender && counter < 6)
                    {
                        exerciseCage.Hamsters.Add(hamster);
                        cage.Hamsters.Remove(hamster);
                        counter++;
                    }
                }
            }
            dbContext.SaveChanges();
            //var hamsterQueue = new Queue<Hamster>();

            //foreach (var hamster in dbContext.Hamsters)
            //{
            //        hamsterQueue.Enqueue(hamster);
            //}
            //foreach (var exerciseCage in dbContext.ExerciseCages)
            //{
            //    for (int i = 0; i < 6; i++)
            //    {
            //        var hamster = hamsterQueue.Dequeue();
            //        foreach (var cage in dbContext.Cages)
            //        {
            //            if (cage.Hamsters.Contains(hamster))
            //            {
            //                cage.Hamsters.Remove(hamster);
            //            }
            //        }
            //        exerciseCage.Hamsters.Add(hamster);
            //    }
            //}
            //dbContext.SaveChanges();
        }
    }
}