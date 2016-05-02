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
            int pupilcount, teachercount, i = 0;
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

            /*command.CommandText = "SELECT * FROM pupils";
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
           oleDbConnection1.Close();*/

            //command.CommandText = "SELECT * FROM teachers";
            //oleDbConnection1.Open();
            //OleDbDataReader reader2 = command.ExecuteReader();
            //string[] tun = new string[teachercount + 3];
            //string[] tps = new string[teachercount + 3];
            //while (reader2.Read())
            //{
            //    tun[i] = reader2["username"].ToString();
            //    tps[i] = reader2["password"].ToString();
            //    ++i;
            //}
            //oleDbConnection1.Close();

            string user = textBox1.Text;
            string pass = textBox2.Text;

            command.CommandText = "SELECT * FROM auth_pups";
            oleDbConnection1.Open();
            OleDbDataReader reader = command.ExecuteReader();
            string[,] pups = new string[3, pupilcount + 1]; //объявление массива [3, кол-во учеников+1]
            while (reader.Read())
            {
                pups[0, i] = reader["username"].ToString();
                pups[1, i] = reader["password"].ToString();
                pups[2, i] = reader["pups_id"].ToString();
                //richTextBox1.Text += pups[0, i] + " : " + pups[1, i] + " - " + pups[2, i] + '\n';
                ++i;
            }
            oleDbConnection1.Close();

            i = 0;

            command.CommandText = "SELECT * FROM auth_teachs";
            oleDbConnection1.Open();
            OleDbDataReader reader2 = command.ExecuteReader();
            string[,] teachs = new string[3, teachercount + 1]; //объявление массива [3, кол-во учителей+1]
            while (reader2.Read())
            {
                teachs[0, i] = reader2["username"].ToString();
                teachs[1, i] = reader2["password"].ToString();
                teachs[2, i] = reader2["teacher_id"].ToString();
                //richTextBox1.Text += teachs[0, i] + " : " + teachs[1, i] + " - " + teachs[2, i] + '\n';
                ++i;
            }
            oleDbConnection1.Close();

            i = 0;
            
            while (true)
            {
                for (i = 0; i<pupilcount;i++)
                {
                    if (user == pups[0,i])
                    {
                        if (pass == pups[1,i])
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
                }
                for (i = 0; i < teachercount; i++)
                {
                    if (user == teachs[0, i])
                    {
                        if (pass == teachs[1, i])
                        {
                            groupBox1.Visible = false;
                            groupBox2.Visible = true;
                            dataGridView1.Visible = true;
                            textBox1.Text = "";
                            textBox2.Text = "";
                            button2.Text = "Деавторизация";
                            break;
                        }
                    }
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
