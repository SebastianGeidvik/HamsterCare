using BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    internal class Operations
    {
        public static async Task FillCages() // Method for filling cages with hamsters. Seperates females from males and putts them in two different queues.
        {
            await Task.Run (() =>
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
                            hamster.Logs.Add(new Log(Simulator.Date, Activity.Arrival));
                            hamster.CheckedIn = Simulator.Date;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            var hamster = femaleHamsterQueue.Dequeue();
                            cage.Hamsters.Add(hamster);
                            hamster.Logs.Add(new Log(Simulator.Date, Activity.Arrival));
                            hamster.CheckedIn = Simulator.Date;
                        }
                    }
                }
                dbContext.SaveChanges();
                return true;
            });
        }
        public static async Task CheckOutHamsters() // Method to check out the hamsters from the daycare.
        {
            await Task.Run(() =>
            {
                var dbContext = new DaycareContext();
                dbContext.Cages.ToList().ForEach(c => c.Hamsters.Clear());
                dbContext.ExerciseCages.First().Hamsters.Clear();
                dbContext.Hamsters.ToList().ForEach(h => h.Logs.Add(new Log(Simulator.Date, Activity.Departure)));
                dbContext.Hamsters.ToList().ForEach(h => h.CheckedIn = null);
                dbContext.SaveChanges();
            });
        }
        public static async Task Exercise() // Method for getting hamsters from their cage to exercise cage.
                                            // Checks number of times the hamsters has been exercising and picks the hamster who's been exercising least.
        {
            await Task.Run(() =>
            {
                var dbContext = new DaycareContext();

                var query = from Hamster in dbContext.Hamsters.ToList()
                            group Hamster by Hamster into HamsterGroup
                            select new { Hamster = HamsterGroup.Key, ExerciseCount = HamsterGroup.Key.Logs.Where(l => l.Activity == Activity.Exercise).Count() };

                query = query.ToList().OrderBy(h => h.ExerciseCount);
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
                        counter++;
                    }
                }
                dbContext.SaveChanges();
            });
        }
        public static async Task GoToCage() // Method for getting the hamsters back to their cage from Exercise cage.
        {
            await Task.Run(() =>
            {
                var dbContext = new DaycareContext();

                foreach (var cage in dbContext.ExerciseCages)
                {
                    foreach (var hamster in cage.Hamsters)
                    {
                        var freeCage = dbContext.Cages.First(c => c.Hamsters.Count < 3);
                        freeCage.Hamsters.Add(hamster);
                        //hamster.Logs.Add(new Log(Simulator.Date, Activity.InCage));
                        cage.Hamsters.Remove(hamster);
                    }
                }
                dbContext.SaveChanges();
            });
        }
    }
}