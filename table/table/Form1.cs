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
        public OleDbCommand command;
        public Int16 whologin;
        public Int16 who=0;
        public DialogResult res = new DialogResult();
        public OleDbDataReader reader;


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
            command = oleDbConnection1.CreateCommand();
            command.CommandType = CommandType.Text;

            command.CommandText = "SELECT COUNT(*) FROM auth_pups";
            oleDbConnection1.Open();
            pupilcount = (int)command.ExecuteScalar();
            oleDbConnection1.Close();

            command.CommandText = "SELECT COUNT(*) FROM auth_teachs";
            oleDbConnection1.Open();
            teachercount = (int)command.ExecuteScalar();
            oleDbConnection1.Close();

            string user = textBox1.Text;
            string pass = textBox2.Text;

            command.CommandText = "SELECT * FROM auth_pups";
            string[,] pups = new string[3, pupilcount + 1]; //объявление массива [3, кол-во учеников+1]
            oleDbConnection1.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                pups[0, i] = reader["username"].ToString();
                pups[1, i] = reader["password"].ToString();
                pups[2, i] = reader["ID"].ToString();
                //richTextBox1.Text += pups[0, i] + " : " + pups[1, i] + " - " + pups[2, i] + '\n';
                ++i;
            }
            oleDbConnection1.Close();

            i = 0;

            command.CommandText = "SELECT * FROM auth_teachs";
            string[,] teachs = new string[3, teachercount + 1]; //объявление массива [3, кол-во учителей+1]
            oleDbConnection1.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                teachs[0, i] = reader["username"].ToString();
                teachs[1, i] = reader["password"].ToString();
                teachs[2, i] = reader["ID"].ToString();
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
                            who = 1;
                            whologin = Convert.ToInt16(pups[2, i]);
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
                            who = 2;
                            whologin = Convert.ToInt16(teachs[2, i]);
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
                 
            switch (who)
            {
                case 1:
                    this.marksTableAdapter.Fill(this.tabledbDataSet1.marks);
                    break;
                case 2:
                    {
                        List<string> fid = new List<string>(); // form_id
                        List<string> fn = new List<string>(); // pups_surname, pups_name
                        int j = 0, mark = 0;
                        string sid = teachs[2, i];

                        command = oleDbConnection1.CreateCommand();
                        command.CommandType = CommandType.Text;      

                        command.CommandText = "SELECT form_id FROM [table] WHERE (subject_id =" + sid + ")";
                        
                        oleDbConnection1.Open();
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            fid.Add(reader["form_id"].ToString());
                        }
                        oleDbConnection1.Close();

                        
                        while (j < fid.Count())
                        {
                            oleDbConnection1.Open();
                            command.CommandText = "SELECT forms.form_year, forms.form_letter, pupils.pups_surname, pupils.pups_name, pupils.pupils_id FROM (forms INNER JOIN pupils ON forms.ID = pupils.form_id) WHERE (forms.ID =" + fid[j] + ")";
                            reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                fn.Add(reader["form_year"].ToString());
                                fn.Add(reader["form_letter"].ToString());
                                fn.Add(reader["pups_surname"].ToString());
                                fn.Add(reader["pups_name"].ToString());
                                fn.Add(reader["pupils_id"].ToString());                                
                            }                           
                            j++;
                            oleDbConnection1.Close();
                        }

                        j = 0;

                        oleDbConnection1.Open();
                        do
                        {
                            command.CommandText = "SELECT mark FROM [marks] WHERE (pupil_id =" + fn[j * 5 + 4] + ") AND (subject_id =" + sid + ")";
                            richTextBox1.Text += fn.Count + " " + fn[j * 5 + 4] + " " + sid + '\n';
                            if (command.ExecuteScalar() == null)
                            {
                                mark = 0;
                            }
                            else mark = (int)command.ExecuteScalar();
                            dataGridView1.Rows.Add(fn[j * 5], fn[j * 5 + 1], fn[j * 5 + 2], fn[j * 5 + 3], mark);
                            j++;
                        } while (j < fn.Count() /5 );
                        oleDbConnection1.Close();
                        break;
                    }
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
