using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workoutTracker
{
    public static class ExerciseLibrary
    {
        public static List<Exercise> ExerciseList = new List<Exercise>();
        public static void ResetList()
        {
            Workoutlist.Clear();
        }
        public static List<WorkoutData> Workoutlist = new List<WorkoutData>();
    }
}

