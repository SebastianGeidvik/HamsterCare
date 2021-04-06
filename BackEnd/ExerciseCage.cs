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

        public static void Exercise()
        {
            var dbContext = new DaycareContext();

            var query = from Hamster in dbContext.Hamsters.ToList()
                        group Hamster by Hamster into HamsterGroup
                        orderby HamsterGroup.Key.Logs.Count ascending
                        select new { Hamster = HamsterGroup.Key, ExerciseCount = HamsterGroup.Key.Logs.Where(l => l.Activity == Activity.Exercise).Count() };

            var exerciseCage = dbContext.ExerciseCages.First();
            Gender gender = Gender.Unspecified;
            int counter = 0;

            foreach (var group in query)
            {
                if (counter == 0)
                {
                    gender = group.Hamster.Gender;
                }
                if (group.Hamster.Gender == gender && counter < 6)
                {
                    foreach (var cage in dbContext.Cages)
                    {
                        foreach (var hamster in dbContext.Hamsters)
                        {
                            if (hamster == group.Hamster)
                            {
                                cage.Hamsters.Remove(hamster);
                            }
                        }
                    }
                    exerciseCage.Hamsters.Add(group.Hamster);
                    group.Hamster.Logs.Add(new Log(DateTime.Now, Activity.Exercise));
                    counter++;
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
                    hamster.Logs.Add(new Log(DateTime.Now, Activity.InCage));
                    cage.Hamsters.Remove(hamster);
                }
            }
            dbContext.SaveChanges();
        }
        //public static void CountHamsterExercise()
        //{
        //    var dbContext = new DaycareContext();
        //    var query = from Hamster in dbContext.Hamsters.ToList()
        //                group Hamster by Hamster into HamsterGroup
        //                orderby HamsterGroup.Key.Logs.Count ascending
        //                select new { HamsterId = HamsterGroup.Key.Id, HamsterName = HamsterGroup.Key.Name, Hamsterlogs = HamsterGroup.Key.Logs.Where(l => l.Activity == Activity.Exercise).Count() };

        //    foreach (var group in query)
        //    {
        //        Console.WriteLine(group.HamsterId + " " + group.HamsterName + " " + group.Hamsterlogs);
        //    }

            //var hamsterQuery = dbContext.Hamsters.ToList()
            //    .GroupBy(h => h)
            //    .Select(g => new {
            //        Hamster = g.Key,
            //        ExerciseCount = g.Key.Logs
            //    .Where(l => l.Activity == Activity.Exercise).Count()
            //    });
        //}
    }
}