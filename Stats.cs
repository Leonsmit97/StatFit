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
using System.Windows.Forms.DataVisualization.Charting;

namespace workoutTracker
{
    public partial class Stats : UserControl
    {
        public Stats()
        {
            InitializeComponent();
        }

        private void Stats_Load(object sender, EventArgs e)
        {
            Update_Alignment();
            WindowManager.WindowChanged += update_Return;
            ExerciseSelector.ExerciseSelected += Choose_Exercise;
        }

        private void update_Return()
        {
            //workoutCreator.Return_Window = WindowManager.Window.Stats;
        }

        void Update_Alignment()
        {
            CentreLabelx(label1, pictureBox1);
            CentreLabelx(label3, pictureBox10);
            CentreLabelx(panel6, pictureBox5);
            CentreLabelx(panel7, pictureBox5);
            CentreLabelx(label14, pictureBox5);
            CentreLabelx(avg_set_weight_label, pictureBox3);
            CentreLabelx(avrageSet_reps_label, pictureBox3);
            CentreLabelx(label13, pictureBox5);
            CentreLabelx(label19, pictureBox8);

            Roundrect(panel1);
            Roundrect(panel2);
            Roundrect(panel3);
            Roundrect(panel4);
            Roundrect(panel5);
            Roundrect(panel6);
            Roundrect(panel7);
            Roundrect(panel8);
            Roundrect(panel9);
        }
        #region Windows

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
        #endregion
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

        void CentreLabelx(Control child, Control parent)
        {
            child.Location = new Point(parent.Location.X + parent.Width / 2 - child.Width / 2, child.Location.Y);
        }
        Exercise e;
        void Choose_Exercise()
        {
            if (workoutCreator.Return_Window != WindowManager.Window.Stats) return;
            e = workoutCreator.exer;
            label1.Text = e.Name;

            //Top Set Calculations
            double TopWeight = 0;
            double TopReps = 0;
            foreach (ExerciseData i in e.History)
            {
                if (i.Weight > TopWeight) TopWeight = i.Weight;
            }
            foreach (ExerciseData i in e.History)
            {
                if (i.Weight == TopWeight)
                {
                    if (i.Reps > TopReps) TopReps = i.Reps;
                }

            }
            label3.Text = TopWeight + "kg X " + TopReps;
            //------------------------------------
            chart1.Series["w"].Points.Clear();
            chart1.Series["bestfit"].Points.Clear();


            double total = 0;
            int entries = 0;
            List<Point> WeightByMonth = new List<Point>();
            for (int i = 1; i < 13; i++)
            {
                foreach (ExerciseData p in e.History)
                {
                    if (p.Date.Month == i)
                    {
                        total += p.Weight;
                        entries++;
                    }
                }
                if (entries > 0 && total > 0)
                {
                    WeightByMonth.Add(new Point(i, (int)Math.Round(total / entries)));
                }
                else
                {
                    WeightByMonth.Add(new Point(i, 0));
                }
                total = 0;
                entries = 0;
            }
            for (int i = WeightByMonth.Count - 1; i > -1; i--)
            {
                if (WeightByMonth[i].Y == 0)
                {
                    WeightByMonth.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
            foreach (Point i in WeightByMonth)
            {
                chart1.Series["w"].Points.AddXY(i.X, i.Y);
            }

            //---------------------------------------------
            #region day of week ting
            double mon = 0, teu = 0, wed = 0, thu = 0, fri = 0, sat = 0, sun = 0;
            foreach (ExerciseData ex in e.History)
            {
                if (ex.Date.DayOfWeek == DayOfWeek.Monday)
                {
                    mon++;
                }
                if (ex.Date.DayOfWeek == DayOfWeek.Tuesday)
                {
                    teu++;
                }
                if (ex.Date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    wed++;
                }
                if (ex.Date.DayOfWeek == DayOfWeek.Thursday)
                {
                    thu++;
                }
                if (ex.Date.DayOfWeek == DayOfWeek.Friday)
                {
                    fri++;
                }
                if (ex.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    sat++;
                }
                if (ex.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    sun++;
                }
            }
            chart2.Series["Series1"].Points.Clear();

            if (mon > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Monday", mon);
            }
            if (teu > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Teusday", teu);
            }
            if (wed > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Wednesday", wed);
            }
            if (thu > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Thursday", thu);
            }
            if (fri > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Friday", fri);

            }
            if (sat > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Saturday", sat);

            }
            if (sun > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Sunday", sun);

            }
            chart2.Series["Series1"].Label = "";
            #endregion
            //---------------------------------------------
            double a, b;
            List<Point> bestFit = GenerateLinearBestFit(WeightByMonth, out a, out b);
            foreach (Point p in bestFit)
            {
                chart1.Series["bestfit"].Points.AddXY(p.X, p.Y);
            }
            int y = WeightByMonth.Count + 1;
            for (int i = y; i < 13; i++)
            {
                chart1.Series["bestfit"].Points.AddXY(i, a * i - b);
            }
            //Calculating the arrow stats on stat page
            int total_reps = 0;
            double total_weight = 0;
            int total_entries = 0;
            DateTime lasttrained = DateTime.Now;
            //THIS NEEDS TO BE FIXED BEFORE UPLOADING TO USB CHANGE PATHS
            string DownArrow = @"C:\Users\Leon\source\repos\workoutTracker\workoutTracker\images\Stats_Arrow_down.png";
            string UpArrow = @"C:\Users\Leon\source\repos\workoutTracker\workoutTracker\images\Stats_Arrow_up.png";
            foreach (ExerciseData i in e.History)
            {
                total_reps += i.Reps;
                total_weight += i.Weight;
                total_entries++;
                lasttrained = i.Date;
            }
            reps_label.Text = "Reps: " + total_reps.ToString();
            avg_label.Text = "AVG Weight: " + Math.Round(total_weight / total_entries).ToString() + "kg";
            Avg_Weight_Pic.Image = new Bitmap((Is_Previous_AVG_Weight_Bigger(WeightByMonth, Math.Round(total_weight / total_entries)) ? DownArrow : UpArrow));
            max_weight_label.Text = "Max Weight: " + TopWeight + "kg";
            max_weight_pic.Image = new Bitmap((Current_Max_Weight_This_Month() == TopWeight) ? UpArrow : DownArrow);
            LastTrained_label.Text = "Last Trained: " + (DateTime.Today - lasttrained).Days + "d";
            DateTime dt1 = DateTime.Now;
            int CurrentMonth1 = dt1.Month;
            double PercentImporv = Convert.ToDouble(WeightByMonth[CurrentMonth1 - 1].Y - WeightByMonth[CurrentMonth1 - 2].Y) / WeightByMonth[CurrentMonth1 - 2].Y * 100;
            Mom_Imporv_label.Text = "MoM Improvement: " + Math.Round(PercentImporv, 2) + "%";
            //avrage set calculations
            double t_weight = 0;
            double t_reps = 0;
            int Number_of_entries = 0;
            foreach (ExerciseData i in e.History)
            {
                t_weight += i.Weight;
                t_reps += i.Reps;
                Number_of_entries++;
            }
            avg_set_weight_label.Text = Math.Round(t_weight / Number_of_entries).ToString() + "kg";
            avrageSet_reps_label.Text = Math.Round(t_reps / Number_of_entries).ToString();
            //---------------------------------------------
            Update_Alignment();
        }
        
        double Current_Max_Weight_This_Month()
        {
            DateTime dt = DateTime.Now;
            int CurrentMonth = dt.Month;
            double max = 0;
            for (int i = 1; i < 13; i++)
            {
                foreach (ExerciseData p in e.History)
                {
                    if (p.Date.Month == CurrentMonth)
                    {
                        max = (p.Weight > max) ? p.Weight : max;
                    }
                }
            }
            return max;
        }
    
        bool Is_Previous_AVG_Weight_Bigger(List<Point> AVG_Per_Month, double Current_AVG)
        {
            DateTime dt = DateTime.Now;
            int CurrentMonth = dt.Month;
            int count = 0;
            int total = 0;
            foreach (Point p in AVG_Per_Month)
            {
                if (p.X >= CurrentMonth) break;
                if(p.Y != 0 ) count++;
                total += p.Y;
            }
            if (count == 0) return false;
            return ((total/count) > Current_AVG) ? true : false;
        }
        public static List<Point> GenerateLinearBestFit(List<Point> points, out double a, out double b)
        {
            int numPoints = points.Count;
            double meanX = points.Average(point => point.X);
            double meanY = points.Average(point => point.Y);
            double sumXSquared = points.Sum(point => point.X * point.X);
            double sumXY = points.Sum(point => point.X * point.Y);
            a = (sumXY / numPoints - meanX * meanY) / (sumXSquared / numPoints - meanX * meanX);
            b = (a * meanX - meanY);
            double a1 = a;
            double b1 = b;
            return points.Select(point => new Point() { X = point.X, Y = Convert.ToInt32(a1 * point.X - b1)}).ToList();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            select_exercise();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            select_exercise();
        }
        void select_exercise()
        {
            WindowManager.CurrentWindow = WindowManager.Window.exerciseselector;
            workoutCreator.Return_Window = WindowManager.Window.Stats;
        }
        void showHistory()
        {
            string history = e.Name + "\n";
            foreach (ExerciseData i in e.History)
            {
                history += i.Weight + "kg, " + i.Reps + ", " + i.Date + "\n";
            }
            MessageBox.Show(history);
        }
        private void label19_Click(object sender, EventArgs e)
        {
            showHistory();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel9_MouseClick(object sender, MouseEventArgs e)
        {
            showHistory();
        }
    }
}
