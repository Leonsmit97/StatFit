using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace workoutTracker
{
    public partial class WorkoutHistory : UserControl
    {
        public WorkoutHistory()
        {
            InitializeComponent();
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.Home;
        }

        private void workoutbtn_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.Workout;
        }

        private void historybtn_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.History;
        }

        private void statsbtn_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.Stats;
        }

        private void WorkoutHistory_Load(object sender, EventArgs e)
        {
            WindowManager.WindowChanged += WindowChange;
        }

        void WindowChange()
        {
            HistoryListbox.Items.Clear();
            foreach (WorkoutData i in ExerciseLibrary.Workoutlist)
            {
                TimeSpan time_difference = i.end_time - i.start_time;
                HistoryListbox.Items.Add("--------------------------------------------------------------------------");
                HistoryListbox.Items.Add(i.name.ToUpper() + $"({time_difference.Hours}:{time_difference.Minutes}:{time_difference.Seconds})" + " | IsPreset: " + i.ispreset + " | " + i.workoutid);
                HistoryListbox.Items.Add("--------------------------------------------------------------------------");
                foreach (Exercise ex in ExerciseLibrary.ExerciseList)
                {
                    foreach (ExerciseData ed in ex.History)
                    {
                        if(ed.Workoutid - i.workoutid < 1 && ed.Workoutid - i.workoutid >= 0)
                        {
                            HistoryListbox.Items.Add(ex.Name + " | Reps: " + ed.Reps + " | Weight: " + ed.Weight + "kg");
                        }
                    }
                }

            }
        }

        private void HistoryListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HistoryListbox.SelectedIndex != -1)
            {
                string selectedItem = HistoryListbox.SelectedItem.ToString();
                string[] i1 = selectedItem.Split('|');
                if (i1.Length == 3)
                {
                    string Workoutid = i1[2].Trim();
                    int Id;
                    bool success = int.TryParse(Workoutid, out Id);
                    if (success)
                    {
                        foreach (WorkoutData w in ExerciseLibrary.Workoutlist)
                        {
                            if (w.workoutid == Id)
                            {
                                if (w.ispreset)
                                {
                                    w.ispreset = false;
                                }
                                else
                                {
                                    w.ispreset = true;
                                }
                            }
                        }
                    }
                }
            }
            WindowChange();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            HistoryListbox.Items.Clear();
            foreach (WorkoutData i in ExerciseLibrary.Workoutlist)
            {
                if (i.name.ToLower().Contains(textBox1.Text))
                {
                    HistoryListbox.Items.Add("--------------------------------------------------------------------------");
                    HistoryListbox.Items.Add(i.name.ToUpper() + " | IsPreset: " + i.ispreset + " | " + i.workoutid);
                    HistoryListbox.Items.Add("--------------------------------------------------------------------------");
                    foreach (Exercise ex in ExerciseLibrary.ExerciseList)
                    {
                        foreach (ExerciseData ed in ex.History)
                        {
                            if (ed.Workoutid - i.workoutid < 1 && ed.Workoutid - i.workoutid >= 0)
                            {
                                HistoryListbox.Items.Add(ex.Name + " | Reps: " + ed.Reps + " | Weight: " + ed.Weight + "kg");
                            }
                        }
                    }
                }

            }
        }
    }
}
