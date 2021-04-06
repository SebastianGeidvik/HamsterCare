using System;
using System.Threading;
using System.Threading.Tasks;

namespace Simulator
{
    public class Simulator
    {
        //public Timer Timer { get; set; }
        //public Simulator()
        //{
        //    Timer = new Timer(OnTick,null, 1000, 1000);
        //}
        public void TimerMethod()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    OnTick(null);
                }
            });
        }
        public void OnTick(object obj)
        {
            Console.WriteLine("Tick.");
        }
    }
}