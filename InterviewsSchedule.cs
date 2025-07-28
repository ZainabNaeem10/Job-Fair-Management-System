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
    public partial class InterviewsSchedule : Form
    {
        int studentId;
        public InterviewsSchedule()
        {
            InitializeComponent();
            this.studentId = studentId;
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Hide the current form and show the dashboard
            this.Hide();
            StudentDashboard dashboard = new StudentDashboard();
            dashboard.Show();
        }

        private void InterviewsSchedule_Load_1(object sender, EventArgs e)
        {
            // Clear existing columns if any
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = false;


            try
            {
               // string connString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
                string connString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // Step 1: Get UserId using session email/password
                    string getUserIdQuery = "SELECT UserId FROM [User] WHERE Email = @Email AND Password = @Password AND IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(getUserIdQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", Session.Email);
                        cmd.Parameters.AddWithValue("@Password", Session.Password);

                        object result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("User not found.");
                            return;
                        }

                        string userId = result.ToString();

                        // Step 2: Get StudentId using UserId
                        string getStudentIdQuery = "SELECT StudentId FROM Student WHERE UserId = @UserId";
                        using (SqlCommand cmd2 = new SqlCommand(getStudentIdQuery, conn))
                        {
                            cmd2.Parameters.AddWithValue("@UserId", userId);
                            object studentResult = cmd2.ExecuteScalar();

                            if (studentResult == null)
                            {
                                MessageBox.Show("Student ID not found.");
                                return;
                            }

                            string studentId = studentResult.ToString();

                            // Step 3: Fetch interview schedule using studentId
                            string query = @"
                        SELECT 
                            I.InterviewID,
                            I.start_time AS InterviewStartTime,
                            I.end_time AS InterviewEndTime,
                            I.Status AS InterviewStatus,
                            I.Result AS InterviewResult,
                            A.ApplicationID,
                            J.Title AS JobTitle,
                            J.Salary AS JobSalary,
                            J.City AS JobLocation,
                            E.Title AS EventTitle,
                            E.StartDate AS EventStartDate,
                            E.EndDate AS EventEndDate,
                            E.Venue AS EventVenue
                        FROM Interviews I
                        JOIN Applications A ON A.ApplicationID = I.ApplicationID
                        JOIN Jobpostings J ON A.JobID = J.JobID
                        JOIN JobFairEvents E ON J.EventID = E.EventId
                        JOIN Student S ON A.Userid = S.StudentId
                        WHERE S.StudentId = @StudentId
                        ORDER BY I.start_time;";

                            using (SqlCommand cmd3 = new SqlCommand(query, conn))
                            {
                                cmd3.Parameters.AddWithValue("@StudentId", studentId);

                                SqlDataAdapter adapter = new SqlDataAdapter(cmd3);
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);

                                dataGridView1.DataSource = dt;

                                if (dt.Rows.Count == 0)
                                {
                                    MessageBox.Show("No interview schedule found.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading interview schedule: " + ex.Message);
            }
        }

    }
}
