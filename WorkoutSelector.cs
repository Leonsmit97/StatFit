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
    public partial class WorkoutSelector : UserControl
    {
        public WorkoutSelector()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updatelist();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                string selectedItem = listBox1.SelectedItem.ToString();
                string[] i1 = selectedItem.Split('|');
                string name = i1[0].Trim();

                foreach(WorkoutData w in ExerciseLibrary.Workoutlist)
                {
                    if (w.name.ToLower() == name.ToLower() && w.workoutid == Convert.ToInt32(i1[2].Trim()))
                    {
                        w.ispreset = true;
                    }
                }
            }
            WindowManager.CurrentWindow = WindowManager.Window.Workout;
        }
        void updatelist()
        {
            listBox1.Items.Clear();
            foreach (WorkoutData w in ExerciseLibrary.Workoutlist)
            {
                if (w.name.Contains(textBox1.Text))
                {
                    listBox1.Items.Add(w.name + " | " + w.notes + " | " + w.workoutid);
                }
            }
        }
        private void WorkoutSelector_Load(object sender, EventArgs e)
        {
            WindowManager.WindowChanged += updatelist;
        }
    }
}
