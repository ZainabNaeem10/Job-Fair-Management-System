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

namespace TPO
{
    public partial class ApplyForJobs : Form
    {
        public ApplyForJobs()
        {
            InitializeComponent();
        }

      


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Hide the current form and show the dashboard
            this.Hide();
            StudentDashboard dashboard = new StudentDashboard();
            dashboard.Show();
        }



        private void button2_Click_1(object sender, EventArgs e)
        {//apply

        }

        private void ApplyForJobs_Load_1(object sender, EventArgs e)
        {
            // Clear existing columns if any
            dataGridView1.Columns.Clear();


            // Manually add columns
            dataGridView1.Columns.Add("ApplicationID", "Application ID");
            dataGridView1.Columns.Add("Status", "Status");
            dataGridView1.Columns.Add("DateApplied", "Date Applied");
            dataGridView1.Columns.Add("Userid", "User ID");
            dataGridView1.Columns.Add("JobID", "Job ID");

        }
    }
}
