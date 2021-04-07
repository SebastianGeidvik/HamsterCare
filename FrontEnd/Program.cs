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
            Simulator();
        }

        private static void Simulator()
        {
            Console.Clear();
            Console.WriteLine("Welcome to simulator for Hamsters");
            Console.Write("Enter how many days the simulator will run: ");
            int.TryParse(Console.ReadLine(), out int days);
            if (days == 0 || days > 11)
            {
                Console.Clear();
                Console.WriteLine("Can't choose zero days. Try again.");
                Console.WriteLine("Press any key.");
                Console.ReadKey();
                Simulator();
            }
            Console.Clear();
            Console.Write("Enter speed (milliseconds): ");
            int.TryParse(Console.ReadLine(), out int speed);
            if (speed < 1 || speed > 2000)
            {
                Console.Clear();
                Console.WriteLine("To slow. Try again.");
                Console.WriteLine("Press any key.");
                Console.ReadKey();
                Simulator();
            }
            else
            {
                Console.Clear();
                var simulator = new Simulator.Simulator(speed, days);
                simulator.RunSimulator();
                Console.ReadKey();
            }
        }
    }
}
