
namespace workoutTracker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.personalRecord1 = new workoutTracker.PersonalRecord();
            this.workoutSelector1 = new workoutTracker.WorkoutSelector();
            this.exerciseSelector1 = new workoutTracker.ExerciseSelector();
            this.workoutCreator1 = new workoutTracker.workoutCreator();
            this.workoutHistory1 = new workoutTracker.WorkoutHistory();
            this.home1 = new workoutTracker.Home();
            this.workout1 = new workoutTracker.Workout();
            this.createExercise1 = new workoutTracker.CreateExercise();
            this.stats1 = new workoutTracker.Stats();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // personalRecord1
            // 
            this.personalRecord1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(12)))));
            this.personalRecord1.Location = new System.Drawing.Point(0, 0);
            this.personalRecord1.Name = "personalRecord1";
            this.personalRecord1.Size = new System.Drawing.Size(414, 901);
            this.personalRecord1.TabIndex = 7;
            // 
            // workoutSelector1
            // 
            this.workoutSelector1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(12)))));
            this.workoutSelector1.Location = new System.Drawing.Point(74, 134);
            this.workoutSelector1.Name = "workoutSelector1";
            this.workoutSelector1.Size = new System.Drawing.Size(260, 369);
            this.workoutSelector1.TabIndex = 6;
            // 
            // exerciseSelector1
            // 
            this.exerciseSelector1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(12)))));
            this.exerciseSelector1.Location = new System.Drawing.Point(74, 134);
            this.exerciseSelector1.Name = "exerciseSelector1";
            this.exerciseSelector1.Size = new System.Drawing.Size(260, 369);
            this.exerciseSelector1.TabIndex = 5;
            // 
            // workoutCreator1
            // 
            this.workoutCreator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(12)))));
            this.workoutCreator1.Location = new System.Drawing.Point(0, 0);
            this.workoutCreator1.Name = "workoutCreator1";
            this.workoutCreator1.Size = new System.Drawing.Size(414, 901);
            this.workoutCreator1.TabIndex = 4;
            // 
            // workoutHistory1
            // 
            this.workoutHistory1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(12)))));
            this.workoutHistory1.Location = new System.Drawing.Point(0, 0);
            this.workoutHistory1.Name = "workoutHistory1";
            this.workoutHistory1.Size = new System.Drawing.Size(414, 901);
            this.workoutHistory1.TabIndex = 3;
            // 
            // home1
            // 
            this.home1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(12)))));
            this.home1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.home1.Location = new System.Drawing.Point(-1, -1);
            this.home1.Name = "home1";
            this.home1.Size = new System.Drawing.Size(423, 901);
            this.home1.TabIndex = 0;
            // 
            // workout1
            // 
            this.workout1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(12)))));
            this.workout1.Location = new System.Drawing.Point(0, 0);
            this.workout1.Name = "workout1";
            this.workout1.Size = new System.Drawing.Size(414, 901);
            this.workout1.TabIndex = 1;
            // 
            // createExercise1
            // 
            this.createExercise1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(12)))));
            this.createExercise1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.createExercise1.Location = new System.Drawing.Point(83, 275);
            this.createExercise1.Name = "createExercise1";
            this.createExercise1.Size = new System.Drawing.Size(239, 228);
            this.createExercise1.TabIndex = 2;
            // 
            // stats1
            // 
            this.stats1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(10)))), ((int)(((byte)(12)))));
            this.stats1.Location = new System.Drawing.Point(0, 0);
            this.stats1.Name = "stats1";
            this.stats1.Size = new System.Drawing.Size(414, 901);
            this.stats1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 899);
            this.Controls.Add(this.stats1);
            this.Controls.Add(this.personalRecord1);
            this.Controls.Add(this.workoutSelector1);
            this.Controls.Add(this.exerciseSelector1);
            this.Controls.Add(this.workoutCreator1);
            this.Controls.Add(this.workoutHistory1);
            this.Controls.Add(this.home1);
            this.Controls.Add(this.workout1);
            this.Controls.Add(this.createExercise1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Home home1;
        private Workout workout1;
        private System.Windows.Forms.Timer timer1;
        private CreateExercise createExercise1;
        private WorkoutHistory workoutHistory1;
        private workoutCreator workoutCreator1;
        private ExerciseSelector exerciseSelector1;
        private WorkoutSelector workoutSelector1;
        private PersonalRecord personalRecord1;
        private Stats stats1;
    }
}

