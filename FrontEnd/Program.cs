using BackEnd;
using System;
using System.Linq;
using Simulator;
using System.Threading;
using System.Threading.Tasks;

namespace FrontEnd
{
    class Program
    {
        private static int _printSpeed;
        private static bool _printerActive;
        static void Main(string[] args)
        {
            StartUpDatabase.CreateDatabase();
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
                ReadingInput();
                Print();
            }
        }
        private static void ReadingInput() // A paus function running on a seperate thread.
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var input = Console.ReadKey(true);
                    if (input.Key == ConsoleKey.Spacebar)
                    {
                        if (_printerActive == true)
                        {
                            _printerActive = false;
                        }
                        else if (_printerActive == false)
                        {
                            _printerActive = true;
                        }
                    }
                    if (input.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                }
            });
        }
        private static void Print() // A method that prints all info from the database log.
        {
            _printerActive = true;
            var dbContext = new DaycareContext();
            var nowDate = DateTime.Now;
            var dateTime = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 7, 0, 0);
            var tickCount = 0;
            while (true)
            {
                while (_printerActive)
                {
                    Thread.Sleep(_printSpeed);
                    var getLogs = dbContext.Logs.Where(l => l.TimeStamp == dateTime).OrderBy(h => h.Hamster.Name).ThenBy(h => h.Activity).ToList();
                    if (getLogs.Count() == 60 && tickCount == 0)
                    {
                        Console.WriteLine($"Tick: {tickCount++}".PadRight(15) + $"{dateTime}");
                        Console.WriteLine();
                        Console.WriteLine($"Name".PadRight(15) + $"Age".PadRight(15) + $"Activity".PadRight(15) + $"Time waiting for exercise (min)".PadRight(35) + $"Exercised number of times");
                        Console.WriteLine();
                        var count = 2;
                        foreach (var log in getLogs)
                        {
                            if (count % 2 == 0)
                            {
                                int numberOftimesExercised = log.Hamster.Logs.Where(l => l.Activity == Activity.Exercise && l.TimeStamp >= dateTime.Date).Count() / 10;
                                var minutes = TimeWaitingForExercise(log.Hamster, dateTime);
                                Console.WriteLine($"{log.Hamster.Name}".PadRight(15) + $"{log.Hamster.Age}".PadRight(15) + $"{log.Activity}".PadRight(15) + $"{minutes}".PadRight(35) + $"{numberOftimesExercised}");
                            }
                            else
                            {
                                Console.WriteLine($"".PadRight(30) + $"{log.Activity}");
                            }
                            count++;
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
                    else if (getLogs.Count() > 1 && tickCount > 0)
                    {
                        Console.WriteLine($"Tick: {tickCount++}".PadRight(15) + $"{dateTime}");
                        Console.WriteLine();
                        Console.WriteLine($"Name".PadRight(15) + $"Age".PadRight(15) + $"Gender".PadRight(15) + $"Activity".PadRight(15) + $"Time waiting for exercise (min)".PadRight(35) + $"Exercised number of times");
                        Console.WriteLine();

                        foreach (var log in getLogs)
                        {
                            int numberOftimesExercised = log.Hamster.Logs.Where(l => l.Activity == Activity.Exercise && l.TimeStamp >= dateTime.Date).Count() / 10;
                            var minutes = TimeWaitingForExercise(log.Hamster, dateTime);
                            Console.WriteLine($"{log.Hamster.Name}".PadRight(15) + $"{log.Hamster.Age}".PadRight(15) + $"{log.Hamster.Gender}".PadRight(15) + $"{log.Activity}".PadRight(15) + $"{minutes}".PadRight(35) + $"{numberOftimesExercised}");
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
        private static int TimeWaitingForExercise(Hamster hamster, DateTime dateTime) // Method for calculate how long Hamsters have been waiting for exercise.
        {
            var logList = hamster.Logs.Where(l => l.Activity == Activity.Exercise && l.TimeStamp.Day == dateTime.Day).ToList().OrderBy(l => l.TimeStamp);
            var checkInTime = new TimeSpan(7, 0, 0);

            if (logList.Any())
            {
                var timeWaited = logList.First().TimeStamp.TimeOfDay - checkInTime;
                var totalTimeWaiting = timeWaited.TotalMinutes;
                return (int)totalTimeWaiting;
            }
            else
            {
                var timeStillWaiting = dateTime.TimeOfDay - checkInTime;
                var totalTimeStillWaiting = timeStillWaiting.TotalMinutes;
                return (int)totalTimeStillWaiting;
            }
        }
    }
}