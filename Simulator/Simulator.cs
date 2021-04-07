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
        public int TickCounter { get; set; }
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
        public void RunSimulator()
        {
            Task.Run(() =>
            {
                while (InputDay > DaysPassed)
                {
                    Thread.Sleep(SleepTime);
                    OnTick();
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
        public void OnTick()
        {
            if (TickCounter == 0)
            {
                Operations.FillCages();
            }
            if (TickCounter == 1)
            {
                Operations.Exercise();
            }
            if (TickCounter == 10)
            {
                Operations.GoToCage();
                Operations.Exercise();
            }
            if (TickCounter == 20)
            {
                Operations.GoToCage();
                Operations.Exercise();
            }
            if (TickCounter == 30)
            {
                Operations.GoToCage();
                Operations.Exercise();
            }
            if (TickCounter == 40)
            {
                Operations.GoToCage();
                Operations.Exercise();
            }
            if (TickCounter == 50)
            {
                Operations.GoToCage();
                Operations.Exercise();
            }
            if (TickCounter == 60)
            {
                Operations.GoToCage();
                Operations.Exercise();
            }
            if (TickCounter == 70)
            {
                Operations.GoToCage();
                Operations.Exercise();
            }
            if (TickCounter == 80)
            {
                Operations.GoToCage();
                Operations.Exercise();
            }
            if (TickCounter == 90)
            {
                Operations.GoToCage();
                Operations.Exercise();
            }
            if (TickCounter == 100)
            {
                Operations.GoToCage();
                Operations.CheckOutHamsters();
            }
            Console.WriteLine($"Ticks: {TickCounter} Date: {Date}");
        }
    }
}