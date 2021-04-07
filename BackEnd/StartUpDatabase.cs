using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class StartUpDatabase
    {
        public static void CreateDatabase()
        {
            WipeLogs();
            ImportHamsters();
            CreateCages();
            CreateExerciseCage();
        }

        private static void WipeLogs()
        {
            var dbContext = new DaycareContext();
            if (dbContext.Logs.Count() > 0)
            {
                foreach (var log in dbContext.Logs)
                {
                    dbContext.Remove(log);
                }
            }
            dbContext.SaveChanges();
        }

        private static void ImportHamsters()
        {
            var dbContext = new DaycareContext();
            if (dbContext.Hamsters.Count() == 0)
            {
                var csvLines = File.ReadAllLines(@"..\net5.0\SeedFromFile\Hamsterlista30.csv");

                foreach (var csvLine in csvLines)
                {
                    var hamster = new Hamster();

                    string[] values = csvLine.Split(";");
                    hamster.Name = (values[0]);
                    hamster.Age = Convert.ToInt32(values[1]);
                    if (values[2] == "M")
                    {
                        hamster.Gender = Gender.Male;
                    }
                    else if (values[2] == "K")
                    {
                        hamster.Gender = Gender.Female;
                    }
                    hamster.Owner = (values[3]);

                    dbContext.Hamsters.Add(new Hamster() { Name = hamster.Name, Age = hamster.Age, Gender = hamster.Gender, Owner = hamster.Owner });
                    dbContext.SaveChanges();
                }
            }
        }
        private static void CreateCages()
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
        private static void CreateExerciseCage()
        {
            var dbContext = new DaycareContext();
            if (dbContext.ExerciseCages.Count() == 0)
            {
                dbContext.ExerciseCages.Add(new ExerciseCage());
                dbContext.SaveChanges();
            }
        }
    }
}