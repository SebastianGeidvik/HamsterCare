using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public enum Activity
    {
        Arrival, Exercise, InCage, Departure
    }
    public class Log
    {
        public int Id { get; set; }
        public Activity Activity  { get; set; }
        public DateTime TimeStamp { get; set; }

        public Log(Activity activity, DateTime date)
        {
            Activity = activity;
            TimeStamp = date;
        }
    }
}
