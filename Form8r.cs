using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPO
{
    public partial class Form8r : Form
    {
        public Form8r()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2r form2 = new Form2r(); // Create instance of the new form
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3r form3 = new Form3r(); // Create instance of the new form
            form3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4r form4 = new Form4r(); // Create instance of the new form
            form4.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5r form5 = new Form5r(); // Create instance of the new form
            form5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6r form6 = new Form6r(); // Create instance of the new form
            form6.Show();
        }

        private void Form8_Load(object sender, EventArgs e)
        {


        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main(); // Create instance of the new form
            main.Show();
        }
    }
}
