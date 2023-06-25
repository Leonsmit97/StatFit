using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace workoutTracker
{
    public partial class workoutCreator : UserControl
    {
        public workoutCreator()
        {
            InitializeComponent();
        }
        public static Exercise exer = null;
        public static WindowManager.Window Return_Window = WindowManager.Window.createworkout;
        private static DateTime Start_time;
        private static bool IsFirst = false;
        private void workoutCreator_Load(object sender, EventArgs e)
        {
            ExerciseSelector.ExerciseSelected += addcontrol;
            Workout.PresetExerciseAdd += addcontrol;
            WindowManager.WindowChanged += SetTime;
            RoundButton(button1);
            RoundButton(button2);
            RoundButton(button3);
            RoundButton(button4);
            RoundPanel(panel1);
            RoundPanel(panel2);
        }
        void RoundPanel(Panel btn)
        { 
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 12, 12));
        }
        void RoundButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 12, 12));
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        void SetTime()
        {
            //Return_Window = WindowManager.Window.createworkout;
            if (!IsFirst)
            {
                Start_time = DateTime.Now;
                IsFirst = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Return_Window = WindowManager.Window.createworkout;
            WindowManager.CurrentWindow = WindowManager.Window.exerciseselector;
        }
        List<Control> copycontrols = new List<Control>();
        void addcontrol()
        {
            if (Return_Window != WindowManager.Window.createworkout) return;
            if (exer == null) return;
            Panel originalPanel = ExerPanel;

            Panel newPanel = new Panel();
            
            newPanel.Size = originalPanel.Size;
            newPanel.Location = originalPanel.Location;
            newPanel.BackColor = originalPanel.BackColor;
            newPanel.BorderStyle = originalPanel.BorderStyle;
            newPanel.Name = originalPanel.Name + "_copy";
            RoundPanel(newPanel);
            bool istitle = false;
            foreach (Control control in originalPanel.Controls)
            {
                istitle = (control.Name == "title") ? true : false;
                Control newControl = (Control)Activator.CreateInstance(control.GetType());
                newControl.Location = control.Location;

                newControl.BackColor = control.BackColor;
                newControl.ForeColor = control.ForeColor;
                if (!istitle)
                {
                    newControl.Size = control.Size;
                    newControl.Text = control.Text;
                }
                else
                {
                    Label titleLabel = (Label)newControl; // assuming the title control is a Label
                    titleLabel.AutoSize = true;
                    
                    titleLabel.Text = exer.Name; 
                    newControl = titleLabel;
                    istitle = false;

                }
                newControl.Font = control.Font;
                if(newControl is TextBox)
                {
                    TextBox t = (TextBox)newControl;
                    t.BorderStyle = BorderStyle.FixedSingle;
                }
                newPanel.Controls.Add(newControl);
            }
            copycontrols.Add(newPanel);
            flowLayoutPanel1.Controls.Add(newPanel);

        }
        //Done Button
        //add stops for if theres no input
        void SaveWorkout(bool ispreset)
        {
            List<Panel> panels = new List<Panel>();
            WorkoutManager.NewWorkout();
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is Panel)
                {
                    panels.Add((Panel)c);
                }
            }
            foreach (Panel p in panels)
            {
                int textboxIncrement = 0;
                foreach (Control c in p.Controls)
                {
                    if (c is Label)
                    {
                        if (c.Text != "Weight:" && c.Text != "Reps:")
                        {
                            foreach (Exercise ex in ExerciseLibrary.ExerciseList)
                            {
                                if (ex.Name == c.Text)
                                {
                                    double weight = 0;
                                    int reps = 0;
                                    foreach (Control c1 in p.Controls)
                                    {
                                        if (c1 is TextBox && textboxIncrement == 1)
                                        {
                                            weight += Convert.ToDouble(c1.Text);
                                            textboxIncrement++;
                                        }
                                        if (c1 is TextBox && textboxIncrement == 0)
                                        {
                                            reps += Convert.ToInt32(c1.Text);
                                            textboxIncrement++;
                                        }

                                    }
                                    DateTime today = DateTime.Now;
                                    WorkoutManager.CurrentWorkout += 0.01;
                                    ex.AddData(new ExerciseData(reps, weight, today, WorkoutManager.CurrentWorkout));
                                }
                            }
                        }
                    }
                }
            }
            ExerciseLibrary.Workoutlist.Add(new WorkoutData(textBox1.Text, textBox2.Text, Math.Floor(WorkoutManager.CurrentWorkout), ispreset, Start_time, DateTime.Now));
            IsFirst = false;
            foreach (Control c in copycontrols)
            {
                if (c is Panel)
                {
                    flowLayoutPanel1.Controls.Remove(c);
                }
            }
            WindowManager.CurrentWindow = WindowManager.Window.Workout;
            copycontrols.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveWorkout(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveWorkout(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control c in copycontrols)
            {
                if (c is Panel)
                {
                    flowLayoutPanel1.Controls.Remove(c);
                }
            }
            WindowManager.CurrentWindow = WindowManager.Window.Workout;
            copycontrols.Clear();
        }
    }
}
