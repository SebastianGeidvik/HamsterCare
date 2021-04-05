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
        }
        public static void GoToCage()
        {
            var dbContext = new DaycareContext();

            foreach (var cage in dbContext.ExerciseCages)
            {
                foreach (var hamster in cage.Hamsters)
                {
                    var freeCage = dbContext.Cages.First(c => c.Hamsters.Count < 3);
                    freeCage.Hamsters.Add(hamster);
                    cage.Hamsters.Remove(hamster);
                }
            }
            dbContext.SaveChanges();
        }
    }
}