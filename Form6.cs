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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedReport = comboBox1.SelectedItem.ToString();

            switch (selectedReport)
            {
                // Student Participation Reports
                case "📊 Department-wise Registration Counts":
                    Form16 form16 = new Form16(); // Create instance of the new form
                    form16.Show();
                    break;

                case "📊 GPA Distribution Across Applicants":
                    Form17 form17 = new Form17(); // Create instance of the new form
                    form17.Show();
                    break;

                case "📊 Top Skills (Frequency of Skills)":
                    Form18 form18 = new Form18(); // Create instance of the new form
                    form18.Show();
                    break;

                // Recruiter Activity Reports
                case "🧑‍💼 Total Interviews Conducted per Company":
                    Form7 form7 = new Form7(); // Create instance of the new form
                    form7.Show();
                    break;

                case "🧑‍💼 Offer-to-Application Acceptance Ratios":
                    Form11 form11 = new Form11(); // Create instance of the new form
                    form11.Show();
                    break;

                case "🧑‍💼 Student Ratings for Recruiters":
                    Form12 form12 = new Form12(); // Create instance of the new form
                    form12.Show();
                    break;

                // Placement Statistics
                case "🎯 Overall Placement Percentage":
                    Form13 form13 = new Form13(); // Create instance of the new form
                    form13.Show();
                    break;

                case "🎯 Placement Rates per Department (CS, SE, AI, DS)":
                    Form14 form14 = new Form14(); // Create instance of the new form
                    form14.Show();
                    break;

                case "🎯 Average Salary Offered by Degree Program":
                    Form15 form15 = new Form15(); // Create instance of the new form
                    form15.Show();
                    break;

                // Event Performance Report
                case "🏟️ Booth Traffic Stats":
                    Form19 form19 = new Form19(); // Create instance of the new form
                    form19.Show();
                    break;

                case "🏟️ Peak Participation Hours":
                    Form20 form20 = new Form20(); // Create instance of the new form
                    form20.Show();
                    break;

                case "🏟️ Resource Usage Metrics":
                    Form21 form21 = new Form21(); // Create instance of the new form
                    form21.Show();
                    break;

                default:
                    MessageBox.Show("Report not implemented yet.");
                    break;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8(); // Create instance of the new form
            form8.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
           
            comboBox1.Items.Add("📊 Department-wise Registration Counts");
            comboBox1.Items.Add("📊 GPA Distribution Across Applicants");
            comboBox1.Items.Add("📊 Top Skills (Frequency of Skills)");

            comboBox1.Items.Add("🧑‍💼 Total Interviews Conducted per Company");
            comboBox1.Items.Add("🧑‍💼 Offer-to-Application Acceptance Ratios");
            comboBox1.Items.Add("🧑‍💼 Student Ratings for Recruiters");

            comboBox1.Items.Add("🎯 Overall Placement Percentage");
            comboBox1.Items.Add("🎯 Placement Rates per Department (CS, SE, AI, DS)");
            comboBox1.Items.Add("🎯 Average Salary Offered by Degree Program");

            comboBox1.Items.Add("🏟️ Booth Traffic Stats");
            comboBox1.Items.Add("🏟️ Peak Participation Hours");
            comboBox1.Items.Add("🏟️ Resource Usage Metrics");

            comboBox1.SelectedIndex = 0; // Optional: default selection
        }

    }
}
