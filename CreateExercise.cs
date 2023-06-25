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
    public partial class CreateExercise : UserControl
    {
        public CreateExercise()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool exists = false;
            foreach(Exercise ex in ExerciseLibrary.ExerciseList)
            {
                if (ex.Name == textBox1.Text) exists = true;
            }
            if (!exists)
            {
                new Exercise(comboBox1.Text, textBox1.Text, comboBox2.Text);
                WindowManager.CurrentWindow = WindowManager.Window.Workout;
            }
            else
            {
                MessageBox.Show("Exercise Allready Exists");
            }  
        }

        private void CreateExercise_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.Workout;
        }
    }
}
