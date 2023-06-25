using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workoutTracker
{
    public class Exercise
    {
        public Exercise(string muscleGroup, string name, string type)
        {
            MuscleGroup = muscleGroup;
            Name = name;
            Type = type;
            ExerciseLibrary.ExerciseList.Add(this);
        }
        //Creation
        public string MuscleGroup { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        //Data
        public List<ExerciseData> History = new List<ExerciseData>();
        public void AddData(ExerciseData data)
        {
            History.Add(new ExerciseData(data.Reps, data.Weight, data.Date, data.Workoutid));
        }
    }
}
