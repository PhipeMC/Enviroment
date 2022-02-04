using App.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.UI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Angulo test = new Angulo(new Point(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox1.Text)),
                    new Point(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox5.Text)),
                    new Point(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox6.Text)));
                MessageBox.Show(test.calculateAngle().ToString());
            }
            catch (Exception) {
                MessageBox.Show("Sin datos");
            }
        }
    }
}
