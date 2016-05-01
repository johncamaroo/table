using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace table
{
    public partial class Form2 : Form
    {
        public Form2(string data)
        {
            InitializeComponent();
            //Обрабатываем данные
            //Или записываем их в поле
            this.data = data;
        }
        string data;

        private void Form2_Load(object sender, EventArgs e)
        {
            switch (data)
            {
                case "save":
                    label1.Text = "Изменения сохранены";
                    break;
                case "deauthconf":
                    {
                        label1.Text = "Вы точно хотите деавторизоваться?";
                        break;
                    }
                case "C++":
                    Console.WriteLine("Вы выбрали язык С++");
                    break;
                default:
                    Console.WriteLine("Такой язык я не знаю");
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
