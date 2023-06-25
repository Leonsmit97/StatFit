using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace workoutTracker
{
   
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            TimeFrame.TimeRange = TimeFrame.range.month;
            UpdateTimeFrame();
            WindowManager.WindowChanged += UpdateInfo;
            RoundButton(yeer_select);
            RoundButton(month_select);
        }
        void UpdateInfo()
        {
            UpdatePieChart();
            UpdatePieChart1();
            UpdateMonthCountChart();
            UpdateStats();
            updatePR();
        }
        void updatePR()
        {
            int totaldays = (int)Math.Round((PersonalRecord.PrDate - DateTime.Now).TotalDays);
            if (totaldays > 10) topcirclepic.BringToFront();
            if (totaldays <= 10) middlepic.BringToFront();
            if (totaldays <= 1) bottomcirclepic.BringToFront();
            string message = $"Currently {totaldays} days out from personal record attempt will de load for 10 days and cease any high intensity exercise in {totaldays-10} days";
            if(totaldays <= 10)
            {
                message = $"Currently {totaldays} days out from personal record attempt, Should be deloading omiting any high intensity exercises";
            }
            if (totaldays <= 1)
            {
                message = $"Pr attempt is today";
            }
            label12.Text = message;
        }
        //this is the stats shown on top panel 
        void UpdateStats()
        {
            double total_weight = 0;
            int total_reps = 0;
            foreach (Exercise ex in ExerciseLibrary.ExerciseList)
            {
                foreach (ExerciseData ed in ex.History)
                {
                    total_weight += ed.Weight * ed.Reps;
                    total_reps += ed.Reps;
                }
            }
            TimeSpan total_time = new TimeSpan();
            foreach (WorkoutData ed in ExerciseLibrary.Workoutlist)
            {
                total_time += ed.end_time - ed.start_time;
            }
            tweight.Text = ConvertToReadableForm(total_weight);
            treps.Text = ConvertToReadableForm(total_reps);
            tworkouts.Text = ExerciseLibrary.Workoutlist.Count().ToString();
            timel.Text = total_time.Hours + "h" + total_time.Minutes + "m";
            CentreLabelx(tweight, weightL);
            CentreLabelx(timet, timel);
            CentreLabelx(treps, repsL);
            CentreLabelx(tworkouts, workoutsL);
        }
        void CentreLabelx(Label child, Label parent)
        {
            child.Location = new Point(parent.Location.X + parent.Width / 2 - child.Width / 2, child.Location.Y);
        }
        // convert numbers to like 12m or 12k
        public static string ConvertToReadableForm(double value)
        {
            if (value >= 1000000000)
            {
                return (value / 1000000000D).ToString("0.#") + "b";
            }
            else if (value >= 1000000)
            {
                return (value / 1000000D).ToString("0.#") + "m";
            }
            else if (value >= 1000)
            {
                return (value / 1000D).ToString("0.#") + "k";
            }
            else
            {
                return value.ToString();
            }
        }
        void UpdatePieChart()
        {
            double arms = 0, chest = 0, legs = 0, back= 0, shoulders = 0;
            foreach (Exercise ex in ExerciseLibrary.ExerciseList)
            {
                if(ex.MuscleGroup == "Arms")
                {
                    arms += ex.History.Count();
                }
                if (ex.MuscleGroup == "Shoulders")
                {
                    shoulders += ex.History.Count();
                }
                if (ex.MuscleGroup == "Legs")
                {
                    legs += ex.History.Count();
                }
                if (ex.MuscleGroup == "Chest")
                {
                    chest += ex.History.Count();
                }
                if (ex.MuscleGroup == "Back")
                {
                    back += ex.History.Count();
                }
            }
            chart2.Series["Series1"].Points.Clear();
            double sum = arms + shoulders + back + legs + chest;
            if (arms > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Arms(" + Math.Round(arms/sum*100) + "%)", arms);
            }
            if (shoulders > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Shoulders(" + Math.Round(shoulders / sum * 100) + "%)", shoulders);
            }
            if (back > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Back(" + Math.Round(back / sum * 100) + "%)", back);
            }
            if (legs > 0)
            { 
                chart2.Series["Series1"].Points.AddXY("Legs(" + Math.Round(legs / sum * 100) + "%)", legs);
            }
            if (chest > 0)
            {
                chart2.Series["Series1"].Points.AddXY("Chest(" + Math.Round(chest / sum * 100) + "%)", chest);
                
            }
            chart2.Series["Series1"].Label = "";
        }
        void UpdatePieChart1()
        {
            double arms = 0, chest = 0, legs = 0, back = 0, shoulders = 0;
            foreach (Exercise ex in ExerciseLibrary.ExerciseList)
            {
                if (ex.MuscleGroup == "Arms")
                {
                    arms += ex.History.Count();
                }
                if (ex.MuscleGroup == "Shoulders")
                {
                    shoulders += ex.History.Count();
                }
                if (ex.MuscleGroup == "Legs")
                {
                    legs += ex.History.Count();
                }
                if (ex.MuscleGroup == "Chest")
                {
                    chest += ex.History.Count();
                }
                if (ex.MuscleGroup == "Back")
                {
                    back += ex.History.Count();
                }
            }
            chart3.Series["Series1"].Points.Clear();
            double sum = arms + shoulders + back + legs + chest;
            if (arms > 0)
            {
                chart3.Series["Series1"].Points.AddXY("Arms(" + Math.Round(arms / sum * 100) + "%)", arms);
            }
            if (shoulders > 0)
            {
                chart3.Series["Series1"].Points.AddXY("Shoulders(" + Math.Round(shoulders / sum * 100) + "%)", shoulders);
            }
            if (back > 0)
            {
                chart3.Series["Series1"].Points.AddXY("Back(" + Math.Round(back / sum * 100) + "%)", back);
            }
            if (legs > 0)
            {
                chart3.Series["Series1"].Points.AddXY("Legs(" + Math.Round(legs / sum * 100) + "%)", legs);
            }
            if (chest > 0)
            {
                chart3.Series["Series1"].Points.AddXY("Chest(" + Math.Round(chest / sum * 100) + "%)", chest);

            }
            chart3.Series["Series1"].Label = "";
        }
        void UpdateMonthCountChart()
        {
             List<MonthCount> monthCounts = ExerciseLibrary.Workoutlist
            .GroupBy(date => new { date.end_time.Year, date.end_time.Month })
            .Select(group => new MonthCount
            {
                 Year = group.Key.Year,
                 Month = group.Key.Month,
                 Count = group.Count()

            }).ToList();
            double best = 0, count = 0, toatal = 0;
            foreach (MonthCount m in monthCounts)
            {
                if(m.Year == 2023)
                {
                    count++;
                    toatal += m.Count;
                    best = (m.Count > best) ? m.Count : best;
                    chart1.Series["w"].Points.AddXY(m.Month,m.Count);
                }
            }
            avg_label.Text = Math.Round(toatal/count).ToString();
            best_label.Text = best.ToString();
            CentreLabelx(avg_label, label17);
            CentreLabelx(best_label, label19);
        }
        void RoundButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 12, 12));
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        void UpdateTimeFrame()
        {
            if (TimeFrame.TimeRange == TimeFrame.range.year)
            {
                UpdateTimeFrameText("Yearly");
                TimeRangeLabel.Location = new Point(355, 60);
            }
            if (TimeFrame.TimeRange == TimeFrame.range.month)
            {
                UpdateTimeFrameText("Monthly");
                TimeRangeLabel.Location = new Point(355, 48);
            }
        }
        void UpdateTimeFrameText(string text)
        {
            TimeRangeLabel.Text = text;
        }

        private void yeer_select_Click(object sender, EventArgs e)
        {
            TimeFrame.TimeRange = TimeFrame.range.year;
            UpdateTimeFrame();
        }

        private void month_select_Click(object sender, EventArgs e)
        {
            TimeFrame.TimeRange = TimeFrame.range.month;
            UpdateTimeFrame();
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            string new_username= Microsoft.VisualBasic.Interaction.InputBox("Enter your username:", "Change Username");
            label1.Text = (new_username != "") ? new_username : label1.Text;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.personalrecord;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.personalrecord;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            WindowManager.CurrentWindow = WindowManager.Window.personalrecord;
        }
    }
    public class MonthCount
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
    }
}
