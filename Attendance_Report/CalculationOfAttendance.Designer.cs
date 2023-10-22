namespace Attendance_Report
{
    partial class CalculationOfAttendance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculationOfAttendance));
            this.CB_Student = new System.Windows.Forms.ComboBox();
            this.CB_Group = new System.Windows.Forms.ComboBox();
            this.DATA = new System.Windows.Forms.DataGridView();
            this.TB_StartDate = new System.Windows.Forms.TextBox();
            this.TB_EndDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CB_AttendanceMark = new System.Windows.Forms.ComboBox();
            this.Refresh_Button = new System.Windows.Forms.PictureBox();
            this.Lab_Out = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DATA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Refresh_Button)).BeginInit();
            this.SuspendLayout();
            // 
            // CB_Student
            // 
            this.CB_Student.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_Student.FormattingEnabled = true;
            this.CB_Student.Location = new System.Drawing.Point(321, 72);
            this.CB_Student.Margin = new System.Windows.Forms.Padding(23, 15, 15, 15);
            this.CB_Student.Name = "CB_Student";
            this.CB_Student.Size = new System.Drawing.Size(493, 31);
            this.CB_Student.TabIndex = 5;
            this.CB_Student.Text = "Студент";
            this.CB_Student.SelectedIndexChanged += new System.EventHandler(this.CB_Student_SelectedIndexChanged);
            // 
            // CB_Group
            // 
            this.CB_Group.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_Group.FormattingEnabled = true;
            this.CB_Group.Location = new System.Drawing.Point(24, 72);
            this.CB_Group.Margin = new System.Windows.Forms.Padding(23, 15, 15, 15);
            this.CB_Group.Name = "CB_Group";
            this.CB_Group.Size = new System.Drawing.Size(259, 31);
            this.CB_Group.TabIndex = 3;
            this.CB_Group.Text = "Группа";
            this.CB_Group.SelectedIndexChanged += new System.EventHandler(this.CB_Group_SelectedIndexChanged);
            // 
            // DATA
            // 
            this.DATA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DATA.Location = new System.Drawing.Point(12, 12);
            this.DATA.Name = "DATA";
            this.DATA.Size = new System.Drawing.Size(10, 10);
            this.DATA.TabIndex = 6;
            this.DATA.Visible = false;
            // 
            // TB_StartDate
            // 
            this.TB_StartDate.Location = new System.Drawing.Point(130, 31);
            this.TB_StartDate.Name = "TB_StartDate";
            this.TB_StartDate.Size = new System.Drawing.Size(259, 26);
            this.TB_StartDate.TabIndex = 7;
            // 
            // TB_EndDate
            // 
            this.TB_EndDate.Location = new System.Drawing.Point(555, 31);
            this.TB_EndDate.Name = "TB_EndDate";
            this.TB_EndDate.Size = new System.Drawing.Size(259, 26);
            this.TB_EndDate.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Дата начала";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(417, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "Дата окончания";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Выполнить расчет для";
            // 
            // CB_AttendanceMark
            // 
            this.CB_AttendanceMark.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_AttendanceMark.FormattingEnabled = true;
            this.CB_AttendanceMark.Items.AddRange(new object[] {
            "Отсутствовал",
            "Опоздал",
            "Присутствовал"});
            this.CB_AttendanceMark.Location = new System.Drawing.Point(200, 118);
            this.CB_AttendanceMark.Margin = new System.Windows.Forms.Padding(23, 15, 15, 15);
            this.CB_AttendanceMark.Name = "CB_AttendanceMark";
            this.CB_AttendanceMark.Size = new System.Drawing.Size(223, 29);
            this.CB_AttendanceMark.TabIndex = 12;
            this.CB_AttendanceMark.Text = "Вариант посещаемости";
            this.CB_AttendanceMark.SelectedIndexChanged += new System.EventHandler(this.CB_AttendanceMark_SelectedIndexChanged);
            // 
            // Refresh_Button
            // 
            this.Refresh_Button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Refresh_Button.BackgroundImage")));
            this.Refresh_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Refresh_Button.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Refresh_Button.Location = new System.Drawing.Point(441, 117);
            this.Refresh_Button.Name = "Refresh_Button";
            this.Refresh_Button.Size = new System.Drawing.Size(31, 31);
            this.Refresh_Button.TabIndex = 13;
            this.Refresh_Button.TabStop = false;
            this.Refresh_Button.Click += new System.EventHandler(this.Refresh_Button_Click);
            // 
            // Lab_Out
            // 
            this.Lab_Out.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Lab_Out.Location = new System.Drawing.Point(28, 186);
            this.Lab_Out.Name = "Lab_Out";
            this.Lab_Out.Size = new System.Drawing.Size(786, 43);
            this.Lab_Out.TabIndex = 14;
            this.Lab_Out.Text = "Студент отсутствовал - 0 раз из 0 зафиксированных занятий (пар)";
            this.Lab_Out.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CalculationOfAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 263);
            this.Controls.Add(this.Lab_Out);
            this.Controls.Add(this.Refresh_Button);
            this.Controls.Add(this.CB_AttendanceMark);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_EndDate);
            this.Controls.Add(this.TB_StartDate);
            this.Controls.Add(this.DATA);
            this.Controls.Add(this.CB_Student);
            this.Controls.Add(this.CB_Group);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "CalculationOfAttendance";
            this.Text = "Расчет Посещаемости";
            this.Load += new System.EventHandler(this.CalculationOfAttendance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DATA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Refresh_Button)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CB_Student;
        private System.Windows.Forms.ComboBox CB_Group;
        private System.Windows.Forms.DataGridView DATA;
        private System.Windows.Forms.TextBox TB_StartDate;
        private System.Windows.Forms.TextBox TB_EndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CB_AttendanceMark;
        private System.Windows.Forms.PictureBox Refresh_Button;
        private System.Windows.Forms.Label Lab_Out;
    }
}