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

namespace Attendance_Report
{
    public partial class Authorization : Form
    {
        public Authorization() => InitializeComponent();

        // Строка подключения
        public static string ConnectString = "Data Source=PC-LEONID29\\SQLEXPRESS;Integrated Security=True";

        private void Button_Input_Click(object sender, EventArgs e)
        {
            bool T = true;
            if (TB_Login.Text != "")
            {
                string SQL = $"SELECT * FROM [Attendance_Report].[dbo].[AccessRights] WHERE Login = '{TB_Login.Text}';"; // SQL-запрос
                SqlDataAdapter data = new SqlDataAdapter(SQL, ConnectString); DataSet Set = new DataSet(); data.Fill(Set, "[AccessRights]"); DATA.DataSource = Set.Tables["[AccessRights]"].DefaultView;
                if (TB_Password.Text == DATA.Rows[0].Cells[1].Value.ToString())
                {
                    switch (DATA.Rows[0].Cells[2].Value.ToString())
                    {
                        case "C3": { Main.AccessRights = "Elder"; } break;
                        case "D4": { Main.AccessRights = "Student"; } break;
                    }

                    Main.Login = DATA.Rows[0].Cells[0].Value.ToString(); Hide(); new Main().ShowDialog();
                }
                else T = false;
            }
            else T = false;

            if (T == false) MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Временно
        private void pictureBox1_Click(object sender, EventArgs e) { TB_Login.Text = "SharpGlove"; TB_Password.Text = "SG57"; }
        private void pictureBox2_Click(object sender, EventArgs e) { TB_Login.Text = "YoungHouse"; TB_Password.Text = "YH43"; }
    }
}