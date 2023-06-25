using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workoutTracker
{
    public class WorkoutData
    {
        public string name;
        public string notes;
        public double workoutid;
        public bool ispreset;
        public DateTime start_time;
        public DateTime end_time;

        public WorkoutData(string name, string notes, double workoutid, bool ispreset, DateTime start_time, DateTime end_time)
        {
            this.name = name;
            this.notes = notes;
            this.workoutid = workoutid;
            this.ispreset = ispreset;
            this.start_time = start_time;
            this.end_time = end_time;
        }
    }
}
