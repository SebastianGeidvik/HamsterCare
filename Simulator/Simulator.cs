using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackEnd;

namespace Simulator
{
    public class Simulator
    {
        public static bool IsActive { get; set; }
        public static int TickCounter { get; set; }
        public static DateTime Date { get; set; }
        public int SleepTime { get; set; }
        public int InputDay { get; set; }
        public int DaysPassed { get; set; }
        public Simulator(int sleepTime, int days)
        {
            TickCounter = 0;
            var nowDate = DateTime.Now;
            Date = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 7, 0, 0);
            SleepTime = sleepTime;
            InputDay = days;
            DaysPassed = 0;
        }
        public void RunSimulator() // A method for starting the simulator. Runs on a seprate thread and makes the days tick depending on the input from the user.
        {
            Task.Run(async () =>
            {
                while (InputDay > DaysPassed)
                {
                    Thread.Sleep(SleepTime);
                    await OnTick();
                    TickCounter++;
                    Date = Date.AddMinutes(6);
                    if (TickCounter == 101)
                    {
                        DaysPassed++;
                        TickCounter = 0;
                        var nowDate = DateTime.Now;
                        Date = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 7, 0, 0).AddDays(DaysPassed);
                    }
                }
            });
        }
        public async Task OnTick() // This method runs on every tick and the methods inside it runs asynchronously. 
        {
            if (TickCounter == 0)
            {
                await Operations.FillCages();
                await Operations.Exercise();
            }
            if (TickCounter == 10)
            {
                await Operations.GoToCage();
                await Operations.Exercise();
            }
            if (TickCounter == 20)
            {
                await Operations.GoToCage();
                await Operations.Exercise();
            }
            if (TickCounter == 30)
            {
                await Operations.GoToCage();
                await Operations.Exercise();
            }
            if (TickCounter == 40)
            {
                await Operations.GoToCage();
                await Operations.Exercise();
            }
            if (TickCounter == 50)
            {
                await Operations.GoToCage();
                await Operations.Exercise();
            }
            if (TickCounter == 60)
            {
                await Operations.GoToCage();
                await Operations.Exercise();
            }
            if (TickCounter == 70)
            {
                await Operations.GoToCage();
                await Operations.Exercise();
            }
            if (TickCounter == 80)
            {
                await Operations.GoToCage();
                await Operations.Exercise();
            }
            if (TickCounter == 90)
            {
                await Operations.GoToCage();
                await Operations.Exercise();
            }
            if (TickCounter == 100)
            {
                await Operations.GoToCage();
                await Operations.CheckOutHamsters();
            }
            await LogTickActivity();
        }
        private async Task LogTickActivity() // Logs hamsters on every tick.
        {
            await Task.Run(() =>
            {
                var dbContext = new DaycareContext();
                foreach (var cage in dbContext.Cages)
                {
                    foreach (var hamster in cage.Hamsters)
                    {
                        hamster.Logs.Add(new Log(Date, Activity.InCage));
                    }
                }
                foreach (var cage in dbContext.ExerciseCages)
                {
                    foreach (var hamster in cage.Hamsters)
                    {
                        hamster.Logs.Add(new Log(Date, Activity.Exercise));
                    }
                }
                dbContext.SaveChanges();
            });
        }
    }
}