using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data.Common;
using Tutorial.SqlConn;
using System.Speech.Synthesis;


namespace ЛАБОРАТОРНЫЕ_РАБОТЫ__ЯП_
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        SpeechSynthesizer synth = new SpeechSynthesizer();
        private void Form7_Load(object sender, EventArgs e)
        {
            synth.SpeakAsync("Отправить результат на почту?"+groupBox1.Text);
            textBox1.Text = Form2.email;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            DbDataReader reader;
            string sql, gg;
            int trueAns, wrongAns, missedAns;
            double proc;
            bool pass;
            try
            {
                textBox1.Text = textBox1.Text.Replace(" ", "");
                if (!textBox1.Text.Contains("@"))
                {
                    MessageBox.Show("Ввод не коректный", "Ошибка ввода");
                }
                else
                {
                    MailAddress otpravitel = new MailAddress("testingmtuci@gmail.com", "Тестирование(МТУСИ)");
                    MailAddress poluchatel = new MailAddress(textBox1.Text);
                    MailMessage message = new MailMessage(otpravitel, poluchatel);
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(otpravitel.Address, "Testing001");
                    message.Subject = Form2.name + ", результат тестирования";
                    //Получение доступа к БД SQL
                    SqlConnection conn = DBUtils.GetDBConnection();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    sql = "select TrueAnswer, WrongAnswer, MissedAnswer, Procent, Result from Results where IDStudent = '" + Form2.id + "' and IDTest = '" + Form2.id_test + "'";
                    cmd.CommandText = sql;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    trueAns = reader.GetInt32(0);
                    wrongAns = reader.GetInt32(1);
                    missedAns = reader.GetInt32(2);
                    proc = reader.GetDouble(3);
                    pass = reader.GetBoolean(4);
                    reader.Close();
                    if (pass)
                        gg = "Вы прошли тест";
                    else
                        gg = "Вы не прошли тест";
                    message.Body = "<h2>Здравствуйте, " + Form2.name + "!</h2><br>Отправляем вам результаты тестирования по предмету " + Form2.name_subject + " (" + Form2.name_test + ")<br>" +
                        "Правильных ответов: " + trueAns + "<br>Неверных ответов: " + wrongAns + "<br>Пропущеных вопросов: " + missedAns + "<br>Ваш результат: " + proc + "% (" + gg + ")";
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                    MessageBox.Show("Результат тестирования успешно отправлен на указанную почту.", "Результат отправлен");
                    Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            synth.SpeakAsyncCancelAll();
        }
    }
}
