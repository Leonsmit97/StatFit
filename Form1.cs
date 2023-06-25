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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 20;
            timer1.Start();
            WindowManager.LoadData();
            WindowManager.CurrentWindow = WindowManager.Window.Home;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BringToFrontt();
        }
        
        void BringToFrontt()
        {
            if (WindowManager.CurrentWindow == WindowManager.Window.Home) home1.BringToFront();
            if (WindowManager.CurrentWindow == WindowManager.Window.Workout) workout1.BringToFront();
            if (WindowManager.CurrentWindow == WindowManager.Window.CreateExercise) createExercise1.BringToFront();
            if (WindowManager.CurrentWindow == WindowManager.Window.History) workoutHistory1.BringToFront();
            if (WindowManager.CurrentWindow == WindowManager.Window.createworkout) workoutCreator1.BringToFront();
            if (WindowManager.CurrentWindow == WindowManager.Window.exerciseselector)
            {
                exerciseSelector1.BringToFront();
            }
            if (WindowManager.CurrentWindow == WindowManager.Window.workoutselector) workoutSelector1.BringToFront();
            if (WindowManager.CurrentWindow == WindowManager.Window.personalrecord) personalRecord1.BringToFront();
            if (WindowManager.CurrentWindow == WindowManager.Window.Stats) stats1.BringToFront();

        }
    }
}
