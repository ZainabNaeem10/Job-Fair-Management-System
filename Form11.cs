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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'jobfairDataSet1.CompanyOfferApplicationRatios' table. You can move, or remove it, as needed.
            this.companyOfferApplicationRatiosTableAdapter.Fill(this.jobfairDataSet1.CompanyOfferApplicationRatios);

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
