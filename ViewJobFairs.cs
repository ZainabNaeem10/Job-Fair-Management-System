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
    public partial class ViewJobFairs : Form
    {
        public ViewJobFairs()
        {
            InitializeComponent();
        }

        private void ViewJobFairs_Load(object sender, EventArgs e)
        {
            // Call the method to load job fairs
            LoadJobFairs();
        }

        // Method to load job fairs from the database into the DataGridView
        // Method to load job fairs from the database into the DataGridView
        private void LoadJobFairs()
        {
            //string connString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
            string connString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT EventId, Title, StartDate, EndDate, Venue, BoothSlots FROM JobFairEvents";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;

                        // optional: customize headers
                        dataGridView1.Columns["EventId"].HeaderText = "Event ID";
                        dataGridView1.Columns["Title"].HeaderText = "Event Title";
                        dataGridView1.Columns["StartDate"].HeaderText = "Start Date";
                        dataGridView1.Columns["EndDate"].HeaderText = "End Date";
                        dataGridView1.Columns["Venue"].HeaderText = "Venue";
                        dataGridView1.Columns["BoothSlots"].HeaderText = "Booth Slots";
                    }
                    else
                    {
                        MessageBox.Show("No job fair events found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading job fairs: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
           
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Hide the current form and show the dashboard
            this.Hide();
            StudentDashboard dashboard = new StudentDashboard();
            dashboard.Show();

        }
    }
}


