using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace workoutTracker
{
    public static class WindowManager
    {
        public enum Window
        {
            Home, Workout, History, Stats, CreateExercise, createworkout, exerciseselector, workoutselector,personalrecord
        }
        private static Window currentWindow;
        public static Window CurrentWindow
        {
            get { return currentWindow; }
            set
            {
                WindowChanged?.Invoke();
                currentWindow = value;
            }
        }

        public delegate void MyEventHandler();
        public static event MyEventHandler WindowChanged;
        //change before usb export
        private static string Exercisefile = @"C:\Users\Leon\source\repos\workoutTracker\workoutTracker\Classes\Data\ExerciseDB.txt";
        private static string Workoutfile = @"C:\Users\Leon\source\repos\workoutTracker\workoutTracker\Classes\Data\WorkoutDB.txt";
        private static string PrFile = @"C:\Users\Leon\source\repos\workoutTracker\workoutTracker\Classes\Data\PrDB.txt";
        public static void SaveData()
        { 
            //save exercise data
            File.WriteAllText(Exercisefile, "");
            foreach (Exercise i in ExerciseLibrary.ExerciseList)
            {
                string SaveFormat = i.MuscleGroup + "," + i.Name + "," + i.Type + ",";
                foreach (ExerciseData d in i.History)
                {
                    SaveFormat += d.Reps + "#" + d.Weight + "#" + d.Date + "#" + d.Workoutid +"!";
                }
                SaveFormat += "@";
                File.AppendAllText(Exercisefile, SaveFormat);
            }
            //save workout data
            File.WriteAllText(Workoutfile, "");
            foreach (WorkoutData wd in ExerciseLibrary.Workoutlist)
            {
                File.AppendAllText(Workoutfile, wd.name + "#" + wd.notes + "#" + wd.workoutid + "#" + wd.ispreset + "#" + wd.start_time + "#" + wd.end_time + ",");
            }
            //save pr data
            File.WriteAllText(PrFile, "");
            File.AppendAllText(PrFile, PersonalRecord.ExerName.ToString() + "#" + PersonalRecord.CurrentWeight.ToString() + "#" + PersonalRecord.GoalWeight + "#" + PersonalRecord.PrDate);
        }
        public static void LoadData()
        {
            #region LoadExercise_Data
            string data = File.ReadAllText(Exercisefile);
            ExerciseLibrary.ResetList();

            string[] exercises = data.Split('@');
            foreach (string s in exercises)
            {
                if (s != "")
                {
                    string[] EInfo = s.Split(',');
                    Exercise addex = new Exercise(EInfo[0], EInfo[1], EInfo[2]);
                    if (EInfo.Length > 3)
                    { 
                        if (EInfo[3] != "")
                        {
                            string[] history = EInfo[3].Split('!');
                            foreach (string h in history)
                            {
                                if (h != "")
                                {
                                    string[] edata = h.Split('#');
                                    addex.AddData(new ExerciseData(Convert.ToInt32(edata[0]), Convert.ToDouble(edata[1]), Convert.ToDateTime(edata[2]), Convert.ToDouble(edata[3])));
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            string workoutData = File.ReadAllText(Workoutfile);
            string[] workouts = workoutData.Split(',');
            int workoutidd = 0;
            foreach (string s in workouts)
            {
                if (s != "")
                {
                    string[] workoutInfo = s.Split('#');
                    if (Convert.ToInt32(workoutInfo[2]) > workoutidd) workoutidd = Convert.ToInt32(workoutInfo[2]);
                    ExerciseLibrary.Workoutlist.Add(new WorkoutData(workoutInfo[0], workoutInfo[1], Convert.ToInt32(workoutInfo[2]), Convert.ToBoolean(workoutInfo[3]), Convert.ToDateTime(workoutInfo[4]), Convert.ToDateTime(workoutInfo[5])));
                }
            }
            workoutidd++;
            WorkoutManager.CurrentWorkout = workoutidd;
            //load pr data
            string prdata = File.ReadAllText(PrFile);
            string[] PrArray = prdata.Split('#');
            if (PrArray.Length > 2)
            {
                PersonalRecord.ExerName = PrArray[0];
                PersonalRecord.CurrentWeight = Convert.ToDouble(PrArray[1]);
                PersonalRecord.GoalWeight = Convert.ToDouble(PrArray[2]);
                PersonalRecord.PrDate = Convert.ToDateTime(PrArray[3]);
                WindowChanged?.Invoke();
            }

        }
    }
}
