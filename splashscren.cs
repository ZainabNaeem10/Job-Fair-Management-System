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
    public partial class splashscren : Form
    {
        public splashscren()
        {
            InitializeComponent();
            timer1 = new Timer();
            timer1.Interval = 4000; // 4 seconds
            timer1.Tick += Timer_Tick;
            timer1.Start();

            // Customize ProgressBar position
            progressBar1.Location = new Point(50, this.Height - 50); // X=50, Y=bottom
            progressBar1.Size = new Size(this.Width - 100, 20); // stretch width

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer1.Stop();


            // back to dashboard
            this.Hide();
            Form1 dashboard = new Form1();//put registration from here
            dashboard.Show();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
