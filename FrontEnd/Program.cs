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
            var simulator = new Simulator.Simulator(10, 2);
            simulator.TimerMethod();
            Console.ReadKey();
        }
    }
}