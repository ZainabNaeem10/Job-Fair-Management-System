using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPO
{
    public partial class SearchCompanies : Form
    {
       
        public SearchCompanies()
        {
            InitializeComponent();
        }

        private void SearchCompanies_Load(object sender, EventArgs e)
        {
            groupBox1.BackColor= Color.CornflowerBlue;
            // create your connection string here
            //string connString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
            string connString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);

            // clear existing columns if any
            dataGridView1.Columns.Clear();

           

            // fill dropdowns with items
            comboBox1.Items.AddRange(new string[] { "Full-time","Internship","Remote" }); // Job Type
            comboBox2.Items.AddRange(new string[] { "Lahore", "Karachi", "Islamabad" }); // City
            comboBox3.Items.AddRange(new string[] { "80,000", "120,000", "15,000" }); // Salary Range
            comboBox4.Items.AddRange(new string[] { "C++","Java", "C#", "Python" }); // Skill (optional)

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
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

        /*private void button2_Click(object sender, EventArgs e)
        {
            // this is your Search button
            string jobType = comboBox1.SelectedItem?.ToString();
            string location = comboBox2.SelectedItem?.ToString();
            string salaryRange = comboBox3.SelectedItem?.ToString();
            string skill = comboBox4.SelectedItem?.ToString();

            string query = "SELECT * FROM Companies WHERE 1=1"; // start basic

            if (!string.IsNullOrEmpty(jobType))
            {
                query += " AND Sector = '" + jobType + "'";
            }

            if (!string.IsNullOrEmpty(location))
            {
                query += " AND City = '" + location + "'";
            }

            if (!string.IsNullOrEmpty(skill))
            {
                query += " AND Name LIKE '%" + skill + "%'";
            }

            if (!string.IsNullOrEmpty(salaryRange))
            {
                // you can adjust according to your database schema
                if (salaryRange == "10k–20k")
                {
                    query += " AND Salary BETWEEN 10000 AND 20000";
                }
                else if (salaryRange == "20k–50k")
                {
                    query += " AND Salary BETWEEN 20000 AND 50000";
                }
                else if (salaryRange == "50k–100k")
                {
                    query += " AND Salary BETWEEN 50000 AND 100000";
                }
            }

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while searching: " + ex.Message);
            }
        }*/




        private void button2_Click(object sender, EventArgs e)
        {
            // create your connection string here
            //string connString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
            string connString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);

            // Get selected values from ComboBoxes
            string jobType = comboBox1.SelectedItem?.ToString();
            string location = comboBox2.SelectedItem?.ToString();
            string salaryRange = comboBox3.SelectedItem?.ToString();
            string skill = comboBox4.SelectedItem?.ToString();

            // Start building the query with inner joins
            string query = @"
        SELECT 
            C.Name, 
            J.Title, 
            J.Salary, 
            J.City, 
            JT.Type, 
            JR.RequiredSkills,
            J.Description 
            
        FROM Jobpostings J
        JOIN Companies C ON J.CompanyID = C.CompanyID
        JOIN Jobpostings_Type JT ON J.JobID = JT.Job_id
        JOIN Jobpostings_RequiredSkills JR ON J.JobID = JR.Job_id
        WHERE 1=1"; // Base condition to always return true

            // Add filters based on selected values
            if (!string.IsNullOrEmpty(jobType))
            {
                query += " AND JT.Type = @JobType";
            }

            if (!string.IsNullOrEmpty(location))
            {
                query += " AND J.City = @Location";
            }

            if (!string.IsNullOrEmpty(salaryRange))
            {
                // Apply salary range filter
                if (salaryRange == "10k–20k")
                {
                    query += " AND CAST(J.Salary AS INT) BETWEEN 10000 AND 20000";
                }
                else if (salaryRange == "20k–50k")
                {
                    query += " AND CAST(J.Salary AS INT) BETWEEN 20000 AND 50000";
                }
                else if (salaryRange == "50k–100k")
                {
                    query += " AND CAST(J.Salary AS INT) BETWEEN 50000 AND 100000";
                }
            }

            if (!string.IsNullOrEmpty(skill))
            {
                // Match skills using LIKE
                query += " AND JR.RequiredSkills LIKE '%' + @Skill + '%'";
            }

            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                // Add parameters to avoid SQL injection
                cmd.Parameters.AddWithValue("@JobType", jobType ?? "");
                cmd.Parameters.AddWithValue("@Location", location ?? "");
                cmd.Parameters.AddWithValue("@Skill", skill ?? "");

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                // Bind the result to the DataGridView
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No results found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while searching: " + ex.Message);
            }
        }




        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
