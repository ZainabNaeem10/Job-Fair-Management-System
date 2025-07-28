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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'jobfairDataSet4.RecruiterRating' table. You can move, or remove it, as needed.
            this.recruiterRatingTableAdapter1.Fill(this.jobfairDataSet4.RecruiterRating);
            // TODO: This line of code loads data into the 'jobfairDataSet3.RecruiterRating' table. You can move, or remove it, as needed.
            this.recruiterRatingTableAdapter.Fill(this.jobfairDataSet3.RecruiterRating);

            this.reportViewer1.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 form6 = new Form6(); // Create instance of the new form
            form6.Show();
        }
    }
}
