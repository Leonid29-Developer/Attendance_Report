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
using static System.Windows.Forms.LinkLabel;

namespace Attendance_Report
{
    public partial class ValueEditor : Form
    {
        public ValueEditor() => InitializeComponent();

        public static Label Link; public static string Tea;

        private void ValueEditor_Load(object sender, EventArgs e)
        {
            // Очистка таблицы-вывод
            Table.BringToFront(); Table.Controls.Clear(); Table.AutoScroll = false; Table.AutoScroll = true; string[] AM = { "H", "Отсутствовал", "О", "Опоздал", "", "Присутствовал" };
            Point NLocation = Link.PointToScreen(Point.Empty); Location = new Point(NLocation.X + Link.Width / 4 * 3, NLocation.Y + Link.Height);

            if (Main.AccessRights == "Teacher")
            {
                string SQL = $"SELECT [Subjects].[Subject] FROM [Attendance_Report].[dbo].[Subjects], [Attendance_Report].[dbo].[AccessRights] WHERE [AccessRights].[Login] = '{Main.Login}' AND [Subjects].[ID] = [AccessRights].[UniqueData]";
                SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DataGridView DATA = Example_DATA; DATA.DataSource = Set.Tables["[]"].DefaultView;
                Tea = DATA.Rows[0].Cells[0].Value.ToString();
            }

            string[] Data = Link.Tag.ToString().Split('_'); switch (Data[0])
            {
                case "Sub":
                    {
                        string SQL = ""; if (Main.AccessRights == "Elder") SQL = $"SELECT [Subject] FROM [Attendance_Report].[dbo].[Subjects]";
                        else SQL = $"SELECT [Subjects].[Subject] FROM [Attendance_Report].[dbo].[Subjects], [Attendance_Report].[dbo].[AccessRights] WHERE [AccessRights].[Login] = '{Main.Login}' AND [Subjects].[ID] = [AccessRights].[UniqueData]";
                        SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DataGridView DATA = Example_DATA; DATA.DataSource = Set.Tables["[]"].DefaultView;

                        if (Main.AccessRights == "Teacher") { Height = 322; if (Data.Length == 5) { if (Main.AccessRights == "Teacher" & Data[4] != Tea) Close(); } else Height = 246; }

                        if (Main.AccessRights == "Elder")
                        {
                            Height = 428; Panel Panel_Week = new Panel { Size = new Size(Table.Width - 6, 100) };
                            {
                                Label Lab1 = new Label { Size = new Size(Panel_Week.Width, Panel_Week.Height), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Times New Roman", 16), Text = "Установить\nвыходной" };
                                { Lab1.Tag = "ADD"; Lab1.Click += Label_Click; Panel_Week.Controls.Add(Lab1); Table.Controls.Add(Panel_Week); }
                            }

                            if (Data.Length == 4) Height = 352;
                        }

                        Panel Panel_Main = new Panel { Name = "Main", Size = new Size(Table.Width - 6, 200), BorderStyle = BorderStyle.FixedSingle };
                        {
                            Label Lab1 = new Label { Size = new Size(Panel_Main.Width, 80), TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Times New Roman", 14), Text = "Введите наименование предмета", Top = 10 };
                            { Panel_Main.Controls.Add(Lab1); }

                            ComboBox CB = new ComboBox { Name = "TB_Class", Size = new Size(Table.Width - 26, 40), Anchor = AnchorStyles.None, Font = new Font("Times New Roman", 14), Left = 12, Top = 96 };

                            for (int i = 0; i < DATA.RowCount - 1; i++) CB.Items.Add(DATA.Rows[i].Cells[0].Value); Panel_Main.Controls.Add(CB);
                            if (Main.AccessRights == "Teacher") { CB.Text = DATA.Rows[0].Cells[0].Value.ToString(); CB.Enabled = false; }

                            Label End = new Label { Size = new Size(150, 40), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Times New Roman", 12), Text = "Установить предмет", Left = Panel_Main.Width / 2 - 75, Top = 143 };
                            { End.Tag = "Class"; End.Click += Label_Click; Panel_Main.Controls.Add(End); Table.Controls.Add(Panel_Main); }
                        }

                        if (Data.Length == 5) if (Main.AccessRights == "Elder" | (Main.AccessRights == "Teacher" & Data[4] == Tea))
                            {
                                Panel Panel_Week = new Panel { Size = new Size(Table.Width - 6, 70) };
                                {
                                    Label Lab1 = new Label { Size = new Size(Panel_Week.Width, Panel_Week.Height), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Times New Roman", 14), Text = "Убрать предмет" };
                                    { Lab1.Tag = "REMOVE"; Lab1.Click += Label_Click; Panel_Week.Controls.Add(Lab1); Table.Controls.Add(Panel_Week); }
                                }
                            }
                    }
                    break;

                case "Week":
                    {
                        if (Main.AccessRights == "Teacher") Close();

                        string SQL = $"SELECT DISTINCT [Weekend].[Date],[Weekend].[Group] FROM [Attendance_Report].[dbo].[Weekend], [Attendance_Report].[dbo].[Groups] WHERE [Groups].[Group] = '{Data[2]}' AND [Weekend].[Group] = [Groups].[ID] AND [Weekend].[Date] = '{Data[1]}'";
                        SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DataGridView DATA = Example_DATA; DATA.DataSource = Set.Tables["[]"].DefaultView;
                        if (DATA.RowCount - 1 > 0)
                        {
                            Height = 120; Panel Panel_Main = new Panel { Size = new Size(Table.Width - 6, Table.Height - 6) };
                            {
                                Label Lab1 = new Label { Size = new Size(Panel_Main.Width, Panel_Main.Height), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Times New Roman", 16), Text = "Убрать выходной" };
                                { Lab1.Click += Label_Click; Panel_Main.Controls.Add(Lab1); Table.Controls.Add(Panel_Main);}
                            }
                        }
                        else Close();
                    }
                    break;

                case "AM":
                    {
                        string SQL = $"SELECT (SELECT [Subjects].[Subject] FROM [Attendance_Report].[dbo].[Subjects] WHERE [Subjects].[ID] = [Lessons].[Subject]) AS [Subject]  FROM [Attendance_Report].[dbo].[Lessons] WHERE [Lessons].[Date] = '{Data[2]}' AND [Lessons].[ClassNumber] = '{Data[3]}' AND [Lessons].[Group] = (SELECT [Students].[Group] FROM [Attendance_Report].[dbo].[Students] WHERE [Students].[Student] = '{Data[1]}')";
                        SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DataGridView DATA = Example_DATA; DATA.DataSource = Set.Tables["[]"].DefaultView;
                        if (Main.AccessRights == "Teacher") if (DATA.RowCount - 1 > 0) { if (DATA.Rows[0].Cells[0].Value.ToString() != Tea) Close(); } else Close();

                        Height = 300; for (int I1 = 0; I1 < 3; I1++)
                        {
                            Panel Panel_Main = new Panel { Size = new Size(Table.Width - 8, (Table.Height - 21) / 3) };
                            {
                                Label Lab1 = new Label { Size = new Size(Panel_Main.Width / 4, Panel_Main.Height), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Times New Roman", 14), Text = AM[I1 * 2] };
                                { Lab1.Click += Label_Click; Lab1.Tag = I1 + 1; Panel_Main.Controls.Add(Lab1); }

                                Label Lab2 = new Label { Size = new Size(Panel_Main.Width / 4 * 3, Panel_Main.Height), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Times New Roman", 14), Text = AM[I1 * 2 + 1], Left = Panel_Main.Width / 4 };
                                { Lab2.Click += Label_Click; Lab2.Tag = I1 + 1; Panel_Main.Controls.Add(Lab2); Table.Controls.Add(Panel_Main);}
                            }
                        }
                    }
                    break;
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            string[] Data = Link.Tag.ToString().Split('_'); Label Lab = (Label)sender;
            switch (Data[0])
            {
                case "Sub":
                    {
                        if (Lab.Tag.ToString() == "ADD")
                        {
                            string Request, SQL = $"SELECT TOP 1 [Weekend].[ID] FROM [Attendance_Report].[dbo].[Weekend] ORDER BY [ID] DESC"; string Temp1 = "";
                            SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DataGridView DATA = Example_DATA; DATA.DataSource = Set.Tables["[]"].DefaultView;
                            for (int I1 = 1; I1 < DATA.Rows[0].Cells[0].Value.ToString().Length; I1++) { Temp1 += DATA.Rows[0].Cells[0].Value.ToString()[I1]; }
                            Temp1 = "W" + (Convert.ToInt16(Temp1) + 1); Link.Name = "Weekend";

                            Request = $"INSERT INTO [Attendance_Report].[dbo].[Weekend]([ID],[Date],[Group]) VALUES ('{Temp1}','{Data[2]}',(SELECT [Groups].[ID] FROM [Attendance_Report].[dbo].[Groups] WHERE [Groups].[Group] = '{Data[1]}'))"; 
                            SqlConnection SQL_Connection = new SqlConnection(Authorization.ConnectString); SqlCommand SQL_Command; SQL_Connection.Open(); SQL_Command = SQL_Connection.CreateCommand(); SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                        }

                        if (Lab.Tag.ToString() == "REMOVE")
                        {
                            Link.Tag = Data[0] + "_" + Data[1] + "_" + Data[2] + "_" + Data[3]; Link.Text = ""; Link.Name = "Subject0";
                            string Request = $"DELETE FROM [Attendance_Report].[dbo].[Lessons] WHERE [Lessons].[Date] = '{Data[2]}' AND [Lessons].[Group] = (SELECT [Groups].[ID] FROM [Attendance_Report].[dbo].[Groups] WHERE [Groups].[Group] = '{Data[1]}') AND [Lessons].[ClassNumber] = '{Data[3]}'";
                            SqlConnection SQL_Connection = new SqlConnection(Authorization.ConnectString); SqlCommand SQL_Command; SQL_Connection.Open(); SQL_Command = SQL_Connection.CreateCommand(); SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                            Request = $"DELETE FROM [Attendance_Report].[dbo].[AttendanceTracking] WHERE [AttendanceTracking].[Date] = '{Data[2]}' AND '{Data[1]}' = (SELECT [Groups].[Group] FROM [Attendance_Report].[dbo].[Groups]" +
                                $"WHERE [Groups].[ID] = (SELECT [Students].[Group] FROM [Attendance_Report].[dbo].[Students] WHERE [Students].[Student] = [AttendanceTracking].[Student])) AND [AttendanceTracking].[ClassNumber] = '{Data[3]}'";
                            SQL_Connection = new SqlConnection(Authorization.ConnectString); SQL_Connection.Open(); SQL_Command = SQL_Connection.CreateCommand(); SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                        }

                        if (Lab.Tag.ToString() == "Class")
                        {
                            string SQL = $"SELECT DISTINCT [Weekend].[Date],[Weekend].[Group] FROM[Attendance_Report].[dbo].[Weekend], [Attendance_Report].[dbo].[Students] WHERE [Students].[Student] = '{Data[1]}' AND [Weekend].[Group] = [Students].[Group] AND [Weekend].[Date] = '{Data[2]}'";
                            SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DataGridView DATA = Example_DATA; DATA.DataSource = Set.Tables["[]"].DefaultView;

                            if ((DATA.RowCount - 1) == 0) foreach (var A in Table.Controls.OfType<Panel>().ToList()) if (A.Name == "Main") foreach (var B in A.Controls.OfType<ComboBox>().ToList())
                                            if (B.Text != "") { Link.Tag = Data[0] + "_" + Data[1] + "_" + Data[2] + "_" + Data[3] + "_" + B.Text; Link.Text = B.Text; } else MessageBox.Show("Данные не были выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;

                case "AM":
                    {
                        string SQL = $"SELECT DISTINCT [Weekend].[Date],[Weekend].[Group] FROM[Attendance_Report].[dbo].[Weekend], [Attendance_Report].[dbo].[Students] WHERE [Students].[Student] = '{Data[1]}' AND [Weekend].[Group] = [Students].[Group] AND [Weekend].[Date] = '{Data[2]}'";
                        SqlDataAdapter data = new SqlDataAdapter(SQL, Authorization.ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[]"); DataGridView DATA = Example_DATA; DATA.DataSource = Set.Tables["[]"].DefaultView;

                        if ((DATA.RowCount - 1) == 0) { Link.Tag = Data[0] + "_" + Data[1] + "_" + Data[2] + "_" + Data[3] + "_AMs" + Lab.Tag.ToString(); Link.Text = AttendanceMarks(Lab.Tag.ToString()); }
                    }
                    break;

                case "Week":
                    {
                        string Request = $"DELETE FROM [Attendance_Report].[dbo].[Weekend] WHERE [Weekend].[Group] = (SELECT [Groups].[ID] FROM [Attendance_Report].[dbo].[Groups] WHERE [Group] = '{Data[2]}') AND [Weekend].[Date] = '{Data[1]}'";
                        SqlConnection SQL_Connection = new SqlConnection(Authorization.ConnectString); SqlCommand SQL_Command; SQL_Connection.Open(); SQL_Command = SQL_Connection.CreateCommand(); SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                    }
                    break;
            }

            Close();
        }

        private string AttendanceMarks(string G) { switch (G) { case "1": return "Н"; case "2": return "О"; case "3": return " "; default: return ""; } }
    }
}