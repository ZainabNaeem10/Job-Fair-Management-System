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
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
        }

        private void Form15_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'jobfairDataSet8.AverageSalaryByDegree' table. You can move, or remove it, as needed.
            this.averageSalaryByDegreeTableAdapter.Fill(this.jobfairDataSet8.AverageSalaryByDegree);

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
