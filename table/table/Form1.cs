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
            int n, i = 0;
            OleDbCommand command = oleDbConnection1.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT COUNT(*) FROM auth";
            oleDbConnection1.Open();
            n = (int)command.ExecuteScalar();
            oleDbConnection1.Close();
            command.CommandText = "SELECT * FROM auth";
            oleDbConnection1.Open();
            OleDbDataReader reader = command.ExecuteReader();
            string[] users = new string[n + 1];
            string[] passes = new string[n + 1];

            while (reader.Read())
            {
                users[i] = reader["username"].ToString();
                passes[i] = reader["password"].ToString();
                ++i;
            }
             
            oleDbConnection1.Close();
            string user = textBox1.Text;
            string pass = textBox2.Text;
            for (i = 0; i < n; i++)
            {
                if (user == users[i])
                {
                    if (pass == passes[i])
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
                //label3.ForeColor = System.Drawing.Color.Red;
                label3.Text = "Incorrect username or password";
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
            if(button2.Text == "Деавторизация")
            {
                res = MessageBox.Show("Вы действительно хотите деавторизоваться?", "Деавторизация", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    button2.Text = "Авторизация";
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
