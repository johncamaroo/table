using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace table
{
    public partial class Form1 : Form
    {
        DialogResult res = new DialogResult();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            int pupilcount, teachercount, i = 0, j = 0;
            OleDbCommand command = oleDbConnection1.CreateCommand();
            command.CommandType = CommandType.Text;

            command.CommandText = "SELECT COUNT(*) FROM pupils";
            oleDbConnection1.Open();
            pupilcount = (int)command.ExecuteScalar();
            oleDbConnection1.Close();

            command.CommandText = "SELECT COUNT(*) FROM teachers";
            oleDbConnection1.Open();
            teachercount = (int)command.ExecuteScalar();
            oleDbConnection1.Close();

            command.CommandText = "SELECT * FROM pupils";
            oleDbConnection1.Open();
            OleDbDataReader reader = command.ExecuteReader();
            string[] pun = new string[pupilcount + 1];
            string[] pps = new string[pupilcount + 1];
            while (reader.Read())
            {
                pun[i] = reader["username"].ToString();
                pps[i] = reader["password"].ToString();
                ++i;
            }             
            oleDbConnection1.Close();
            i = 0;
            command.CommandText = "SELECT * FROM teachers";
            oleDbConnection1.Open();
            OleDbDataReader reader2 = command.ExecuteReader();
            string[] tun = new string[teachercount + 3];
            string[] tps = new string[teachercount + 3];
            while (reader2.Read())
            {
                tun[i] = reader2["username"].ToString();
                tps[i] = reader2["password"].ToString();
                ++i;
            }
            oleDbConnection1.Close();

            string user = textBox1.Text;
            string pass = textBox2.Text;

            i = 0;
            while (true)
            {
                while (i < pupilcount)
                {
                    if (user == pun[i])
                    {
                        if (pass == pps[i])
                        {
                            groupBox1.Visible = false;
                            groupBox2.Visible = false;
                            dataGridView1.Visible = true;
                            textBox1.Text = "";
                            textBox2.Text = "";
                            button2.Text = "Деавторизация";
                            break;
                        }
                    }
                    i++;
                }

                while (j < teachercount)
                {

                    if (user == tun[j])
                    {
                        if (pass == tps[j])
                        {
                            groupBox1.Visible = false;
                            dataGridView1.Visible = true;
                            textBox1.Text = "";
                            textBox2.Text = "";
                            button2.Text = "Деавторизация";
                            groupBox2.Visible = true;
                            break;
                        }
                    }
                    j++;
                }
               
                label3.Text = "Incorrect username or password";
                break;
            }
            
               

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == false)
                textBox2.UseSystemPasswordChar = true;
            else if (textBox2.UseSystemPasswordChar == true)
                textBox2.UseSystemPasswordChar = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            if (button2.Text == "Деавторизация")
            {
                res = MessageBox.Show("Вы действительно хотите деавторизоваться?", "Деавторизация", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    button2.Text = "Авторизация";
                    dataGridView1.Visible = false;
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                }
                else return;
            }
            groupBox1.Visible = true;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            res = MessageBox.Show("Изменения сохранены","",MessageBoxButtons.OK);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }
    }
}
