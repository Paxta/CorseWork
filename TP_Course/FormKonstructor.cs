using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ЛАБОРАТОРНЫЕ_РАБОТЫ__ЯП_
{
    public partial class FormKonstructor : Form
    {
        public FormKonstructor()
        {
            InitializeComponent();
        }

        static int amount, curQues;

        public void changeGP1()
        {
            if (amount == 0)
                groupBox2.Enabled = false;
            else
                groupBox2.Enabled = true;
        }

        public struct Questions
        {
            public int typeOfQuestion;
            public string question;
            public bool[] ansChecked;
            public int quanOfAnswers;
            public string[] answers;
        };
        public static Questions[] quest;

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
                e.Handled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            checkBox3.Visible = true;
            checkBox4.Visible = true;
            checkBox5.Visible = true;
            checkBox6.Visible = true;
            richTextBox1.Visible = true;
            richTextBox2.Visible = true;
            richTextBox3.Visible = true;
            richTextBox4.Visible = true;
            richTextBox5.Visible = true;
            richTextBox6.Visible = true;
            richTextBox7.Visible = true;
            richTextBox2.ReadOnly = true;
            richTextBox3.ReadOnly = true;
            richTextBox4.ReadOnly = true;
            richTextBox5.ReadOnly = true;
            richTextBox6.ReadOnly = true;
            richTextBox7.ReadOnly = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            checkBox3.Visible = true;
            checkBox4.Visible = true;
            checkBox5.Visible = true;
            checkBox6.Visible = true;
            richTextBox1.Visible = true;
            richTextBox2.Visible = true;
            richTextBox3.Visible = true;
            richTextBox4.Visible = true;
            richTextBox5.Visible = true;
            richTextBox6.Visible = true;
            richTextBox7.Visible = true;
            richTextBox2.ReadOnly = true;
            richTextBox3.ReadOnly = true;
            richTextBox4.ReadOnly = true;
            richTextBox5.ReadOnly = true;
            richTextBox6.ReadOnly = true;
            richTextBox7.ReadOnly = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            checkBox6.Visible = false;
            richTextBox1.Visible = false;
            richTextBox2.Visible = false;
            richTextBox3.Visible = false;
            richTextBox4.Visible = false;
            richTextBox5.Visible = false;
            richTextBox6.Visible = false;
            richTextBox7.Visible = false;
            richTextBox2.ReadOnly = false;
            richTextBox3.ReadOnly = false;
            richTextBox4.ReadOnly = false;
            richTextBox5.ReadOnly = false;
            richTextBox6.ReadOnly = false;
            richTextBox7.ReadOnly = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            checkBox6.Visible = false;
            richTextBox1.Visible = false;
            richTextBox2.Visible = false;
            richTextBox3.Visible = false;
            richTextBox4.Visible = false;
            richTextBox5.Visible = false;
            richTextBox6.Visible = false;
            richTextBox7.Visible = false;
            richTextBox2.ReadOnly = false;
            richTextBox3.ReadOnly = false;
            richTextBox4.ReadOnly = false;
            richTextBox5.ReadOnly = false;
            richTextBox6.ReadOnly = false;
            richTextBox7.ReadOnly = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox2.ReadOnly = !richTextBox2.ReadOnly;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox3.ReadOnly = !richTextBox3.ReadOnly;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox4.ReadOnly = !richTextBox4.ReadOnly;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox5.ReadOnly = !richTextBox5.ReadOnly;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox6.ReadOnly = !richTextBox6.ReadOnly;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            richTextBox7.ReadOnly = !richTextBox7.ReadOnly;
        }

        private void FormKonstructor_Load(object sender, EventArgs e)
        {
            amount = 0;
            changeGP1();
            curQues = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { amount = Convert.ToInt32(textBox1.Text); } catch { return; }
            changeGP1();
            if (amount == 0)
                return;
            quest = new Questions[amount];
            groupBox1.Text = "Вопрос " + (curQues + 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (curQues < amount - 1)
            {
                curQues++;
                groupBox1.Text = "Вопрос " + (curQues + 1);
            }
            else return;
            int type = 0, quant = 0;
            //Выбор типа вопроса
            if (radioButton1.Checked) type = 1;
            if (radioButton2.Checked) type = 2;
            if (radioButton3.Checked) type = 3;
            if (radioButton4.Checked) type = 4;
            //Заполение структуры текущего вопроса
            quest[curQues - 1].typeOfQuestion = type;
            quest[curQues - 1].question = richTextBox1.Text;
            quest[curQues - 1].ansChecked = new bool[6];
            //Подсчет количества ответов
            if (checkBox1.Checked) { quant++; quest[curQues - 1].ansChecked[0] = true; }
            if (checkBox2.Checked) { quant++; quest[curQues - 1].ansChecked[1] = true; }
            if (checkBox3.Checked) { quant++; quest[curQues - 1].ansChecked[2] = true; }
            if (checkBox4.Checked) { quant++; quest[curQues - 1].ansChecked[3] = true; }
            if (checkBox5.Checked) { quant++; quest[curQues - 1].ansChecked[4] = true; }
            if (checkBox6.Checked) { quant++; quest[curQues - 1].ansChecked[5] = true; }
            quest[curQues - 1].quanOfAnswers = quant;
            quest[curQues - 1].answers = new string[6];
            quest[curQues - 1].answers[0] = richTextBox2.Text;
            quest[curQues - 1].answers[1] = richTextBox3.Text;
            quest[curQues - 1].answers[2] = richTextBox4.Text;
            quest[curQues - 1].answers[3] = richTextBox5.Text;
            quest[curQues - 1].answers[4] = richTextBox6.Text;
            quest[curQues - 1].answers[5] = richTextBox7.Text;

            //Очистка следующего вопроса
            richTextBox1.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            richTextBox5.Text = "";
            richTextBox6.Text = "";
            richTextBox7.Text = "";
            //Вывод структуры следующего вопроса
            try
            {
                if (quest[curQues].typeOfQuestion == 1) radioButton1.Checked = true;
                if (quest[curQues].typeOfQuestion == 2) radioButton2.Checked = true;
                if (quest[curQues].typeOfQuestion == 3) radioButton3.Checked = true;
                if (quest[curQues].typeOfQuestion == 4) radioButton4.Checked = true;
                richTextBox1.Text = quest[curQues].question;
                checkBox1.Checked = quest[curQues].ansChecked[0];
                checkBox2.Checked = quest[curQues].ansChecked[1];
                checkBox3.Checked = quest[curQues].ansChecked[2];
                checkBox4.Checked = quest[curQues].ansChecked[3];
                checkBox5.Checked = quest[curQues].ansChecked[4];
                checkBox6.Checked = quest[curQues].ansChecked[5];
                richTextBox2.Text = quest[curQues].answers[0];
                richTextBox3.Text = quest[curQues].answers[1];
                richTextBox4.Text = quest[curQues].answers[2];
                richTextBox5.Text = quest[curQues].answers[3];
                richTextBox6.Text = quest[curQues].answers[4];
                richTextBox7.Text = quest[curQues].answers[5];
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (curQues > 0)
            {
                curQues--;
                groupBox1.Text = "Вопрос " + (curQues + 1);
            }
            else return;
            int type = 0, quant = 0;
            //Выбор типа вопроса
            if (radioButton1.Checked) type = 1;
            if (radioButton2.Checked) type = 2;
            if (radioButton3.Checked) type = 3;
            if (radioButton4.Checked) type = 4;
            //Заполение структуры текущего вопроса
            quest[curQues + 1].typeOfQuestion = type;
            quest[curQues + 1].question = richTextBox1.Text;
            quest[curQues + 1].ansChecked = new bool[6];
            //Подсчет количества ответов
            if (checkBox1.Checked) { quant++; quest[curQues + 1].ansChecked[0] = true; }
            if (checkBox2.Checked) { quant++; quest[curQues + 1].ansChecked[1] = true; }
            if (checkBox3.Checked) { quant++; quest[curQues + 1].ansChecked[2] = true; }
            if (checkBox4.Checked) { quant++; quest[curQues + 1].ansChecked[3] = true; }
            if (checkBox5.Checked) { quant++; quest[curQues + 1].ansChecked[4] = true; }
            if (checkBox6.Checked) { quant++; quest[curQues + 1].ansChecked[5] = true; }
            quest[curQues + 1].quanOfAnswers = quant;
            quest[curQues + 1].answers = new string[6];
            quest[curQues + 1].answers[0] = richTextBox2.Text;
            quest[curQues + 1].answers[1] = richTextBox3.Text;
            quest[curQues + 1].answers[2] = richTextBox4.Text;
            quest[curQues + 1].answers[3] = richTextBox5.Text;
            quest[curQues + 1].answers[4] = richTextBox6.Text;
            quest[curQues + 1].answers[5] = richTextBox7.Text;

            //Очистка прудыдущего вопроса
            richTextBox1.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            richTextBox5.Text = "";
            richTextBox6.Text = "";
            richTextBox7.Text = "";
            //Вывод структуры предыдущего вопроса
            try
            {
                if (quest[curQues].typeOfQuestion == 1) radioButton1.Checked = true;
                if (quest[curQues].typeOfQuestion == 2) radioButton2.Checked = true;
                if (quest[curQues].typeOfQuestion == 3) radioButton3.Checked = true;
                if (quest[curQues].typeOfQuestion == 4) radioButton4.Checked = true;
                richTextBox1.Text = quest[curQues].question;
                checkBox1.Checked = quest[curQues].ansChecked[0];
                checkBox2.Checked = quest[curQues].ansChecked[1];
                checkBox3.Checked = quest[curQues].ansChecked[2];
                checkBox4.Checked = quest[curQues].ansChecked[3];
                checkBox5.Checked = quest[curQues].ansChecked[4];
                checkBox6.Checked = quest[curQues].ansChecked[5];
                richTextBox2.Text = quest[curQues].answers[0];
                richTextBox3.Text = quest[curQues].answers[1];
                richTextBox4.Text = quest[curQues].answers[2];
                richTextBox5.Text = quest[curQues].answers[3];
                richTextBox6.Text = quest[curQues].answers[4];
                richTextBox7.Text = quest[curQues].answers[5];
            }
            catch { }
        }
    }
}
