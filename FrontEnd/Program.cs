using BackEnd;
using System;
using System.Linq;
using Simulator;

namespace FrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            StartUpDatabase.CreateDatabase();
            var simulator = new Simulator.Simulator(100, 2);
            simulator.RunSimulator();
            Console.ReadKey();
        }
    }
}