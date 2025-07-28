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
    public partial class AcademicRecord : Form
    {
        public AcademicRecord()
        {
            InitializeComponent();
        }

        private void AcademicRecord_Load(object sender, EventArgs e)
        {
            // clear existing columns if any
            dataGridView1.Columns.Clear();

            // manually add columns (only headers, no data)
            
            dataGridView1.Columns.Add("StudentId", "Student Id");
            dataGridView1.Columns.Add("DegreeProgram", " Degree Program");
            dataGridView1.Columns.Add("CurrentSemester", "Current Semester");
            dataGridView1.Columns.Add("cgpa", "cgpa");
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Hide the current form and show the dashboard
            this.Hide();
            StudentDashboard dashboard = new StudentDashboard();
            dashboard.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
