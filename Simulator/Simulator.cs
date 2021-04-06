using System;
using System.Threading;
using System.Threading.Tasks;

namespace Simulator
{
    public class Simulator
    {
        public int TickCounter { get; set; }
        public DateTime Date { get; set; }
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
        public void TimerMethod()
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
            Console.WriteLine($"Ticks: {TickCounter} Date: {Date}");
        }
    }
}