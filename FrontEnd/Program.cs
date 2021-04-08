﻿using BackEnd;
using System;
using System.Linq;
using Simulator;
using System.Threading;

namespace FrontEnd
{
    class Program
    {
        private static int _printSpeed;
        static void Main(string[] args)
        {
            StartUpDatabase.CreateDatabase();
            //Console.WriteLine(ExerciseCount());
            MainMenu();
        }
        private static void MainMenu()
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
                MainMenu();
            }
            Console.Clear();
            Console.Write("Enter tick speed (milliseconds): ");
            int.TryParse(Console.ReadLine(), out int tickSpeed);
            if (tickSpeed < 1 || tickSpeed > 2000)
            {
                Console.Clear();
                Console.WriteLine("To slow. Try again.");
                Console.WriteLine("Press any key.");
                Console.ReadKey();
                MainMenu();
            }
            Console.Clear();
            Console.Write("Enter print speed (milliseconds): ");
            int.TryParse(Console.ReadLine(), out int printSpeed);
            _printSpeed = printSpeed;
            if (printSpeed < tickSpeed)
            {
                Console.Clear();
                Console.WriteLine("Print speed can't be lower than tick speed. Try again.");
                Console.WriteLine("Press any key.");
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                Console.Clear();
                var simulator = new Simulator.Simulator(tickSpeed, days);
                simulator.RunSimulator();
                Print();
            }
        }
        private static void Print()
        {
            var dbContext = new DaycareContext();
            var nowDate = DateTime.Now;
            var dateTime = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 7, 0, 0);
            var tickCount = 0;

            while (true)
            {
                Thread.Sleep(_printSpeed);
                var getLogs = dbContext.Logs.Where(l => l.TimeStamp == dateTime).OrderBy(h => h.Hamster.Name);

                if (getLogs.Count() > 1)
                {
                    Console.WriteLine($"Ticker: {tickCount++}".PadRight(15) + $"{dateTime}");
                    Console.WriteLine();
                    Console.WriteLine($"Name".PadRight(15) + $"Age".PadRight(15) + $"Activity".PadRight(15) + $"Time waiting for exercise".PadRight(35) + $"Exercised number of times");
                    Console.WriteLine();

                    foreach (var log in getLogs)
                    {
                        int numberOftimesExercised = log.Hamster.Logs.Where(l => l.Activity == Activity.Exercise).Count();
                        Console.WriteLine($"{log.Hamster.Name}".PadRight(15) + $"{log.Hamster.Age}".PadRight(15) + $"{log.Activity}".PadRight(50) + $"{numberOftimesExercised}");
                    }
                    if (dateTime.Hour == 17)
                    {
                        dateTime = dateTime.AddHours(14);
                        tickCount = 0;
                    }
                    else
                    {
                        dateTime = dateTime.AddMinutes(6);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}