using BackEnd;
using System;
using System.Linq;

namespace FrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new DaycareContext();
            //var cage = dbContext.Cages.First();
            //cage.Hamsters.Add(dbContext.Hamsters.First());
            //dbContext.SaveChanges();

            var readFromFile = new ReadFromFile();
            readFromFile.ImportHamsters();

        }
    }
}