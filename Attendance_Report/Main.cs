using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Label = System.Windows.Forms.Label;

namespace Attendance_Report
{
    public partial class Main : Form
    {
        public Main() => InitializeComponent(); //LogForm L = new LogForm(); L.Show(); L.DG.DataSource = Set.Tables["[]"].DefaultView; 

        public static string Login { get; set; }
        public static string AccessRights { get; set; }

        private string ChRequest_FROM, ChRequest_WHERE; private DateTime Date;

        private void Main_Load(object sender, EventArgs e)
        {
            string SQL; SqlDataAdapter data; DataSet Set;

            if (AccessRights == "Teacher")
            {
                // Получение списка групп
                SQL = $"SELECT [Group] FROM [Attendance_Report].[dbo].[Groups]";
                data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;
                for (int I1 = 0; I1 < DATA_Temp1.RowCount - 1; I1++) CB_Group.Items.Add(DATA_Temp1.Rows[I1].Cells[0].Value.ToString()); CB_Group.SelectedIndex = 0;
            }

            if (AccessRights == "Student" | AccessRights == "Elder")
            {
                // Стартовые настройки
                CB_Group.Enabled = false;

                // Получение списка групп
                SQL = $"SELECT [Groups].[Group] FROM [Attendance_Report].[dbo].[AccessRights], [Attendance_Report].[dbo].[Students], [Attendance_Report].[dbo].[Groups]  WHERE [AccessRights].[Login] = '{Login}' AND [Students].[ID] = [AccessRights].[UniqueData] AND [Groups].[ID] = [Students].[Group]";
                data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;
                CB_Group.Items.Add(DATA_Temp1.Rows[0].Cells[0].Value.ToString()); CB_Group.SelectedIndex = 0;
            }
        }

        private void UpdateData()
        {
            // Получение данных
            string SQL = $"SELECT DISTINCT [Groups].[Group] AS [Group Name], [AttendanceTracking].[Student], [AttendanceTracking].[Date], [AttendanceTracking].[ClassNumber] AS [Class Number], [AttendanceTracking].[AttendanceMark] FROM [Attendance_Report].[dbo].[Groups], [Attendance_Report].[dbo].[AttendanceTracking]{ChRequest_FROM} " +
            $"WHERE [Groups].[Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}' AND [AttendanceTracking].[Date] >= '{Date}' AND [AttendanceTracking].[Date] <= '{Date.AddDays(5)}'{ChRequest_WHERE}";
            SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;

            if (DATA_Temp1.RowCount - 1 == 0)
            {
                // Повторное получение данных
                SQL = $"SELECT DISTINCT [Groups].[Group] AS [Group Name],  [AttendanceTracking].[Student], '' AS [Date], '' AS [Class Number], '' AS [AttendanceMark] FROM [Attendance_Report].[dbo].[Groups], [Attendance_Report].[dbo].[AttendanceTracking]{ChRequest_FROM} WHERE [Groups].[Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}'{ChRequest_WHERE}";
                data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;
            }

            // Заполнение данными
            {
                // Очистка таблицы-вывод
                Table.BringToFront(); Table.Controls.Clear(); Table.AutoScroll = false; Table.AutoScroll = true;

                Panel Heading = new Panel { Size = new Size(Table.Width, 120), Name = "Heading" };
                {
                    Label Lab_Name = new Label { AutoSize = false, Size = new Size(300, Heading.Height), Font = new Font("Times New Roman", 14), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Text = "Фамилия   Имя   Отчество" }; Heading.Controls.Add(Lab_Name);

                    string[] Days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };

                    for (int I1 = 0; I1 < 6; I1++) { Label Lab_Day = new Label { AutoSize = false, Size = new Size(160, 40), Font = new Font("Times New Roman", 14), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Text = Days[I1], Left = 300 + 160 * I1 }; Heading.Controls.Add(Lab_Day); }

                    // Получение данных
                    SQL = $"SELECT DISTINCT [Lessons].[Date], [Lessons].[ClassNumber], (SELECT [Subjects].[Subject] FROM [Attendance_Report].[dbo].[Subjects] WHERE [Subjects].[ID] = [Lessons].[Subject]) AS [Subject] FROM [Attendance_Report].[dbo].[Lessons] " +
                        $"WHERE [Lessons].[Date] >= '{Date}' AND [Lessons].[Date] <= '{Date.AddDays(5)}' AND [Lessons].[Group] = (SELECT [Groups].[ID] FROM [Attendance_Report].[dbo].[Groups] WHERE [Groups].[Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}')";
                    data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp2.DataSource = Set.Tables["[]"].DefaultView;

                    // Получение данных
                    SQL = $"SELECT DISTINCT [Weekend].[Date] FROM [Attendance_Report].[dbo].[Weekend], [Attendance_Report].[dbo].[Groups] WHERE [Weekend].[Date] >= '{Date}' AND [Weekend].[Date] <= '{Date.AddDays(5)}' AND [Weekend].[Group] = (SELECT [Groups].[ID] FROM [Attendance_Report].[dbo].[Groups] WHERE [Groups].[Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}')";
                    data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp3.DataSource = Set.Tables["[]"].DefaultView;

                    for (int I1 = 0, I4 = 0, I5 = 0; I1 < 6; I1++)
                    {
                        bool T = true;
                        if (DATA_Temp3.RowCount - I5 - 1 > 0)
                        {
                            if (DATA_Temp3.Rows[I5].Cells[0].Value.ToString() == Date.AddDays(I1).ToString())
                            {
                                Label Lab_Class = new Label { Name = "Weekend", Tag = "Week_" + Date.AddDays(I1) + "_" + CB_Group.Text, AutoSize = false, Size = new Size(160, 80), BorderStyle = BorderStyle.FixedSingle, Font = new Font("Times New Roman", 14, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter, Left = 300 + 160 * I1, Top = 40, Text = "Выходной" };
                                Lab_Class.Click += new EventHandler(Label_Click); Heading.Controls.Add(Lab_Class); I5++;
                            }
                            else T = false;
                        }
                        else T = false;

                        if (T == false) for (int I2 = 0; I2 < 4; I2++)
                            {
                                Label Lab_Class = new Label { Name = "Subject", Tag = "Sub_" + CB_Group.Text + "_" + Date.AddDays(I1) + "_" + (I2 + 1), AutoSize = false, Size = new Size(40, 80), BorderStyle = BorderStyle.FixedSingle, Font = new Font("Times New Roman", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft, Left = 300 + 160 * I1 + I2 * 40, Top = 40 };

                                if (DATA_Temp2.RowCount - I4 - 1 > 0) if (DATA_Temp2.Rows[I4].Cells[0].Value.ToString() == Date.AddDays(I1).ToString()) if (DATA_Temp2.Rows[I4].Cells[1].Value.ToString() == (I2 + 1).ToString())
                                        { Lab_Class.Tag += "_" + DATA_Temp2.Rows[I4].Cells[2].Value.ToString(); Lab_Class.Text = DATA_Temp2.Rows[I4].Cells[2].Value.ToString(); I4++; Lab_Class.Paint += Label_Paint; new System.Windows.Forms.ToolTip().SetToolTip(Lab_Class, Lab_Class.Text); }

                                Lab_Class.Click += new EventHandler(Label_Click); Heading.Controls.Add(Lab_Class);
                            }
                    }

                    Table.Controls.Add(Heading);
                }

                // Получение данных
                SQL = $"SELECT DISTINCT [Students].[Student] FROM [Attendance_Report].[dbo].[Students], [Attendance_Report].[dbo].[Groups] WHERE [Students].[Group] = [Groups].[ID] AND [Groups].[Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}'";
                data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp2.DataSource = Set.Tables["[]"].DefaultView;

                int Count_Student = DATA_Temp2.RowCount - 1; if (CB_Student.SelectedIndex !=0) Count_Student = 1;

                for (int I1 = 0; I1 < Count_Student; I1++)
                {
                    Panel MainPal = new Panel { Size = new Size(Table.Width, 40), Name = ("Main_" + I1 + 1) };
                    {
                        int Fixed_Count = (DATA_Temp1.RowCount - 1) / Count_Student;

                        Label Lab_Name = new Label { AutoSize = false, Size = new Size(300, MainPal.Height), Font = new Font("Times New Roman", 14), TextAlign = ContentAlignment.MiddleLeft, BorderStyle = BorderStyle.FixedSingle };
                        {
                            SQL = $"SELECT [FIO].[Surname] + ' ' + [FIO].[Name]+ ' ' + [FIO].[MiddleName] AS [Student FIO] FROM [Attendance_Report].[dbo].[FIO] WHERE [FIO].[ID] = '{DATA_Temp1.Rows[I1 * Fixed_Count].Cells[1].Value}'";
                            data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp2.DataSource = Set.Tables["[]"].DefaultView;

                            Lab_Name.Text = (I1 + 1) + ". " + DATA_Temp2.Rows[0].Cells[0].Value.ToString();

                            MainPal.Controls.Add(Lab_Name);
                        }

                        for (int I2 = 0, I4 = 0, I5 = 0; I2 < 6; I2++) for (int I3 = 0; I3 < 4; I3++, I4++)
                            {
                                Label Lab = new Label { Name = "AM", Text = ".", AutoSize = false, Size = new Size(40, MainPal.Height), Font = new Font("Times New Roman", 10), TextAlign = ContentAlignment.MiddleCenter, Left = 300 + 40 * I4, BorderStyle = BorderStyle.FixedSingle };
                                Lab.Click += new EventHandler(Label_Click); Lab.Tag = "AM_" + DATA_Temp1.Rows[I1 * Fixed_Count].Cells[1].Value + "_" + Date.AddDays(I2) + "_" + (I3 + 1);

                                if (Fixed_Count - I5 > 0) if (DATA_Temp1.Rows[I1 * Fixed_Count + I5].Cells[2].Value.ToString() == Date.AddDays(I2).ToString()) if (DATA_Temp1.Rows[I1 * Fixed_Count + I5].Cells[3].Value.ToString() == (I3 + 1).ToString())
                                        { Lab.Font = new Font("Times New Roman", 10); Lab.Text = AttendanceMarks(DATA_Temp1.Rows[I1 * Fixed_Count + I5].Cells[4].Value.ToString()); Lab.Tag += "_" + DATA_Temp1.Rows[I1 * Fixed_Count + I5].Cells[4].Value; I5++; }

                                MainPal.Controls.Add(Lab);
                            }

                        Table.Controls.Add(MainPal);
                    }
                }
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            if (Date <= DateTime.Now & DateTime.Now <= Date.AddDays(7)) switch (AccessRights)
                {
                    case "Elder":
                        {
                            ValueEditor.Link = (Label)sender; new ValueEditor().ShowDialog(); if (ValueEditor.Link.Name == "Weekend") UpdateData();
                            if (ValueEditor.Link.Name == "Subject") if (ValueEditor.Link.Name[ValueEditor.Link.Name.Length - 1] == '1') UpdateData(); else ValueEditor.Link.Paint += Label_Paint;
                        }
                        break;
                }

            SaveData();
        }

        private void Label_Paint(object sender, PaintEventArgs e)
        {
            Label lab = (Label)sender; lab.Width = 40; lab.Height = 80; lab.Top = 40;
            e.Graphics.Clear(this.BackColor); e.Graphics.RotateTransform(-90);
            e.Graphics.DrawString(lab.Text, lab.Font, Brushes.Black, -70, 10);
        }

        private void ConvertToDateTime(string TextD, bool T)
        {
            string[] dmy = TextD.Split(' '); string[] DMY = dmy[0].Split('.'); DateTime ND = new DateTime(Convert.ToInt16(DMY[2]), Convert.ToInt16(DMY[1]), Convert.ToInt16(DMY[0]));

            switch (ND.DayOfWeek)
            {
                case DayOfWeek.Tuesday: ND = ND.AddDays(-1); break;
                case DayOfWeek.Wednesday: ND = ND.AddDays(-2); break;
                case DayOfWeek.Thursday: ND = ND.AddDays(-3); break;
                case DayOfWeek.Friday: ND = ND.AddDays(-4); break;
                case DayOfWeek.Saturday: ND = ND.AddDays(-5); break;
                case DayOfWeek.Sunday: ND = ND.AddDays(-6); break;
                default: break;
            }

            if (T == true) CB_Date.Items.Add(ND.Day + "." + ND.Month + "." + ND.Year + " - " + ND.AddDays(5).Day + "." + ND.AddDays(5).Month + "." + ND.AddDays(5).Year);  else Date = ND;
        }

        private void CB_Date_SelectedIndexChanged(object sender, EventArgs e)  
        {  ConvertToDateTime(CB_Date.Items[CB_Date.SelectedIndex].ToString(), false); if (CB_Student.SelectedIndex != 0) CB_Student.SelectedIndex = 0; else CB_Student_SelectedIndexChanged(sender, e); }

        private void CB_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SQL; SqlDataAdapter data; DataSet Set; CB_Date.Items.Clear();  CB_Student.Items.Clear();

            // Заполнение списка студентов
            if (AccessRights == "Teacher")
            {
                SQL = $"SELECT [Students].[Student], [FIO].[Surname], [FIO].[Name], [FIO].[MiddleName] FROM [Attendance_Report].[dbo].[FIO], [Attendance_Report].[dbo].[Students] WHERE [FIO].[ID] = [Students].[Student] AND [Students].[Group] = (SELECT [ID] FROM [Attendance_Report].[dbo].[Groups] WHERE [Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}')";
                data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;
                CB_Student.Items.Add("Все"); for (int I1 = 0; I1 < DATA_Temp1.RowCount-1; I1++) CB_Student.Items.Add($"{DATA_Temp1.Rows[I1].Cells[1].Value} {DATA_Temp1.Rows[I1].Cells[2].Value} {DATA_Temp1.Rows[I1].Cells[3].Value}");
            }

            if (AccessRights == "Student" | AccessRights == "Elder") { CB_Student.Items.Add("Все"); CB_Student.Items.Add("Я"); }

            // Получение данных о зафиксированных датах
            SQL = $"SELECT DISTINCT [FixedWeeks].[DateStartFixed],[FixedWeeks].[DateEndFixed] FROM [Attendance_Report].[dbo].[FixedWeeks] WHERE [FixedWeeks].[Group] = (SELECT [ID] FROM [Attendance_Report].[dbo].[Groups] WHERE [Group] = '{CB_Group.Items[CB_Group.SelectedIndex]}')";
            data = new SqlDataAdapter(SQL, Authorization.ConnectString);Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;

            // Заполнение списка дат
            ConvertToDateTime(DateTime.Now.Date.ToString(), true); ConvertToDateTime(DateTime.Now.Date.ToString(), false); for (int I1 = 0; I1 < DATA_Temp1.RowCount - 1; I1++) if ((DateTime)DATA_Temp1.Rows[I1].Cells[0].Value != Date) ConvertToDateTime(DATA_Temp1.Rows[I1].Cells[0].Value.ToString(), true); CB_Date.SelectedIndex = 0;
        }

        private void CB_Student_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CB_Student.Items[CB_Student.SelectedIndex])
            {
                case "Все": { ChRequest_FROM = $""; ChRequest_WHERE = $""; } break;
                case "Я": { ChRequest_FROM = $", [Attendance_Report].[dbo].[Students], [Attendance_Report].[dbo].[AccessRights]"; ChRequest_WHERE = $" AND [AccessRights].[Login] = '{Login}' AND [Students].[ID] = [AccessRights].[UniqueData] AND [AttendanceTracking].Student = [Students].[Student]"; } break;
                default: { string[] FIO = CB_Student.Items[CB_Student.SelectedIndex].ToString().Split(' '); ChRequest_FROM = $""; ChRequest_WHERE = $" AND [AttendanceTracking].[Student] = (SELECT [FIO].[ID] FROM [Attendance_Report].[dbo].[FIO] WHERE [FIO].[Surname] = '{FIO[0]}' AND [FIO].[Name] = '{FIO[1]}' AND [FIO].[MiddleName] = '{FIO[2]}')"; } break;
            }

            UpdateData();
        }

        private string AttendanceMarks(string G) { switch (G) { case "AMs1": return "Н"; case "AMs2": return "О"; case "AMs3": return " "; default: return ""; } }

        private void SaveData()
        {
            foreach (var A in Table.Controls.OfType<Panel>().ToList())
            {
                switch (A.Name[0])
                {
                    case 'H':
                        {
                            foreach (var B in A.Controls.OfType<Label>().ToList()) if (B.Name == "Subject")
                                {
                                    string[] Data_Lab = B.Tag.ToString().Split('_');

                                    if (Data_Lab.Length == 5)
                                    {
                                        string Request, SQL = $"SELECT * FROM [Attendance_Report].[dbo].[Lessons] WHERE [Date] = '{Data_Lab[2]}' AND [Group] = (SELECT [ID] FROM [Attendance_Report].[dbo].Groups WHERE [Group] = '{Data_Lab[1]}') AND [ClassNumber] = '{Data_Lab[3]}'";
                                        SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;

                                        if (DATA_Temp1.RowCount - 1 == 0)
                                        {
                                            SQL = $"SELECT TOP 1 [Lessons].[ID] FROM [Attendance_Report].[dbo].[Lessons] ORDER BY [ID] DESC"; string Temp1 = "";
                                            data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;
                                            for (int I1 = 1; I1 < DATA_Temp1.Rows[0].Cells[0].Value.ToString().Length; I1++) { Temp1 += DATA_Temp1.Rows[0].Cells[0].Value.ToString()[I1]; }
                                            Temp1 = "L" + (Convert.ToInt16(Temp1) + 1);

                                            Request = $"INSERT INTO [Attendance_Report].[dbo].[Lessons]([ID],[Date],[Group],[ClassNumber],[Subject]) VALUES ('{Temp1}','{Data_Lab[2]}',(SELECT [ID] FROM [Attendance_Report].[dbo].Groups WHERE [Group] = '{Data_Lab[1]}'),'{Data_Lab[3]}', (SELECT [ID] FROM [Attendance_Report].[dbo].[Subjects] WHERE [Subject] = '{Data_Lab[4]}'))";
                                        }
                                        else Request = $"UPDATE [Attendance_Report].[dbo].[Lessons] SET [Lessons].[Subject] = (SELECT [ID] FROM [Attendance_Report].[dbo].[Subjects] WHERE [Subject] = '{Data_Lab[4]}') WHERE [Date] = '{Data_Lab[2]}' AND [Group] = (SELECT [ID] FROM [Attendance_Report].[dbo].Groups WHERE [Group] = '{Data_Lab[1]}') AND [ClassNumber] = '{Data_Lab[3]}'";

                                        SqlConnection SQL_Connection = new SqlConnection(Authorization.ConnectString); SqlCommand SQL_Command; SQL_Connection.Open(); SQL_Command = SQL_Connection.CreateCommand(); SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                                    }
                                }
                        }
                        break;

                    case 'M':
                        {
                            foreach (var B in A.Controls.OfType<Label>().ToList()) if (B.Name == "AM")
                                {
                                    string[] Data_Lab = B.Tag.ToString().Split('_');

                                    if (Data_Lab.Length == 5)
                                    {
                                        string Request, SQL = $"SELECT * FROM [Attendance_Report].[dbo].[AttendanceTracking] WHERE [AttendanceTracking].[Student] = '{Data_Lab[1]}' AND [AttendanceTracking].[Date] = '{Data_Lab[2]}' AND [AttendanceTracking].[ClassNumber] = '{Data_Lab[3]}'";
                                        SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;

                                        if (DATA_Temp1.RowCount - 1 == 0)
                                        {
                                            SQL = $"SELECT TOP 1 [AttendanceTracking].[ID] FROM [Attendance_Report].[dbo].[AttendanceTracking] ORDER BY [ID] DESC"; string Temp1 = "";
                                            data = new SqlDataAdapter(SQL, Authorization.ConnectString); Set = new DataSet(); data.Fill(Set, "[]"); DATA_Temp1.DataSource = Set.Tables["[]"].DefaultView;
                                            for (int I1 = 2; I1 < DATA_Temp1.Rows[0].Cells[0].Value.ToString().Length; I1++) { Temp1 += DATA_Temp1.Rows[0].Cells[0].Value.ToString()[I1]; }
                                            Temp1 = "AT" + (Convert.ToInt16(Temp1) + 1);

                                            Request = $"INSERT INTO [Attendance_Report].[dbo].[AttendanceTracking]([ID],[Student],[Date],[ClassNumber],[AttendanceMark]) VALUES ('{Temp1}','{Data_Lab[1]}','{Data_Lab[2]}','{Data_Lab[3]}','{Data_Lab[4]}')";
                                        }
                                        else Request = $"UPDATE [Attendance_Report].[dbo].[AttendanceTracking] SET [AttendanceTracking].[AttendanceMark] = '{Data_Lab[4]}' WHERE [AttendanceTracking].[Student] = '{Data_Lab[1]}' AND[AttendanceTracking].[Date] = '{Data_Lab[2]}' AND[AttendanceTracking].[ClassNumber] = '{Data_Lab[3]}'";

                                        SqlConnection SQL_Connection = new SqlConnection(Authorization.ConnectString); SqlCommand SQL_Command; SQL_Connection.Open(); SQL_Command = SQL_Connection.CreateCommand(); SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                                    }
                                }
                        }
                        break;
                }
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e) { SaveData(); MessageBox.Show("Успешно", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information); Application.OpenForms["Authorization"].Show(); }

        // Временно
        private void pictureBox1_Click(object sender, EventArgs e) { if (DATA_Temp1.Visible == false) DATA_Temp1.Visible = true; else DATA_Temp1.Visible = false; }

        private void pictureBox2_Click(object sender, EventArgs e) { if (DATA_Temp2.Visible == false) DATA_Temp2.Visible = true; else DATA_Temp2.Visible = false; }

        private void pictureBox3_Click(object sender, EventArgs e) { if (DATA_Temp3.Visible == false) DATA_Temp3.Visible = true; else DATA_Temp3.Visible = false; }

        private void pictureBox4_Click(object sender, EventArgs e) => SaveData();
    }
}