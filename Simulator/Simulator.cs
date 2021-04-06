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
        public int Days { get; set; }
        public Simulator(int sleepTime, int days)
        {
            TickCounter = 0;
            var nowDate = DateTime.Now;
            Date = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 7, 0, 0);
            SleepTime = sleepTime;
            Days = days;
        }
        public void TimerMethod()
        {
            Task.Run(() =>
            {
                int dayCount = 0;
                while (Days > dayCount)
                { 
                    Thread.Sleep(SleepTime);
                    OnTick();
                    TickCounter++;
                    Date = Date.AddMinutes(6);
                    dayCount++;
                }

            });
        }
        public void OnTick()
        {
            Console.WriteLine($"Ticks: {TickCounter + 1} Date: {Date}");

        }
    }
}