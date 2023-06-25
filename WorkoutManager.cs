using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workoutTracker
{
    public static class WorkoutManager
    {
        public static double CurrentWorkout = 0.00;
        public static void NewWorkout()
        {
            CurrentWorkout = Math.Ceiling(CurrentWorkout);
        }

    }
}
