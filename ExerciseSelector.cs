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
    public partial class ExerciseSelector : UserControl
    {
        public ExerciseSelector()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string selectedItem = listBox1.SelectedItem.ToString();
                string[] i1 = selectedItem.Split('|');
                string name = i1[0].Trim();
                foreach (Exercise i in ExerciseLibrary.ExerciseList)
                {
                    if (i.Name == name)
                    {
                        //fix so other tings can use this without invoking shit
                        workoutCreator.exer = i;
                        WindowManager.CurrentWindow = workoutCreator.Return_Window;
                        ExerciseSelected?.Invoke();
                    }
                }
            }
        }
        public void ExerciseSelector_Load(object sender, EventArgs e)
        {
            UpdateList();
            WindowManager.WindowChanged += UpdateList;
        }
        public delegate void MyEventHandler1();
        public static event MyEventHandler1 ExerciseSelected;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Exercise i in ExerciseLibrary.ExerciseList)
            {
                string LowerName = i.Name.ToLower();
                if (LowerName.Contains(textBox1.Text) || i.MuscleGroup.Contains(textBox1.Text))
                {
                    listBox1.Items.Add(i.Name + " | " + i.MuscleGroup);
                }
            }
        }
        void UpdateList()
        {
            listBox1.Items.Clear();
            foreach (Exercise i in ExerciseLibrary.ExerciseList)
            {
                listBox1.Items.Add(i.Name + " | " + i.MuscleGroup);
            }
        }
    }
}
