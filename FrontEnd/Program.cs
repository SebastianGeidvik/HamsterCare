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
            var cage = new Cage();
            cage.AddHamster(dbContext.Hamsters.First());
            dbContext.SaveChanges();


        }
    }
}