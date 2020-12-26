using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ЛАБОРАТОРНЫЕ_РАБОТЫ__ЯП_
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Код кнопки Выход
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 vxod = new Form2();
            vxod.Show();
            Hide();
        }
    }
}
