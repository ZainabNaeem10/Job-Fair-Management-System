using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;

namespace TPO
{
    public partial class ManageInterviews : Form
    {
        //private string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
        private string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
        public ManageInterviews()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.CornflowerBlue;
            LoadCompanies();
            LoadJobFairs();
            LoadDates();
            LoadTimeSlots();
          // // LoadInterviewData();

        }
        // Load Companies into the ComboBox for company selection
        private void LoadCompanies()
        {
            string query = "SELECT CompanyID, Name FROM Companies";
            SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "CompanyID";
            comboBox1.DataSource = dt;
        }

        // Load Job Fair Events into the ComboBox for event selection
        private void LoadJobFairs()
        {
            string query = "SELECT EventId, Title FROM JobFairEvents";
            SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox4.DisplayMember = "Title";
            comboBox4.ValueMember = "EventId";
            comboBox4.DataSource = dt;
        }

        // Load Dates based on available interviews or events
        private void LoadDates()
        {
            string query = "SELECT DISTINCT CAST(start_time AS DATE) AS EventDate FROM Interviews";
            SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox3.DisplayMember = "EventDate";
            comboBox3.ValueMember = "EventDate";
            comboBox3.DataSource = dt;
        }

        // Load Time Slots based on available interview times
        private void LoadTimeSlots()
        {
            string query = "SELECT DISTINCT FORMAT(start_time, 'HH:mm') AS TimeSlot FROM Interviews ORDER BY TimeSlot";
            SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.DisplayMember = "TimeSlot";
            comboBox2.ValueMember = "TimeSlot";
            comboBox2.DataSource = dt;
        }


        // Populate the DataGridView with Interview data
        private void LoadInterviewData()
        {
            string query = "SELECT i.InterviewID, j.Title AS JobTitle, i.start_time, i.end_time, i.Status, i.Result " +
                           "FROM Interviews i " +
                           "INNER JOIN Jobpostings j ON i.EventID = j.EventID";
            SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Hide the current form and show the dashboard
            this.Hide();
            StudentDashboard dashboard = new StudentDashboard();
            dashboard.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create connection and command
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            // Base query to get interview details
            string query = @"
        SELECT i.InterviewID, 
               c.Name AS CompanyName, 
               jp.Title AS JobTitle, 
               i.start_time, 
               i.end_time, 
               i.Status, 
               i.Result
        FROM Interviews i
        INNER JOIN Jobpostings jp ON i.EventID = jp.EventID
        INNER JOIN Companies c ON jp.CompanyID = c.CompanyID
        WHERE 1=1"; // Base condition to always return true (for dynamic filtering)

            // Add filtering conditions based on selected values
            if (comboBox1.SelectedItem != null) // Company selected
            {
                query += " AND c.CompanyID = @CompanyID";
                cmd.Parameters.AddWithValue("@CompanyID", comboBox1.SelectedValue);
            }

            if (comboBox4.SelectedItem != null) // Job fair event selected
            {
                query += " AND i.EventID = @EventID";
                cmd.Parameters.AddWithValue("@EventID", comboBox4.SelectedValue);
            }

            if (comboBox3.SelectedItem != null) // Date selected
            {
                query += " AND CAST(i.start_time AS DATE) = @start_time";
                cmd.Parameters.AddWithValue("@start_time", comboBox3.SelectedValue);
            }

            if (comboBox2.SelectedItem != null) // Time slot selected
            {
                string timeSlot = comboBox2.SelectedItem.ToString();
                query += " AND CONVERT(VARCHAR(5), i.start_time, 108) = @TimeSlot"; // Convert time to match format
                cmd.Parameters.AddWithValue("@TimeSlot", comboBox2.SelectedValue);
            }

            // Set the CommandText property with the final query
            cmd.CommandText = query;

            try
            {
                // Execute the query
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the result to the DataGridView
                dataGridView1.DataSource = dt;

                // Check if no rows found
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No interviews found based on selected filters.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while filtering interviews: " + ex.Message);
            }
        }



        /*  private void ScheduleInterview()
          {
              if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 ||
                  comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1)
              {
                  MessageBox.Show("Please select all required fields before scheduling.");
                  return;
              }

              try
              {
                  int companyID = Convert.ToInt32(comboBox1.SelectedValue);
                  int eventID = Convert.ToInt32(comboBox4.SelectedValue);

                  // get the selected date from comboBox3 as a real DateTime
                  DateTime dateOnly = Convert.ToDateTime(((DataRowView)comboBox3.SelectedItem)["EventDate"]);

                  // parse the selected time string safely
                  DateTime timeOnly = DateTime.ParseExact(comboBox2.SelectedItem.ToString(), "hh:mm tt", CultureInfo.InvariantCulture);

                  // combine date and time into one datetime object
                  DateTime startTime = dateOnly.Date.AddHours(timeOnly.Hour).AddMinutes(timeOnly.Minute);
                  DateTime endTime = startTime.AddHours(1);

                  // insert into DB
                  ScheduleInterviewInDatabase(companyID, eventID, startTime, endTime);
              }
              catch (Exception ex)
              {
                  MessageBox.Show("Invalid input or incomplete data: " + ex.Message);
              }
          }

          // Insert a new interview into the database
          private void ScheduleInterviewInDatabase(int companyID, int eventID, DateTime startTime, DateTime endTime)
          {
              // Insert query without specifying InterviewID, assuming InterviewID is auto-generated
              string query = "INSERT INTO Interviews (start_time, end_time, Status, ApplicationID, Userid, EventID) " +
                             "VALUES (@startTime, @endTime, @status, @applicationID, @userid, @eventID)";

              using (SqlConnection conn = new SqlConnection(connectionString))
              {
                  SqlCommand cmd = new SqlCommand(query, conn);
                  cmd.Parameters.AddWithValue("@startTime", startTime);
                  cmd.Parameters.AddWithValue("@endTime", endTime);
                  cmd.Parameters.AddWithValue("@status", "Scheduled");  // Status as 'Scheduled'
                  cmd.Parameters.AddWithValue("@applicationID", 1);  // Assuming application ID is 1
                  cmd.Parameters.AddWithValue("@userid", 1);  // Assuming recruiter/user ID is 1
                  cmd.Parameters.AddWithValue("@eventID", eventID);

                  try
                  {
                      conn.Open();
                      cmd.ExecuteNonQuery(); // Execute the insert query
                      MessageBox.Show("Interview scheduled successfully.");
                      LoadInterviewData(); // Refresh the grid after scheduling an interview
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show($"Error scheduling interview: {ex.Message}");
                  }
              }
          }
          */


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //company
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //date
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //time
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //event

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
