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
    public partial class kalkulator : Form
    {
        public kalkulator()
        {
            InitializeComponent();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text += (sender as Button).Text;
        }

        double a = 0, b = 0, c = 0;
        char znak = '+';

        private void button1_Click(object sender, EventArgs e)
        {
            a = Convert.ToDouble(textBox1.Text);
            textBox1.Clear();
            znak = (sender as Button).Text[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                b = Convert.ToDouble(textBox1.Text);
                switch (znak)
                {
                    case '+':
                        c = a + b;
                        break;
                    case '-':
                        c = a - b;
                        break;
                    case 'x':
                        c = a * b;
                        break;
                    case '/':
                        c = a / b;
                        break;
                }
                textBox1.Text = c.ToString();
            }

            catch
            {

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                if (textBox1.Text[0] == '-')
                    textBox1.Text = textBox1.Text.Remove(0, 1);
                else textBox1.Text = '-' + textBox1.Text;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (textBox1.Text !="")
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
        }
    }
}
