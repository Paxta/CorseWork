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
    public partial class Form3 : Form
    {
        string sql;
        string[][] mas_question;
        int I = 0;
        /// <summary>
        /// Для проверки правильности ответа
        /// </summary>
        public static bool answers = false;
        RadioButton[] radioButton;
        CheckBox[] checkBox;
        CheckedListBox checkedListBoxes1 = new CheckedListBox();
        ComboBox comboBox1 = new ComboBox();
        GroupBox groupBox2 = new GroupBox();
        HScrollBar hScrollBar1;
        Label label2;
        Label label3;
        Label label6;
        Label label7;
        Label label8;
        Label label9;
        TextBox[] textBoxes;
        ListBox listBox1;
        Button button1 = new Button();
        Button button2 = new Button();
        DateTime endTime = new DateTime();
        TimeSpan remainingTime;
        TextBox textBox1;
        GroupBox groupBox1;
        Button button_off;
        Button[] button;
        public Form3()
        {
            InitializeComponent();
        }
        SpeechSynthesizer synth = new SpeechSynthesizer();
        /// <summary>
        /// Количество правильных ответов
        /// </summary>
        public static int true_answers = 0;
        /// <summary>
        /// Количество неправильные ответы
        /// </summary>
        public static int false_answers = 0;
        /// <summary>
        /// Массив сохранения ответов каждого элемента
        /// </summary>
        string[] mas_answers;
        /// <summary>
        /// Количество вопросов в кнопках
        /// </summary>
        Button[] buttons = new Button[Form2.number_questions];
        /// <summary>
        /// Вопрос
        /// </summary>
        public static string question = "";
        WebBrowser webBrowser = new WebBrowser();
        Label label = new Label();
        private void show_Click(object sender, EventArgs e)
        {
            Form showDialog =new Form();
            //Веб браузер для видео
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            label.Location = new Point(120, -5);
            label.Size = new Size(0, 29);
            webBrowser.Location = new Point(-5, 17);
            webBrowser.ScrollBarsEnabled = false;
            webBrowser.Size = new Size(659, 341);
            Random random = new Random();
            int rand = random.Next(1, 5);
            if (answers==true)
            {
                label.Text = "Вы ответили правильно";
                label.ForeColor = Color.Lime;
                showDialog.Text = "Правильный ответ";
                if (rand == 1)
                {
                    webBrowser.Navigate("https://i.gifer.com/9KVu.gif");
                }
                if (rand == 2)
                {
                    webBrowser.Navigate("https://i.gifer.com/9KVx.gif");
                }
                if (rand == 3)
                {
                    webBrowser.Navigate("https://i.gifer.com/9KVv.gif");
                }
                if (rand == 4)
                {
                    webBrowser.Navigate("https://i.gifer.com/9KVw.gif");
                }
            }
            else
            {
                label.Text = "Вы ответили не правильно";
                label.ForeColor = Color.Red;
                showDialog.Text = "Ответ не верный";
                if (rand == 1)
                {
                    webBrowser.Navigate("https://i.gifer.com/sWa.gif");
                }
                if (rand == 2)
                {
                    webBrowser.Navigate("https://i.gifer.com/B65f.gif");
                }
                if (rand == 3)
                {
                    webBrowser.Navigate("https://i.gifer.com/D5Ep.gif");
                }
                if (rand == 4)
                {
                    webBrowser.Navigate("https://i.gifer.com/1Ei9.gif");
                }
            }
            synth.SpeakAsync(label.Text);
            //showDialog.AutoSize = true;
            showDialog.BackColor = SystemColors.ButtonHighlight;
            showDialog.Size = new Size(510,350);
            //showDialog.ClientSize = new Size(658, 391);
            showDialog.Controls.Add(webBrowser);
            showDialog.Controls.Add(label);
            showDialog.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            showDialog.SizeGripStyle =SizeGripStyle.Show;
            showDialog.StartPosition = FormStartPosition.CenterScreen;
            showDialog.ResumeLayout(false);
            showDialog.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            showDialog.FormClosing += new System.Windows.Forms.FormClosingEventHandler(showDialog_FormClosing);
            showDialog.ShowDialog();
        }
        private void showDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            int n = Form2.number_questions;// Из SQL таблицы взять количество
            SqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!", "" + ex.Message);
            }
            SqlCommand cmd = conn.CreateCommand();
            //DbDataReader reader;
            mas_question = new string[Form2.number_questions][];
            for (int i = 0; i < Form2.number_questions; i++)
            {
                mas_question[i] = new string[10];
                sql = "select * from " + Form2.table_guestions + " where IDTest = '" + i + "'";
                cmd.CommandText = sql;
                DbDataReader reader = cmd.ExecuteReader();
                reader.Read();
                mas_question[i][0] = "no";
                mas_question[i][1] = "" + reader.GetString(0);
                mas_question[i][2] = "" + reader.GetString(1);
                mas_question[i][3] = "" + reader.GetInt32(2);
                mas_question[i][4] = "" + reader.GetString(3);
                mas_question[i][5] = "" + reader.GetInt32(4);
                mas_question[i][6] = "" + reader.GetString(5);
                mas_question[i][7] = "";
                mas_question[i][8] = "";
                reader.Close();
            }
            //Создание lable для отображения Названия тестирования.....
            Label label1 = new Label();
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            label1.Location = new Point(125, 0);
            label1.Text = "" + Form2.name_subject + ". " + Form2.name_test;
            Controls.Add(label1);
            //Создание кнопки ответить
            button1 = new Button();
            button1.FlatAppearance.BorderSize = 3;
            button1.FlatAppearance.MouseDownBackColor = Color.Red;
            button1.FlatAppearance.MouseOverBackColor = Color.Lime;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            button1.Location = new Point(700, 751);//960, 841
            button1.Size = new Size(182, 51);
            button1.Text = "Ответить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            Controls.Add(button1);
            //Создание кнопки пропустить 
            button2 = new Button();
            button2.FlatAppearance.BorderSize = 3;
            button2.FlatAppearance.MouseDownBackColor = Color.Red;
            button2.FlatAppearance.MouseOverBackColor = Color.Lime;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            button2.Location = new Point(1300, 751);
            button2.Size = new Size(234, 51);
            button2.Text = "Пропустить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(this.button2_Click);
            Controls.Add(button2);
            //
            for (int i = 0; i < Form2.number_questions; i++)
            {
                buttons[i] = new Button();
                buttons[i].FlatAppearance.BorderSize = 3;
                buttons[i].FlatAppearance.MouseDownBackColor = Color.Orange;
                buttons[i].FlatAppearance.MouseOverBackColor = Color.Yellow;
                buttons[i].FlatStyle = FlatStyle.Flat;
                buttons[i].Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                buttons[i].Location = new Point(10 + (i * 54), 680);
                buttons[i].Size = new Size(50, 50);
                buttons[i].Text = "" + (i+1);
                buttons[i].Name = "" + i;
                buttons[i].UseVisualStyleBackColor = true;
                Controls.Add(buttons[i]);
                buttons[i].Click += new EventHandler(buttonsnumber_Click);
            }
            // 
            label2 = new Label();
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 22.8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            label2.Location = new Point(700, 70);
            label2.ForeColor = Color.Red;
            label2.Text = "Вопрос №";
            Controls.Add(label2);
            //
            label6 = new Label();
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            label6.Location = new Point(5, 650);
            label6.ForeColor = Color.Orange;
            label6.Text = "Осталось вопросов: ";
            Controls.Add(label6);
            //
            label7 = new Label();
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            label7.Location = new Point(10, 730);
            label7.ForeColor = Color.Lime;
            label7.Text = "Правильных ответов: ";
            Controls.Add(label7);
            //
            label8 = new Label();
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            label8.Location = new Point(10, 755);
            label8.ForeColor = Color.Red;
            label8.Text = "Не правильных ответов: ";
            Controls.Add(label8);
            //Создание окна вопросов
            textBox1 = new TextBox();
            textBox1.Font = new Font("Microsoft Sans Serif", 28.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            textBox1.Location = new Point(10, 120);
            textBox1.Multiline = true;
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(1520, 50);
            textBox1.Click += new EventHandler(textBox1_Click);
            Controls.Add(textBox1);
            // Включение таймера
            var minutes = Form2.time; //////////////////////////Из sql таблицы взять количество минут
            DateTime start = DateTime.UtcNow; //Получаем текущюю дату
            endTime = start.AddMinutes(minutes);//Добавлеям заданую минуту в текущюю дату
            label9 = new Label();
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            label9.Location = new Point(10, 780);
            label9.ForeColor = Color.YellowGreen;
            label9.Text = "Осталось времени: " + (remainingTime = endTime - DateTime.UtcNow).ToString(@"hh\:mm\:ss");//Выводим таймер
            Func(sender, e);
            Controls.Add(label9);
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            Form4 test = new Form4();
            test.ShowDialog();
        }
        private void buttonsnumber_Click(object sender, EventArgs e)
        {
            Fun_button_Colour(sender, e);
            I = Convert.ToInt32((sender as Button).Name);
            Func(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //При каждом тике отнимаем секунду
            remainingTime = endTime - DateTime.UtcNow;
            if (remainingTime < TimeSpan.Zero)
            {
                timer1.Enabled = false;
                Close();
                Form6 form6 = new Form6();
                form6.Show();
            }
            else
            {
                label9.Text = "Осталось времени: " + remainingTime.ToString(@"hh\:mm\:ss"); ;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (mas_question[I][1].Equals("RadioButton"))
            {
                string answer = mas_question[I][6];
                for (int i = 0; i < Convert.ToInt32(mas_question[I][3]); i++)
                {
                    if (radioButton[i].Checked == true)
                    {
                        mas_question[I][7] = ""+i;
                        synth.SpeakAsyncCancelAll();
                        if (radioButton[i].Text.Equals(answer))
                        {
                            answers = true;
                            true_answers++;
                            buttons[I].BackColor = Color.Lime;
                            mas_question[I][8] = "1";
                        }
                        else
                        {
                            false_answers++;
                            answers = false;
                            buttons[I].BackColor = Color.Red;
                            mas_question[I][8] = "0";
                        }
                        mas_question[I][0] = "yes";
                        show_Click(sender, e);
                        button2_Click(sender, e);
                        break;
                    }
                }
            }
            else if (mas_question[I][1].Equals("CheckBox"))
            {
                synth.SpeakAsyncCancelAll();
                string[] answer = mas_question[I][6].Split(';');
                int sum_answer = 0;
                int sum_checkBoxChecked = 0;
                for (int i = 0; i < Convert.ToInt32(mas_question[I][3]); i++)
                {
                    if (checkBox[i].Checked == true)
                    {
                        mas_question[I][7] += ""+i+";";
                        for (int j = 0; j < answer.Length; j++)
                        {
                            if (answer[j].Equals(checkBox[i].Text))
                            {
                                sum_answer++;
                                break;
                            }
                        }
                        sum_checkBoxChecked++;
                    }
                }
                if (sum_checkBoxChecked != 0)
                {
                    synth.SpeakAsyncCancelAll();
                    if (sum_checkBoxChecked == sum_answer && sum_answer == Convert.ToInt32(mas_question[I][5]))
                    {
                        answers = true;
                        true_answers++;
                        buttons[I].BackColor = Color.Lime;
                        mas_question[I][8] = "1";
                    }
                    else
                    {
                        false_answers++;
                        answers = false;
                        buttons[I].BackColor = Color.Red;
                        mas_question[I][8] = "0";
                    }
                    mas_question[I][0] = "yes";
                    show_Click(sender, e);
                    button2_Click(sender, e);
                }
            }
            else if (mas_question[I][1].Equals("ComboBox"))
            {
                string answer = mas_question[I][6];
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберете ответ!", "Ответ не выбран");
                }
                else
                {
                    mas_question[I][7] =""+comboBox1.SelectedIndex;
                    if (comboBox1.Text.Equals(answer))
                    {
                        answers = true;
                        true_answers++;
                        buttons[I].BackColor = Color.Lime;
                        mas_question[I][8] = "1";
                    }
                    else
                    {

                        false_answers++;
                        answers = false;
                        buttons[I].BackColor = Color.Red;
                        mas_question[I][8] = "0";
                    }
                    synth.SpeakAsyncCancelAll();
                    mas_question[I][0] = "yes";
                    show_Click(sender, e);
                    button2_Click(sender, e);
                }
            }
            else if (mas_question[I][1].Equals("HScrollBar"))
            {
                synth.SpeakAsyncCancelAll();
                string answer = mas_question[I][6];
                mas_question[I][7] = "" + label3.Text;
                if (label3.Text.Equals(answer))
                {
                    buttons[I].BackColor = Color.Lime;
                    answers = true;
                    true_answers++;
                    mas_question[I][8] = "1";
                }
                else
                {
                    buttons[I].BackColor = Color.Red;
                    false_answers++;
                    answers = false;
                    mas_question[I][8] = "0";
                }
                synth.SpeakAsyncCancelAll();
                mas_question[I][0] = "yes";
                show_Click(sender, e);
                button2_Click(sender, e);
            }
            else if (mas_question[I][1].Equals("ListBox"))
            {
                synth.SpeakAsyncCancelAll();
                string[] answer = mas_question[I][6].Split(';');
                int sum_true_answer = 0;
                int sum_answer = 0;
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    mas_question[I][7] += textBoxes[i].Text +";";
                    if ((textBoxes[i].Text).Equals(answer[i]))
                    {
                        sum_true_answer++;
                    }
                    if (!textBoxes[i].Text.Equals(""))
                    {
                        sum_answer++;
                    }
                    if(sum_answer==answer.Length||sum_true_answer==answer.Length)
                    {
                        if (sum_true_answer == answer.Length)
                        {
                            buttons[I].BackColor = Color.Lime;
                            answers = true;
                            true_answers++;
                            mas_question[I][8] = "1";
                        }
                        else
                        {
                            buttons[I].BackColor = Color.Red;
                            answers = false;
                            false_answers++;
                            mas_question[I][8] = "0";
                        }
                        synth.SpeakAsyncCancelAll();
                        mas_question[I][0] = "yes";
                        show_Click(sender, e);
                        button2_Click(sender, e);
                        break;
                    }
                }
                if(sum_answer != answer.Length)
                {
                    MessageBox.Show("Поля надо заполнить", "Заполните все поля");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            synth.SpeakAsyncCancelAll();
            if (true_answers + false_answers == Form2.number_questions)
            {
                Close();
                Form6 form6 = new Form6();
                form6.Show();
            }
            else
            {
                Fun_button_Colour(sender, e);
                I = I + 1;
                if (I >= Form2.number_questions)
                {
                    I = 0;
                }
                if (!mas_question[I][0].Equals("no"))
                {
                    button2_Click(sender, e);
                }
                else
                {
                    Func(sender, e);
                }
            }
        }
        private void Func(object sender, EventArgs e)
        {
            synth.SpeakAsyncCancelAll();
            label6.Text = "Осталось вопросов:  " + (Form2.number_questions-true_answers-false_answers);
            label7.Text = "Правильных ответов: "+true_answers;
            label8.Text = "Не верных ответов:  "+false_answers;
            buttons[I].BackColor = Color.Yellow;
            mas_answers = mas_question[I][4].Split(';');
            comboBox1.Size = new Size(0, 0);
            Controls.Remove(groupBox1);
            groupBox1 = new GroupBox();
            groupBox1.Font = new Font("Microsoft Sans Serif", 19.8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            groupBox1.Location = new Point(560, 250);
            groupBox1.TabStop = false;
            groupBox1.Text = "Выберите правильный ответ:";
            groupBox1.Width = 500;
            Controls.Add(groupBox1);
            groupBox1.Height = 0;
            Controls.Add(groupBox1);
            groupBox2.Size = new Size();
            textBox1.Text = mas_question[I][2];
            question = mas_question[I][2];
            button2.Text = "Пропустить";
            button1.Visible = true;
            synth.SpeakAsync(textBox1.Text);
            label2.Text = "Вопрос №" +(I+1);
            if (mas_question[I][1].Equals("RadioButton"))
            {
                groupBox1.Height = 35;
                radioButton = new RadioButton[Convert.ToInt32(mas_question[I][3])];
                synth.SpeakAsync(groupBox1.Text);
                for (int i = 0; i < radioButton.Length; i++)
                {
                    radioButton[i] = new RadioButton();
                    radioButton[i].AutoSize = true;
                    radioButton[i].Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    radioButton[i].Name = "radioButtons" + i;
                    radioButton[i].Text = mas_answers[i];
                    synth.SpeakAsync(mas_answers[i]);
                    radioButton[i].Location = new Point(5, 35 + (i * 30));
                    groupBox1.Height += 30;
                    groupBox1.Controls.Add(radioButton[i]);
                }
            }
            else if (mas_question[I][1].Equals("CheckBox"))
            {
                groupBox1.Text = "Выбирите правильные ответы!";
                checkBox = new CheckBox[Convert.ToInt32(mas_question[I][3])];
                groupBox1.Height = 35;
                synth.SpeakAsync(groupBox1.Text);
                for (int i = 0; i < checkBox.Length; i++)
                {
                    groupBox1.Height += 30;
                    checkBox[i] = new CheckBox();
                    checkBox[i].AutoSize = true;
                    checkBox[i].Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    checkBox[i].Name = "checkBox" + i;
                    checkBox[i].Text = mas_answers[i];
                    synth.SpeakAsync(mas_answers[i]);
                    checkBox[i].Location = new Point(5, 35 + (i * 30));
                    groupBox1.Controls.Add(checkBox[i]);
                }
            }
            else if (mas_question[I][1].Equals("ComboBox"))
            {
                Controls.Remove(comboBox1);
                comboBox1 = new ComboBox();
                comboBox1.Text = "Выберите правильный ответ";
                comboBox1.Font = new Font("Microsoft Sans Serif", 25.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(mas_answers);
                synth.SpeakAsync(comboBox1.Text);
                for (int i = 0; i < Convert.ToInt32(mas_question[I][3]); i++)
                {
                    synth.SpeakAsync(mas_answers[i]);
                }
                comboBox1.Location = new Point(425, 300);
                comboBox1.Size = new Size(678, 59);
                mas_answers = null;
                Controls.Add(comboBox1);
            }
            else if (mas_question[I][1].Equals("HScrollBar"))
            {
                groupBox1.Text = "Прокрутите до нужного числа";
                synth.SpeakAsync(groupBox1.Text);
                hScrollBar1 = new HScrollBar();
                hScrollBar1.LargeChange = 2;
                hScrollBar1.Location = new Point(80, 40);
                hScrollBar1.Maximum = Convert.ToInt32(mas_answers[1]) + 1;
                hScrollBar1.Minimum = Convert.ToInt32(mas_answers[0]);
                hScrollBar1.Name = "hScrollBar1";
                hScrollBar1.Size = new Size(866, 30);
                hScrollBar1.TabIndex = 0;
                hScrollBar1.Value = (hScrollBar1.Minimum + hScrollBar1.Maximum) / 2;
                groupBox1.Controls.Add(hScrollBar1);
                hScrollBar1.Scroll += new ScrollEventHandler(hScrollBar1_Scroll);
                groupBox1.Height = 115;
                groupBox1.Width = 1020;
                groupBox1.Location = new Point(255, 250);

                label3 = new Label();
                label3.AutoSize = true;
                label3.Location = new Point(465, 70);
                label3.Margin = new Padding(4, 0, 4, 0);
                label3.TabIndex = 1;
                label3.Text = "" + hScrollBar1.Value;
                groupBox1.Controls.Add(label3);

                Label label4 = new Label();
                label4.AutoSize = true;
                label4.Location = new Point(7, 35);
                label4.Margin = new Padding(4, 0, 4, 0);
                label4.TabIndex = 1;
                label4.Text = " min" + "\n" + hScrollBar1.Minimum; ;
                groupBox1.Controls.Add(label4);

                Label label5 = new Label();
                label5.AutoSize = true;
                label5.Location = new Point(940, 35);
                label5.Margin = new Padding(4, 0, 4, 0);
                label5.TabIndex = 1;
                label5.Text = " max" + "\n" + (hScrollBar1.Maximum - 1);
                groupBox1.Controls.Add(label5);
            }
            else if (mas_question[I][1].Equals("ListBox"))
            {
                Controls.Remove(groupBox2);
                groupBox2 = new GroupBox();
                groupBox2.Text = "Разместите в поля ответы по порядку";
                groupBox2.Font = new Font("Microsoft Sans Serif", 15.8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                groupBox2.Location = new Point(460, 200);
                groupBox2.TabStop = false;
                groupBox2.Width = 700;
                groupBox2.Height = 130;
                Controls.Add(groupBox2);
                synth.SpeakAsync(groupBox2.Text);
                listBox1 = new ListBox();
                listBox1.FormattingEnabled = true;
                listBox1.ItemHeight = 36;
                listBox1.Location = new Point(10, 30);
                listBox1.Name = "listBox1";
                listBox1.Size = new Size(670, 100);
                groupBox2.Controls.Add(listBox1);
                button = new Button[Convert.ToInt32(mas_question[I][3]) / 2];
                GroupBox[] groupBoxes = new GroupBox[Convert.ToInt32(mas_question[I][3]) / 2];
                textBoxes = new TextBox[Convert.ToInt32(mas_question[I][3]) / 2];
                for (int i = 0; i < Convert.ToInt32(mas_question[I][3]) / 2; i++)
                {
                    listBox1.Items.Add(mas_answers[Convert.ToInt32(mas_question[I][3]) / 2 + i]);
                    groupBox2.Height += 90;
                    groupBoxes[i] = new GroupBox();
                    groupBoxes[i].Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                    groupBoxes[i].Location = new Point(10, 120 + (i * 80));
                    groupBoxes[i].Size = new Size(670, 60);
                    groupBoxes[i].Text = mas_answers[i];
                    synth.SpeakAsync(mas_answers[i]);
                    button[i] = new Button();
                    button[i].FlatAppearance.MouseDownBackColor = Color.Red;
                    button[i].FlatAppearance.MouseOverBackColor = Color.Lime;
                    button[i].FlatStyle = FlatStyle.Flat;
                    button[i].Location = new Point(5, 20);
                    button[i].Size = new Size(45, 35);
                    button[i].Name = "" + i;
                    button[i].Text = "->";
                    button[i].UseVisualStyleBackColor = true;
                    textBoxes[i] = new TextBox();
                    textBoxes[i].Location = new Point(65, 25);
                    textBoxes[i].Size = new Size(600, 35);
                    textBoxes[i].Enabled = false;
                    groupBoxes[i].Controls.Add(button[i]);
                    groupBoxes[i].Controls.Add(textBoxes[i]);
                    groupBox2.Controls.Add(groupBoxes[i]);
                    button[i].Click += new EventHandler(button_Click);
                }
                button_off = new Button();
                button_off.FlatAppearance.MouseDownBackColor = Color.Red;
                button_off.FlatAppearance.MouseOverBackColor = Color.Lime;
                button_off.FlatStyle = FlatStyle.Flat;
                button_off.Location = new Point(250, groupBox2.Height-57);
                button_off.Size = new Size(150, 50);
                button_off.Name = "off";
                button_off.Text = "Сбросить";
                button_off.UseVisualStyleBackColor = true;
                groupBox2.Controls.Add(button_off);
                button_off.Click += new EventHandler(button_off_Click);
            }
            if (!mas_question[I][0].Equals("no"))
            {
                synth.SpeakAsyncCancelAll();
                button2.Text = "Cледующий";
                button1.Visible = false;
                mas_answers = mas_question[I][4].Split(';');
                if (mas_question[I][1].Equals("RadioButton"))
                {
                    int answer_number = Convert.ToInt32(mas_question[I][7]);
                    radioButton[answer_number].Checked = true;
                    radioButton[answer_number].BackColor = Color.Red;
                    if (mas_question[I][8].Equals("0"))
                        groupBox1.Text = "Вы ответили не правильно!";
                    else
                        groupBox1.Text = "Вы ответили правильно!";
                    for (int i = 0; i < radioButton.Length; i++)
                    {
                        radioButton[i].Enabled = false;
                        if (mas_question[I][6].Equals(radioButton[i].Text))
                        {
                            radioButton[i].BackColor = Color.Lime;
                        }
                    }
                }
                else if (mas_question[I][1].Equals("CheckBox"))
                {
                    string[] answer_number = mas_question[I][7].Split(';');
                    string[] mas_answer = mas_question[I][6].Split(';');
                    if (mas_question[I][8].Equals("0"))
                        groupBox1.Text = "Вы ответили не правильно!";
                    else
                        groupBox1.Text = "Вы ответили правильно!";
                    for (int i = 0; i < Convert.ToInt32(answer_number.Length) - 1; i++)
                    {
                        checkBox[Convert.ToInt32(answer_number[i])].Checked = true;
                        checkBox[Convert.ToInt32(answer_number[i])].BackColor = Color.Red;
                    }
                    for (int i = 0; i < checkBox.Length; i++)
                    {
                        checkBox[i].Enabled = false;
                        for (int j = 0; j < mas_answer.Length; j++)
                        {
                            if (checkBox[i].Text.Equals(mas_answer[j]))
                            {
                                checkBox[i].BackColor = Color.Lime;
                            }
                        }
                    }
                }
                else if (mas_question[I][1].Equals("ComboBox"))
                {
                    if (mas_question[I][8].Equals("0"))
                        comboBox1.Text = "Вы ответили не правильно!";
                    else
                        comboBox1.Text = "Вы ответили правильно!";
                    comboBox1.DrawItem += comboBox1_DrawItem;
                    comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
                }
                else if (mas_question[I][1].Equals("HScrollBar"))
                {
                    if (mas_question[I][8].Equals("0"))
                        groupBox1.Text = "Вы ответили не правильно!";
                    else
                        groupBox1.Text = "Вы ответили правильно!";
                    label3.Text = mas_question[I][7];
                    hScrollBar1.Value = Convert.ToInt32(label3.Text);
                    if (mas_question[I][8].Equals("1"))
                    {
                        label3.BackColor = Color.Lime;
                    }

                    else
                    {
                        label3.BackColor = Color.Red;
                        groupBox1.Size = new Size(groupBox1.Width, groupBox1.Height + 20);
                        Label label5 = new Label();
                        label5.Text = ""+mas_question[I][6];
                        label5.AutoSize = true;
                        label3.Margin = new Padding(4, 0, 4, 0);
                        label5.Location = new Point(465, 100);
                        label5.BackColor = Color.Lime;
                        groupBox1.Controls.Add(label5);
                    }
                }
                else if (mas_question[I][1].Equals("ListBox"))
                {
                    listBox1.Items.Clear();
                    if (mas_question[I][8].Equals("0"))
                        groupBox2.Text = "Вы ответили не правильно!";
                    else
                        groupBox2.Text = "Вы ответили правильно!";
                    string[] answer_number = mas_question[I][7].Split(';');
                    mas_answers = mas_question[I][6].Split(';');
                    listBox1.Items.AddRange(mas_answers);
                    //groupBox2.Enabled = false;
                    listBox1.BackColor = Color.Lime;
                    for (int i = 0; i < mas_answers.Length; i++)
                    {
                        textBoxes[i].BackColor = Color.Red;
                        textBoxes[i].Text = answer_number[i];
                        button[i].Enabled = false;
                        if (answer_number[i].Equals(mas_answers[i]))
                        {
                            textBoxes[i].BackColor = Color.Lime;
                        }
                    }
                }
            }
        }
        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString(mas_answers[e.Index], e.Font, Brushes.Black, e.Bounds);
            Brush br = new SolidBrush(Color.Red);
            if (e.Index == Convert.ToInt32(mas_question[I][7]))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
                e.Graphics.DrawString(mas_answers[e.Index], e.Font, Brushes.Black, e.Bounds);
            }   
            br = new SolidBrush(Color.Lime);
            if (mas_answers[e.Index].Equals(mas_question[I][6]))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
                e.Graphics.DrawString(mas_answers[e.Index], e.Font, Brushes.Black, e.Bounds);
            }
        }
        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                string name = listBox1.SelectedItem.ToString();
                textBoxes[Convert.ToInt32((sender as Button).Name)].Text = name;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                (sender as Button).Enabled = false;
            }
            catch {
            }
        }
        private void button_off_Click(object sender, EventArgs e)
        {
            Func(sender,e);
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label3.Text = string.Format("{0}", hScrollBar1.Value);
        }
        private void Fun_button_Colour(object sender, EventArgs e)
        {
            if (mas_question[I][8].Equals(""))
            {
                buttons[I].BackColor = Color.White;
            }
            else if (mas_question[I][8].Equals("0"))
            {
                buttons[I].BackColor = Color.Red;
            }
            else if (mas_question[I][8].Equals("1"))
            {
                buttons[I].BackColor = Color.Lime;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            kalkulator Kalkulator = new kalkulator();
            Kalkulator.ShowDialog();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            synth.SpeakAsyncCancelAll();
        }
    }
}
