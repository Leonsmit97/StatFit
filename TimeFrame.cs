using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workoutTracker
{
    public static class TimeFrame
    {
        public enum range
        {
            year, month, week
        }
        public static range TimeRange { get; set; }
    }
}
