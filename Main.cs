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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration formreg = new Registration(); // Create instance of the new form
            formreg.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1r form1 = new Form1r(); // Create instance of the new form
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 form10 = new Form10(); // Create instance of the new form
            form10.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration_booth formregb = new Registration_booth(); // Create instance of the new form
            formregb.Show();
        }
    }
}
