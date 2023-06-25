using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workoutTracker
{
    public class ExerciseData
    {
        public ExerciseData(int reps, double weight, DateTime date, double workoutid)
        {
            Reps = reps;
            Weight = weight;
            Date = date;
            Workoutid = workoutid;
        }

        public int Reps { get; set; }
        public double Weight { get; set; }
        public DateTime Date { get; set; }
        public double Workoutid { get; set; }
    }
}
