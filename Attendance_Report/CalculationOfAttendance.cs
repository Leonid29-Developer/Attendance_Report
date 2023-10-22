using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Attendance_Report
{
    public partial class CalculationOfAttendance : Form
    {
        public CalculationOfAttendance() => InitializeComponent();

        private string ChRequest_WHERE;

        private void CalculationOfAttendance_Load(object sender, EventArgs e)
        {
            // Получение списка групп
            string SQL = $"SELECT [Group] FROM [Attendance_Report].[dbo].[Groups]";
            SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DATA.DataSource = Set.Tables["[]"].DefaultView;
            for (int I1 = 0; I1 < DATA.RowCount - 1; I1++) CB_Group.Items.Add(DATA.Rows[I1].Cells[0].Value.ToString()); CB_Group.SelectedIndex = 0;
        }

        private void Calculation()
        {
            try
            {
                if (TB_StartDate.Text != "" & TB_EndDate.Text != "" & CB_Student.Text != "Студент" & CB_AttendanceMark.Text != "Вариант посещаемости")
                {
                    string SQL = $"SELECT " +
                        $"(SELECT TOP 1 COUNT([AttendanceTracking].[AttendanceMark]) FROM [Attendance_Report].[dbo].[AttendanceTracking], [Attendance_Report].[dbo].[Groups], [Attendance_Report].[dbo].[Students]" +
                        $"WHERE [AttendanceTracking].[Student] = [Students].[Student] AND [Students].[Group] = [Groups].[ID] AND [Groups].[Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}' AND [AttendanceTracking].[Date] >= '{TB_StartDate.Text}' AND [AttendanceTracking].[Date] <= '{TB_EndDate.Text}' AND [AttendanceTracking].[AttendanceMark] = '{AttendanceMarks()}'{ChRequest_WHERE}) AS [UN]," +
                        $"(SELECT TOP 1 COUNT([AttendanceTracking].[AttendanceMark]) FROM [Attendance_Report].[dbo].[AttendanceTracking], [Attendance_Report].[dbo].[Groups], [Attendance_Report].[dbo].[Students]" +
                        $"WHERE [AttendanceTracking].[Student] = [Students].[Student] AND [Students].[Group] = [Groups].[ID] AND [Groups].[Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}' AND [AttendanceTracking].[Date] >= '{TB_StartDate.Text}' AND [AttendanceTracking].[Date] <= '{TB_EndDate.Text}'{ChRequest_WHERE}) AS [ALL]";
                    SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DATA.DataSource = Set.Tables["[]"].DefaultView;
                    
                    Lab_Out.Text = $"Студент {CB_AttendanceMark.Items[CB_AttendanceMark.SelectedIndex].ToString().ToLower()} {DATA.Rows[0].Cells[0].Value} раз из {DATA.Rows[0].Cells[1].Value} зафиксированных занятий (пар)";
                }
                else MessageBox.Show("Не все данные были введены или выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { MessageBox.Show("Некорректные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void CB_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Заполнение списка студентов
            string SQL = $"SELECT DISTINCT [Students].[Student], [FIO].[Surname], [FIO].[Name], [FIO].[MiddleName] FROM [Attendance_Report].[dbo].[FIO], [Attendance_Report].[dbo].[Students] WHERE [FIO].[ID] = [Students].[Student] AND [Students].[Group] = (SELECT [ID] FROM [Attendance_Report].[dbo].[Groups] WHERE [Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}')";
            SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString);DataSet Set = new DataSet();  data.Fill(Set, "[]"); DATA.DataSource = Set.Tables["[]"].DefaultView;
            for (int I1 = 0; I1 < DATA.RowCount - 1; I1++) CB_Student.Items.Add($"{DATA.Rows[I1].Cells[1].Value} {DATA.Rows[I1].Cells[2].Value} {DATA.Rows[I1].Cells[3].Value}");
        }

        private void CB_Student_SelectedIndexChanged(object sender, EventArgs e)
        { string[] FIO = CB_Student.Items[CB_Student.SelectedIndex].ToString().Split(' '); ChRequest_WHERE = $" AND [AttendanceTracking].[Student] = (SELECT [FIO].[ID] FROM [Attendance_Report].[dbo].[FIO] WHERE [FIO].[Surname] = '{FIO[0]}' AND [FIO].[Name] = '{FIO[1]}' AND [FIO].[MiddleName] = '{FIO[2]}')"; }

        private string AttendanceMarks() { switch (CB_AttendanceMark.SelectedIndex) { case 0: return "AMs1"; case 1: return "AMs2"; case 2: return "AMs3"; default: return ""; } }

        private void CB_AttendanceMark_SelectedIndexChanged(object sender, EventArgs e) => Calculation();

        private void Refresh_Button_Click(object sender, EventArgs e) => Calculation();
    }
}
