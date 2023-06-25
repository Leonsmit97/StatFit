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
    public partial class PersonalRecord : UserControl
    {
        public PersonalRecord()
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
        void CentreLabelx(Control child, Control parent)
        {
            child.Location = new Point(parent.Location.X + parent.Width / 2 - child.Width / 2, child.Location.Y);
        }
        void createSpacey(Control child, Control parent, int space)
        {
            child.Location = new Point(child.Location.X, parent.Location.Y + parent.Height + space);
        }
        void Roundrect(Panel btn)
        {
            btn.BorderStyle = 0;
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
        private List<Control> controls = new List<Control>();
        private void PersonalRecord_Load(object sender, EventArgs e)
        {
            WindowManager.WindowChanged += UpdateValues;
            //panel allignment
            int space = 12;
            createSpacey(panel1, pictureBox1, space);
            createSpacey(panel2, panel1, space);
            createSpacey(panel3, panel2, space);
            createSpacey(panel4, panel3, space);
            //---------------
            Roundrect(panel1);
            Roundrect(panel2);
            Roundrect(panel3);
            Roundrect(panel4);
            CentreLabelx(label1, pictureBox2);
            CentreLabelx(label2, label1);
            //3rd panel allignment
            CentreLabelx(label3, pictureBox7);
            CentreLabelx(label8, pictureBox12);
            CentreLabelx(label4, pictureBox7);
            CentreLabelx(label5, pictureBox7);
            CentreLabelx(label9, pictureBox7);
            CentreLabelx(label7, pictureBox7);
            CentreLabelx(pictureBox8, pictureBox7);
            CentreLabelx(pictureBox9, pictureBox7);
            CentreLabelx(pictureBox10, pictureBox7);
            CentreLabelx(pictureBox11, pictureBox7);
            CentreLabelx(button1, pictureBox7);
            CentreLabelx(button2, pictureBox7);
            CentreLabelx(button3, pictureBox7);
            CentreLabelx(button4, pictureBox7);
            createSpacey(pictureBox8, label3, 9);
            createSpacey(label4, pictureBox8, 6);
            createSpacey(button1, label4, 3);
            createSpacey(pictureBox9, button1, 9);
            createSpacey(label5, pictureBox9, 6);
            createSpacey(button2, label5, 3);
            createSpacey(pictureBox10, button2, 9);
            createSpacey(label9, pictureBox10, 6);
            createSpacey(button3, label9, 3);
            createSpacey(pictureBox11, button3, 9);
            createSpacey(label7, pictureBox11, 6);
            createSpacey(button4, label7, 3);
            //adding to list
            controls.AddRange(new Control[] { button1, button2, button3, button4, label3, label4, label5, label9, label7, pictureBox8, pictureBox9, pictureBox10, pictureBox11 });
            //date times 

            dateTimePicker1.Hide();
            label10.Hide();
            button5.Hide();
            CentreLabelx(dateTimePicker1, pictureBox7);
            CentreLabelx(label10, pictureBox7);
            CentreLabelx(button5, pictureBox7);
            //Round buttons
            RoundButton(button1);
            RoundButton(button2);
            RoundButton(button3);
            RoundButton(button4);
            //add event handler
            ExerciseSelector.ExerciseSelected += addExercise;
        }
        public static string ExerName = "";
        public static double GoalWeight = 0;
        public static double CurrentWeight = 0;
        public static DateTime PrDate;
        public static DateTime currentDate = DateTime.Now;
        void UpdateValues()
        {
            button1.Text = ExerName;
            button2.Text = CurrentWeight.ToString() + "kg";
            button3.Text = GoalWeight.ToString() + "kg";
            button4.Text = PrDate.Date.ToString();
        }
        //save
        private void panel4_Click(object sender, EventArgs e)
        {
            if (button1.Text != "Select" && button2.Text != "Select" && button3.Text != "Select" && button4.Text != "Select")
            {
                WindowManager.CurrentWindow = WindowManager.Window.Home;
            }
            else
            {
                MessageBox.Show("Fill Out All Information");
                WindowManager.SaveData();
            }
        }
        //save
        private void label8_Click(object sender, EventArgs e)
        {
            if (button1.Text != "Select" && button2.Text != "Select" && button3.Text != "Select" && button4.Text != "Select")
            {
                WindowManager.CurrentWindow = WindowManager.Window.Home;
                WindowManager.SaveData();
            }
            else
            {
                MessageBox.Show("Fill Out All Information");
            }
        }
        Exercise prExercise = null;
        private void button1_Click(object sender, EventArgs e)
        {
            workoutCreator.Return_Window = WindowManager.Window.personalrecord;
            WindowManager.CurrentWindow = WindowManager.Window.exerciseselector;
        }
        private void addExercise()
        {
            //selecting exercise to pr in
            prExercise = workoutCreator.exer;
            button1.Text = prExercise.Name;
            double best = 0;
            foreach (ExerciseData ed in prExercise.History)
            {
                if (ed.Weight > best) best = ed.Weight;
            }
            //setting current best weight
            button2.Text = best.ToString() + "kg";
            CurrentWeight = best;
            ExerName = button1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string GoalWeight1 = Microsoft.VisualBasic.Interaction.InputBox("Enter Goal Weight:", "Goal Weight");
            button3.Text = (GoalWeight1 != "") ? GoalWeight1 + "kg" : button3.Text;
            GoalWeight = Convert.ToDouble(GoalWeight1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control c in controls)
            {
                c.Hide();
            }
            dateTimePicker1.Show();
            label10.Show();
            button5.Show();
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Hide();
            label10.Hide();
            button5.Hide();
            foreach (Control c in controls)
            {
                c.Show();
            }
            PrDate = dateTimePicker1.Value;
            button4.Text = PrDate.Date.ToString();
            TimeSpan duration = PrDate - currentDate;
            double totaldays = Math.Round(duration.TotalDays);
            label2.Text = totaldays.ToString();
            if (totaldays > 10)
            {
                pictureBox4.Location = new Point(132, 19);
            }
            if (totaldays <= 10)
            {
                pictureBox4.Location = new Point(281, 19);
            }
            if (totaldays <= 1)
            {
                pictureBox4.Location = new Point(336, 19);
            }
        }
    }
}
