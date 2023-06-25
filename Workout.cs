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
    public partial class Workout : UserControl
    {
        public Workout()
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

        private void Add_Preset_Button_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.workoutselector;
        }
        private void PresetButton(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            //MessageBox.Show(b.Text);
            WindowManager.CurrentWindow = WindowManager.Window.createworkout;
            foreach (WorkoutData i in ExerciseLibrary.Workoutlist)
            {
                if (i.workoutid == Convert.ToInt32(b.Name))
                {
                    foreach (Exercise ex in ExerciseLibrary.ExerciseList)
                    {
                        
                        foreach (ExerciseData d in ex.History)
                        {
                            if (d.Workoutid - i.workoutid < 1 && d.Workoutid - i.workoutid >= 0)
                            {
                               // MessageBox.Show("s");
                                workoutCreator.Return_Window = WindowManager.Window.createworkout;
                                workoutCreator.exer = ex;
                                PresetExerciseAdd?.Invoke();
                                
                            }
                        }
                    }
                   
                }
            }
            
            
        }
        public delegate void MyEventHandler1();
        public static event MyEventHandler1 PresetExerciseAdd;
        private void Add_Exercisebtn_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.CreateExercise;
        }

        private void Workout_Load(object sender, EventArgs e)
        {
            WindowManager.WindowChanged += UpdateList;
            RoundButton(button1);
            RoundButton(Add_Preset_Button);
        }
        void RoundButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 12, 12));
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        
        void UpdatePreset()
        {
            for (int i = workout_preset_parent.Controls.Count - 1; i >= 0; i--)
            {
                Control c = workout_preset_parent.Controls[i];
                if (c.Name != "Add_Preset_Button")
                {
                    workout_preset_parent.Controls.RemoveAt(i);
                }
            }
            foreach (WorkoutData i in ExerciseLibrary.Workoutlist)
            {
                if (i.ispreset)
                {
                    Button myButton = new Button();
                    RoundButton(myButton);
                    myButton.Text = i.name;
                    myButton.Name = i.workoutid.ToString();
                    myButton.Location = new Point(10, 10);
                    myButton.FlatStyle = FlatStyle.Flat;
                    myButton.ForeColor = Color.FromArgb(113, 113, 113);
                    myButton.Click += new EventHandler(PresetButton);
                    myButton.BackColor = Color.FromArgb(52, 53, 63);
                    workout_preset_parent.Controls.Add(myButton);
                }
            }
        }
        private void UpdateList()
        {
            UpdatePreset();
            listBox1.Items.Clear();
            foreach (Exercise i in ExerciseLibrary.ExerciseList)
            {
                int tReps = 0;
                double tWeight = 0;
                foreach (ExerciseData d in i.History)
                {
                    tWeight += d.Weight;
                    tReps += d.Reps;
                }
                listBox1.Items.Add(i.Name.ToUpper());
                listBox1.Items.Add("-----------------------------------------------------------------------------------------------------------------");
                listBox1.Items.Add(i.Type + " | " + i.MuscleGroup + " | Total Reps: " + tReps + " | Total Weight: " + tWeight);
                listBox1.Items.Add("-----------------------------------------------------------------------------------------------------------------");
            }
            WindowManager.SaveData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Exercise i in ExerciseLibrary.ExerciseList)
            {
                if(musclefilter.Text == i.MuscleGroup)
                {
                    int tReps = 0;
                    double tWeight = 0;
                    foreach (ExerciseData d in i.History)
                    {
                        tWeight += d.Weight;
                        tReps += d.Reps;
                    }
                    listBox1.Items.Add(i.Name.ToUpper());
                    listBox1.Items.Add("-----------------------------------------------------------------------------------------------------------------");
                    listBox1.Items.Add(i.Type + " | " + i.MuscleGroup + " | Total Reps: " + tReps + " | Total Weight: " + tWeight);
                    listBox1.Items.Add("-----------------------------------------------------------------------------------------------------------------");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.createworkout;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex != -1)
            {
                string selectedItem = listBox1.SelectedItem.ToString();
                
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.createworkout;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.createworkout;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.createworkout;
        }
    }
}
