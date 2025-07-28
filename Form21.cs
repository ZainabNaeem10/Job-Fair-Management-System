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
    public partial class Form21 : Form
    {
        public Form21()
        {
            InitializeComponent();
        }

        private void Form21_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'jobfairDataSet16.CoordinatorTimeUsage' table. You can move, or remove it, as needed.
            this.coordinatorTimeUsageTableAdapter1.Fill(this.jobfairDataSet16.CoordinatorTimeUsage);
            // TODO: This line of code loads data into the 'jobfairDataSet15.CoordinatorTimeUsage' table. You can move, or remove it, as needed.
            this.coordinatorTimeUsageTableAdapter.Fill(this.jobfairDataSet15.CoordinatorTimeUsage);

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
