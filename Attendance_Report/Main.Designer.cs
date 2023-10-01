namespace Attendance_Report
{
    partial class Main
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
            this.CB_Group = new System.Windows.Forms.ComboBox();
            this.CB_Date = new System.Windows.Forms.ComboBox();
            this.CB_Student = new System.Windows.Forms.ComboBox();
            this.DATA_Temp1 = new System.Windows.Forms.DataGridView();
            this.DATA_Temp2 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Table = new System.Windows.Forms.TableLayoutPanel();
            this.DATA_Temp3 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DATA_Temp1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DATA_Temp2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DATA_Temp3)).BeginInit();
            this.SuspendLayout();
            // 
            // CB_Group
            // 
            this.CB_Group.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_Group.FormattingEnabled = true;
            this.CB_Group.Location = new System.Drawing.Point(285, 19);
            this.CB_Group.Margin = new System.Windows.Forms.Padding(15, 10, 10, 10);
            this.CB_Group.Name = "CB_Group";
            this.CB_Group.Size = new System.Drawing.Size(174, 31);
            this.CB_Group.TabIndex = 0;
            this.CB_Group.Text = "Группа";
            this.CB_Group.SelectedIndexChanged += new System.EventHandler(this.CB_Group_SelectedIndexChanged);
            // 
            // CB_Date
            // 
            this.CB_Date.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_Date.FormattingEnabled = true;
            this.CB_Date.Location = new System.Drawing.Point(19, 19);
            this.CB_Date.Margin = new System.Windows.Forms.Padding(10);
            this.CB_Date.Name = "CB_Date";
            this.CB_Date.Size = new System.Drawing.Size(241, 31);
            this.CB_Date.TabIndex = 1;
            this.CB_Date.TabStop = false;
            this.CB_Date.Text = "Дата";
            this.CB_Date.SelectedIndexChanged += new System.EventHandler(this.CB_Date_SelectedIndexChanged);
            // 
            // CB_Student
            // 
            this.CB_Student.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_Student.FormattingEnabled = true;
            this.CB_Student.Location = new System.Drawing.Point(484, 19);
            this.CB_Student.Margin = new System.Windows.Forms.Padding(15, 10, 10, 10);
            this.CB_Student.Name = "CB_Student";
            this.CB_Student.Size = new System.Drawing.Size(330, 31);
            this.CB_Student.TabIndex = 2;
            this.CB_Student.Text = "Студент";
            this.CB_Student.SelectedIndexChanged += new System.EventHandler(this.CB_Student_SelectedIndexChanged);
            // 
            // DATA_Temp1
            // 
            this.DATA_Temp1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DATA_Temp1.Location = new System.Drawing.Point(12, 565);
            this.DATA_Temp1.Name = "DATA_Temp1";
            this.DATA_Temp1.Size = new System.Drawing.Size(1305, 200);
            this.DATA_Temp1.TabIndex = 3;
            this.DATA_Temp1.Visible = false;
            // 
            // DATA_Temp2
            // 
            this.DATA_Temp2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DATA_Temp2.Location = new System.Drawing.Point(12, 565);
            this.DATA_Temp2.Name = "DATA_Temp2";
            this.DATA_Temp2.Size = new System.Drawing.Size(1305, 200);
            this.DATA_Temp2.TabIndex = 4;
            this.DATA_Temp2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Table);
            this.panel1.Location = new System.Drawing.Point(19, 70);
            this.panel1.Margin = new System.Windows.Forms.Padding(10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1291, 688);
            this.panel1.TabIndex = 5;
            // 
            // Table
            // 
            this.Table.AutoScroll = true;
            this.Table.ColumnCount = 1;
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table.Location = new System.Drawing.Point(0, 0);
            this.Table.Name = "Table";
            this.Table.RowCount = 1;
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.Size = new System.Drawing.Size(1289, 686);
            this.Table.TabIndex = 0;
            // 
            // DATA_Temp3
            // 
            this.DATA_Temp3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DATA_Temp3.Location = new System.Drawing.Point(12, 565);
            this.DATA_Temp3.Name = "DATA_Temp3";
            this.DATA_Temp3.Size = new System.Drawing.Size(1305, 200);
            this.DATA_Temp3.TabIndex = 10;
            this.DATA_Temp3.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 777);
            this.Controls.Add(this.DATA_Temp3);
            this.Controls.Add(this.DATA_Temp1);
            this.Controls.Add(this.DATA_Temp2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CB_Student);
            this.Controls.Add(this.CB_Date);
            this.Controls.Add(this.CB_Group);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Main";
            this.Text = "Рапортичка";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DATA_Temp1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DATA_Temp2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DATA_Temp3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox CB_Group;
        private System.Windows.Forms.ComboBox CB_Date;
        private System.Windows.Forms.ComboBox CB_Student;
        private System.Windows.Forms.DataGridView DATA_Temp1;
        private System.Windows.Forms.DataGridView DATA_Temp2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel Table;
        private System.Windows.Forms.DataGridView DATA_Temp3;
    }
}