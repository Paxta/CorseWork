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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        RadioButton[] radioButtons;
        int n;
        string sql;
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public static string id;
        /// <summary>
        /// Имя студента
        /// </summary>
        public static string name;
        /// <summary>
        /// Mail студента
        /// </summary>
        public static string email;
        /// <summary>
        ///  Название придмета
        /// </summary>
        public static string name_subject;
        /// <summary>
        /// Название теста
        /// </summary>
        public static string name_test;
        /// <summary>
        /// Время на прохождение теста
        /// </summary>
        public static int time;
        /// <summary>
        /// Количество вопросов в тесте
        /// </summary>
        public static int number_questions;
        /// <summary>
        /// Минимум процентов для прохождения тестирования
        /// </summary>
        public static int minimum_percent;
        /// <summary>
        /// Ссылка на тест
        /// </summary>
        public static string test_link;
        /// <summary>
        /// Таблица вопросов
        /// </summary>
        public static string table_guestions;
        /// <summary>
        /// id_test
        /// </summary>
        public static int id_test;
        SpeechSynthesizer synth = new SpeechSynthesizer();
        ComboBox comboBox;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            synth.SpeakAsyncCancelAll();
            Label label2 = new Label();
            label2.AutoSize = true;
            label2.ForeColor = Color.Red;
            label2.Location = new Point(260, 350);
            //fdasf
            //Получение доступа к БД SQL
            SqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,"Ошибка");
            }
            SqlCommand cmd = conn.CreateCommand();
            sql = "select * from table_students where IDStudent = '" + textBox1.Text + "'";
            cmd.CommandText = sql;
            DbDataReader reader = cmd.ExecuteReader();
            reader.Read();
            try
            {
                id = reader.GetString(0);
                name = reader.GetString(1);
                email = reader.GetString(3);
            }
            catch (Exception ex)
            {
                label2.Text = "Введен неверный идентификатор";
                synth.SpeakAsync(label2.Text);
                this.Controls.Add(label2);
            }
            reader.Close();
            //Проверка идентификатора
            if (textBox1.Text == id)//Если идентификатор совпадает
            {
                Controls.Clear();
                Height += 50;
                this.Text = "Выбор придмета";
                Label label = new Label();
                this.Controls.Add(label);
                label.AutoSize = true;
                label.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                label.Location = new Point(70, 10);
                //Получаем имя человека из SQL таблицы
                label.Text = name;//Выводим его имя
                label.Text += ", выберите тест для прохождения:";
                synth.SpeakAsync(label.Text);
                WebBrowser webBrowser2 = new WebBrowser();
                webBrowser2.Size = new Size(707,285);
                webBrowser2.ScrollBarsEnabled = false;
                webBrowser2.Location = new Point(125,28);
                Controls.Add(webBrowser2);
                webBrowser2.Navigate("https://i.gifer.com/9KVa.gif");
                //Получаем количество  тестов из SQL таблицы
                sql = "select COUNT(*) as count FROM name_test";
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                reader.Read();
                n = reader.GetInt32(0);
                reader.Close();
                comboBox = new ComboBox();
                comboBox.Location = new Point(215, 350);
                comboBox.Size = new Size(300, 55);
                comboBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                comboBox.Text = "Доступные по предметам";
                Controls.Add(comboBox);
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                // sql
                sql = "select name_subjects from dbo.name_test";

                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                for (int i=0;i<n;i++)
                {
                    reader.Read();
                    comboBox.Items.Add(reader.GetString(0));
                }
                reader.Close();
                conn.Close();
                //Создаём кнопку
                Button button1 = new Button();
                button1.Text = "Перейти к тестирование";
                button1.Cursor = Cursors.Hand;
                button1.FlatAppearance.BorderSize = 3;
                button1.FlatAppearance.MouseDownBackColor = Color.Red;
                button1.FlatAppearance.MouseOverBackColor =Color.Lime;
                button1.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                button1.Location = new Point(260,415);
                button1.Size = new Size(220, 40);
                Controls.Add(button1);
                button1.Click += new EventHandler(button1_Click);
            }
            else
            {
                label2.Text = "Введен неверный идентификатор";
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            synth.SpeakAsyncCancelAll();
            if (comboBox.SelectedIndex != -1)
            {
                name_subject = comboBox.Text;
                // Отправляем SQL запрос для получения его параметров (Времени, количества вопросов, минимальный проходной бал и.т.д)
                SqlConnection conn = DBUtils.GetDBConnection();
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Ошибка!");
                }
                //SQl
                SqlCommand cmd = conn.CreateCommand();
                //Какой тест был выбран
                sql = "select * from name_test where name_subjects = '" + name_subject + "'";
                cmd.CommandText = sql;
                DbDataReader reader = cmd.ExecuteReader();
                reader.Read();
                name_test = reader.GetString(1);//SQL
                time = reader.GetInt32(3);//SQL
                number_questions = reader.GetInt32(4);//SQL
                minimum_percent = reader.GetInt32(5);//SQL
                table_guestions = reader.GetString(6);//SQL    
                id_test = reader.GetInt32(7);//SQL    
                reader.Close();

                sql = "select IDStudent from[dbo].[Results] where IDStudent = '" + id + "' and IDTest = '" + id_test + "'";
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                reader.Read();
                try
                {
                    string gg = reader.GetString(0);//SQL
                    synth.SpeakAsync("Вы уже прошли это тестирование!!!");
                    MessageBox.Show("Вы уже прошли это тестирование!!!", "Тестирование пройдено");
                    synth.SpeakAsyncCancelAll();
                    Form7 form7 = new Form7();
                    form7.ShowDialog();
                    return;
                }
                catch
                {

                }
                reader.Close();
                Controls.Clear();
                this.Text = "Тема тестирования ";
                Label label = new Label();
                this.Controls.Add(label);
                label.AutoSize = true;
                label.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                label.Location = new Point(94, 10);
                label.Text = name_test + ".";// имя теста полученный в цикле name_test;
                synth.SpeakAsync(label.Text);
                Label labe2 = new Label();
                this.Controls.Add(labe2);
                labe2.AutoSize = true;
                labe2.Location = new Point(230, 315);
                labe2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                labe2.Text = name;
                labe2.Text += ", начинаем тестирование?\n";
                labe2.Text += "Всего вопросов: " + number_questions + "\n" + "Проходной бал: " + minimum_percent + "%\n" + "Время для прохождения: " + time + " минут.";
                synth.SpeakAsync(labe2.Text);
                WebBrowser webBrowser2 = new WebBrowser();
                webBrowser2.Size = new Size(707, 285);
                webBrowser2.ScrollBarsEnabled = false;
                webBrowser2.Location = new Point(125, 28);
                Controls.Add(webBrowser2);
                webBrowser2.Navigate("https://i.gifer.com/4GgJ.gif");
                Button button1 = new Button();
                button1.Text = "Начать тестирование!";
                button1.Cursor = Cursors.Hand;
                button1.FlatAppearance.BorderSize = 3;
                button1.FlatAppearance.MouseDownBackColor = Color.Red;
                button1.FlatAppearance.MouseOverBackColor = Color.Lime;
                button1.FlatStyle = FlatStyle.Flat;
                button1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                button1.Location = new Point(260, 400);
                button1.Size = new Size(200, 40);
                Controls.Add(button1);
                button1.Click += new EventHandler(button2_Click);
                conn.Close();
            }
            else
            {
                Label label3 = new Label();
                Controls.Add(label3);
                label3.AutoSize = true;
                label3.ForeColor = Color.Red;
                label3.Location = new Point(300, 400);
                label3.Text = "Выберите тестирование";
                synth.SpeakAsync(label3.Text);

            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Form3 questions = new Form3();
            questions.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://i.gifer.com/D5Ew.gif");
            synth.SpeakAsync(label1.Text);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

            Form1 form = new Form1();
            form.Close();
            synth.SpeakAsyncCancelAll();
        }
    }
}
