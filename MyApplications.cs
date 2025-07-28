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
    public partial class MyApplications : Form
    {
        private string studentId;
        public MyApplications()
        {
            InitializeComponent();
            this.studentId = ""; // Initialize studentId to an empty string

        }


        private void MyApplications_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            try
            {
               // string connString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
                string connString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // Step 1: Validate the user login using email and password
                    string getUserIdQuery = "SELECT UserId FROM [User] WHERE Email = @Email AND Password = @Password AND IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(getUserIdQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", Session.Email);  // Use email from login session
                        cmd.Parameters.AddWithValue("@Password", Session.Password);  // Use password from login session

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string userId = result.ToString();

                            // Step 2: Retrieve StudentId from the Student table using UserId
                            string getStudentIdQuery = "SELECT StudentId FROM Student WHERE UserId = @UserId";
                            using (SqlCommand cmd2 = new SqlCommand(getStudentIdQuery, conn))
                            {
                                cmd2.Parameters.AddWithValue("@UserId", userId);

                                object studentResult = cmd2.ExecuteScalar();

                                if (studentResult != null)
                                {
                                    studentId = studentResult.ToString();

                                    // Step 3: Now fetch the applications using the StudentId
                                    LoadApplications(conn);
                                }
                                else
                                {
                                    MessageBox.Show("Student ID not found.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading applications: " + ex.Message);
            }
        }

        private void LoadApplications(SqlConnection conn)
        {
            try
            {
                string query = @"
                    SELECT 
                        A.ApplicationID,
                        A.Status,
                        A.[Date Applied],
                        J.Title AS JobTitle,
                        J.Salary,
                        J.City
                    FROM Applications A
                    JOIN Jobpostings J ON A.JobID = J.JobID
                    WHERE A.UserId = @StudentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId); // Use the retrieved StudentId

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No applications found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading applications: " + ex.Message);
            }
        }


    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         try
            {
                //string connString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
                string connString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    // Get UserId from Email and Password
                    string getUserIdQuery = "SELECT UserId FROM [User] WHERE Email = @Email AND Password = @Password AND IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(getUserIdQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", Session.Email);
                        cmd.Parameters.AddWithValue("@Password", Session.Password);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string userId = result.ToString();

                            // Now load applications using UserId
                            LoadApplications(conn);
                        }
                        else
                        {
                            MessageBox.Show("User not found or not active.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user info: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Hide the current form and show the dashboard
            this.Hide();
            StudentDashboard dashboard = new StudentDashboard();
            dashboard.Show();
        }
    }
    }
