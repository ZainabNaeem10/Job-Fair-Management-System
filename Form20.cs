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
    public partial class Form20 : Form
    {
        public Form20()
        {
            InitializeComponent();
        }

        private void Form20_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'jobfairDataSet14.PeakParticipationHours' table. You can move, or remove it, as needed.
            this.peakParticipationHoursTableAdapter1.Fill(this.jobfairDataSet14.PeakParticipationHours);
            // TODO: This line of code loads data into the 'jobfairDataSet13.PeakParticipationHours' table. You can move, or remove it, as needed.
            this.peakParticipationHoursTableAdapter.Fill(this.jobfairDataSet13.PeakParticipationHours);

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
