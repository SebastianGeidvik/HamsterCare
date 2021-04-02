using BackEnd;
using System;
using System.Linq;

namespace FrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            Cage.FillCages();
            var dbContext = new DaycareContext();
            dbContext.Cages.ToList().ForEach(c => c.Hamsters.Clear());
            dbContext.SaveChanges();
        }
    }
}