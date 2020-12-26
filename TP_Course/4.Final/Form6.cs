using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using Tutorial.SqlConn;
using System.Speech.Synthesis;

namespace ЛАБОРАТОРНЫЕ_РАБОТЫ__ЯП_
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        SpeechSynthesizer synth = new SpeechSynthesizer();
        private void Form8_Load_1(object sender, EventArgs e)
        {
            synth.SpeakAsync(Text);
            // Количество пропущенных вопросов
            int pass_questions = Form2.number_questions - Form3.true_answers - Form3.false_answers;
            // Процентов набрано за тест
            double percent_test = Form3.true_answers * 100 / Form2.number_questions;
            label2.Text += Form2.number_questions;
            label3.Text += Form3.true_answers;
            label4.Text += Form3.false_answers;
            label5.Text += pass_questions;
            string pass = "False";
            if (percent_test>=Form2.minimum_percent)
            {
                label6.Text = "Вы прошли тестирование! Ваш результат:   "+percent_test+"%";
                pass = "True";
            }
            else
                label6.Text = "Вы не прошли тестирование. Ваш результат:   "+percent_test+"%";
            //Получение доступа к БД SQL
            SqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,"Ошибка!");
            }
            SqlCommand cmd = conn.CreateCommand();
            string sql = "insert into Results values ('" + Form2.id_test + "','" + Form2.id + "','" + Form3.true_answers + "','" +
            Form3.false_answers + "','" + pass_questions + "','" + percent_test + "','" + pass + "')";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
            chart1.Series["Правильные"].Points.AddY(Form3.true_answers);
            chart1.Series["Неправильные"].Points.AddY(Form3.false_answers);
            chart1.Series["Пропущенные"].Points.AddY(pass_questions);
        }
        int x = 540;
        int l = 1;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            l++;
            if (l < 330)
            {
                label2.Location = new Point(x, 300 + l);
                label3.Location = new Point(x, 330 + l);
                label4.Location = new Point(x, 360 + l);
                label5.Location = new Point(x, 390 + l);
            }
            else
            {
                timer1.Enabled = false;
                button1.Visible = true;
                button2.Visible = true;
                chart1.Visible = true;
                chart1.Location = new Point(530, 370);
                label6.Location = new Point(470, 340);
                label6.Visible = true;
                WebBrowser webBrowser1 = new WebBrowser();
                webBrowser1.ScrollBarsEnabled = false;
                Controls.Add(webBrowser1);
                WebBrowser webBrowser2 = new WebBrowser();
                webBrowser2.Visible = true;
                webBrowser2.ScrollBarsEnabled = false;
                Controls.Add(webBrowser2);
                WebBrowser webBrowser3 = new WebBrowser();
                webBrowser3.Location = new Point(1385, 630);
                webBrowser3.Size = new Size(120, 200);
                webBrowser3.Visible = true;
                webBrowser3.ScrollBarsEnabled = false;
                Controls.Add(webBrowser3);
                WebBrowser webBrowser4 = new WebBrowser();
                webBrowser4.Visible = true;
                webBrowser4.ScrollBarsEnabled = false;
                Controls.Add(webBrowser4);
                WebBrowser webBrowser5 = new WebBrowser();
                webBrowser5.Visible = true;
                webBrowser5.ScrollBarsEnabled = false;
                Controls.Add(webBrowser5);
                synth.SpeakAsync(label6.Text);
                synth.SpeakAsync(label3.Text);
                synth.SpeakAsync(label4.Text);
                if (Form3.true_answers * 100 / Form2.number_questions >= Form2.minimum_percent)
                {
                    webBrowser1.Location = new Point(430, 80);
                    webBrowser1.Size = new Size(500, 250);
                    webBrowser2.Location = new Point(0, 90);
                    webBrowser2.Size = new Size(450, 320);
                    webBrowser3.Location = new Point(1385, 630);
                    webBrowser3.Size = new Size(120, 200);
                    webBrowser4.Location = new Point(15, 440);
                    webBrowser4.Size = new Size(550, 550);
                    webBrowser5.Size = new Size(400, 400);
                    webBrowser5.Location = new Point(1100, 100);
                    webBrowser1.Navigate("https://i.gifer.com/47Bd.gif");
                    webBrowser2.Navigate("https://i.gifer.com/4GZA.gif");
                    webBrowser3.Navigate("https://i.gifer.com/hdt.gif");
                    webBrowser4.Navigate("https://i.gifer.com/1uJP.gif");
                    webBrowser5.Navigate("https://i.gifer.com/WG8R.gif");
                }
                else
                {
                    webBrowser1.Location = new Point(600, 210);
                    webBrowser1.Size = new Size(200, 200);
                    webBrowser2.Location = new Point(0, 90);
                    webBrowser2.Size = new Size(450, 320);
                    webBrowser3.Location = new Point(1350, 700);
                    webBrowser3.Size = new Size(120, 200);
                    webBrowser4.Location = new Point(280, 540);
                    webBrowser4.Size = new Size(200, 100);
                    webBrowser5.Size = new Size(400, 196);
                    webBrowser5.Location = new Point(1000, 350);
                    webBrowser1.Navigate("https://i.gifer.com/8t01.gif");
                    webBrowser2.Navigate("https://i.gifer.com/ZHDd.gif");
                    webBrowser3.Navigate("https://i.gifer.com/6kf.gif");
                    webBrowser4.Navigate("https://i.gifer.com/FleU.gif");
                    webBrowser5.Navigate("https://i.gifer.com/IQ69.gif");
                }
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            synth.SpeakAsyncCancelAll();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            synth.SpeakAsyncCancelAll();
            Form7 form7 = new Form7();
            form7.ShowDialog();
        }
    }
}
