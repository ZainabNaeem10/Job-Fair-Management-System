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
    public partial class BoothTrafficMonitoring : Form
    {
        // Connection string to the database
        //private string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
        private string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

        // Dictionary to store booth ID and its visit count
        private Dictionary<int, int> boothVisitorCounts = new Dictionary<int, int>();

        public BoothTrafficMonitoring()
        {
            InitializeComponent();
        }

        private void BoothTrafficMonitoring_Load(object sender, EventArgs e)
        {
            LoadBoothData(); // Load booth data when the form is loaded
           // groupBox1.BackColor = Color.CornflowerBlue;
        }

        private void LoadBoothData()
        {
            /*
            string query = "SELECT BoothID FROM Booth"; // Adjust this query based on your actual Recruiter table
            SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);

            comboBox1.ValueMember = "BoothID"; // RecruiterId as the value
            comboBox1.DisplayMember = "BoothID"; // Display RecruiterName in the ComboBox
            comboBox1.DataSource = dt;
            */
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to fetch BoothID, CompanyID, Location, and VisitorCount from Booth
                    string query = "SELECT BoothID, CompanyID, Location, VisitorCount FROM Booth";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable boothData = new DataTable();
                    adapter.Fill(boothData);

                    // Populate ComboBox with Booth IDs
                    comboBox1.ValueMember = "BoothID";
                    comboBox1.DisplayMember = "BoothID";
                    comboBox1.DataSource = boothData;

                    // Populate DataGridView with Booth information
                    dataGridView1.DataSource = boothData;

                    // Set column headers
                    dataGridView1.Columns["BoothID"].HeaderText = "Booth ID";
                    dataGridView1.Columns["CompanyID"].HeaderText = "Company ID";
                    dataGridView1.Columns["Location"].HeaderText = "Location";
                    dataGridView1.Columns["VisitorCount"].HeaderText = "Visitor Count";

                    // Update boothVisitorCounts dictionary with current visitor counts from database
                    boothVisitorCounts.Clear(); // Clear any existing data in the dictionary
                    foreach (DataRow row in boothData.Rows)
                    {
                        int boothId = Convert.ToInt32(row["BoothID"]);
                        int visitorCount = row["VisitorCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["VisitorCount"]); // Check for DBNull
                        boothVisitorCounts[boothId] = visitorCount; // Add current visitor count to the dictionary
                    }

                    // Dynamically update visitor count in the DataGridView
                    dataGridView1.CellFormatting += DataGridViewBooths_CellFormatting;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading booth data: " + ex.Message);
            }

        }

        private void DataGridViewBooths_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["VisitorCount"].Index)
            {
                int boothId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["BoothID"].Value);
                e.Value = boothVisitorCounts.ContainsKey(boothId) ? boothVisitorCounts[boothId] : 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedBoothId = Convert.ToInt32(comboBox1.SelectedValue);
            UpdateVisitorCount(selectedBoothId); // Update visitor count for selected booth
        }

        private void UpdateVisitorCount(int boothId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if booth exists before updating
                    string checkQuery = "SELECT COUNT(*) FROM Booth WHERE BoothID = @BoothID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                    checkCmd.Parameters.AddWithValue("@BoothID", boothId);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Booth not found in database.");
                        return;
                    }

                    // Ensure VisitorCount is not NULL before updating it. If it is NULL, set it to 0 first.
                    string updateQuery = @"
                UPDATE Booth 
                SET VisitorCount = 
                    CASE 
                        WHEN VisitorCount IS NULL THEN 1 
                        ELSE VisitorCount + 1 
                    END
                WHERE BoothID = @BoothID";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@BoothID", boothId);
                    updateCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Visit recorded successfully!");

                // Reload the DataGridView to show updated count from database
                LoadBoothData();  // Reload the booth data after the update
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating visitor count: " + ex.Message);
            }
        }





        private void button2_Click(object sender, EventArgs e)
        {
            BoothCoordinatorDashboard dashboard = new BoothCoordinatorDashboard();
            dashboard.Show();
            this.Hide(); // Navigate back to the dashboard
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
