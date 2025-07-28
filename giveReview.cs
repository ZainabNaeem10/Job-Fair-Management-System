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
//using Microsoft.Data.SqlClient;


namespace TPO
{
    public partial class giveReview : Form
    { // Connection string to your database
        //private string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
        private string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
        public giveReview()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void LoadRecruiters()
        {
            /* string query = "SELECT Recruiterid FROM Reviews"; // Adjust this query based on your actual Recruiter table
             SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
             DataTable dt = new DataTable();
             da.Fill(dt);

             comboBox1.ValueMember = "Recruiterid"; // RecruiterId as the value
             comboBox1.DisplayMember = "Recruiterid"; // Display RecruiterName in the ComboBox
             comboBox1.DataSource = dt;*/

            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("3");
            comboBox1.Items.Add("4");
            comboBox1.Items.Add("5");
            comboBox1.Items.Add("6");
            comboBox1.Items.Add("7");
            comboBox1.Items.Add("8");
            comboBox1.Items.Add("9");
            comboBox1.Items.Add("10");
            comboBox1.Items.Add("11");
            comboBox1.Items.Add("12");
            comboBox1.Items.Add("13");
            comboBox1.Items.Add("14");
            comboBox1.Items.Add("15");
            comboBox1.Items.Add("16");
            comboBox1.Items.Add("17");
            comboBox1.Items.Add("18");
            comboBox1.Items.Add("19");
            comboBox1.Items.Add("20");

        }

        // Method to load Ratings (1-5) into ComboBox
        private void LoadRatings()
        {
            comboBox2.Items.Add("1");
            comboBox2.Items.Add("2");
            comboBox2.Items.Add("3");
            comboBox2.Items.Add("4");
            comboBox2.Items.Add("5");
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void giveReview_Load(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.CornflowerBlue;
            LoadRecruiters();
            LoadRatings();
        }

        private int GetNextReviewId()
        {
            string query = "SELECT ISNULL(MAX(ReviewID), 0) + 1 FROM Reviews";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    return (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error getting next ReviewID: {ex.Message}");
                    return 1; // Fallback value
                }
            }
        }

        private void SubmitInterviewReview(string studentId, int rating, string comments, int recruiterId)
        {
            if (string.IsNullOrWhiteSpace(studentId) || recruiterId <= 0 || rating < 1 || rating > 5)
            {
                MessageBox.Show("Invalid review data.");
                return;
            }

            if (HasAlreadyReviewed(studentId, recruiterId))
            {
                MessageBox.Show("You have already submitted a review for this recruiter.");
                return;
            }

            if (!HasInterviewScheduled(studentId))
            {
                MessageBox.Show("You can only submit a review if your application status is 'Interview Scheduled'.");
                return;
            }

            int nextReviewId = GetNextReviewId();

            string query = @"
        INSERT INTO Reviews (ReviewID, Comments, Ratings, Userid, Recruiterid)
        VALUES (@ReviewID, @Comments, @Ratings, @Userid, @Recruiterid);";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ReviewID", nextReviewId);
                cmd.Parameters.AddWithValue("@Comments", comments);
                cmd.Parameters.AddWithValue("@Ratings", rating);
                cmd.Parameters.AddWithValue("@Userid", studentId);
                cmd.Parameters.AddWithValue("@Recruiterid", recruiterId);

                try
                {
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    MessageBox.Show(result > 0 ? "Review submitted successfully!" : "Failed to submit review.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private bool HasInterviewScheduled(string studentId)
        {
            string query = "SELECT COUNT(*) FROM Applications WHERE Userid = @StudentId AND status = 'Interview Scheduled'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


        /* private bool IsInterviewCompleted(string studentId)
         {
             string query = @"
     SELECT COUNT(*) 
     FROM Interviews I
     JOIN Applications A ON I.ApplicationID = A.ApplicationID
     JOIN Recruiter R ON A.RecruiterID = R.RecruiterID
     WHERE A.Userid = @StudentId 

     AND A.status = 'Interview Scheduled'";  // Application must be in correct status

             using (SqlConnection conn = new SqlConnection(connectionString))
             {
                 SqlCommand cmd = new SqlCommand(query, conn);
                 cmd.Parameters.AddWithValue("@StudentId", studentId);

                 try
                 {
                     conn.Open();
                     int count = (int)cmd.ExecuteScalar();
                     if (count > 0)
                     {
                         return true;
                     }
                     else
                     {
                         // Additional check to give more specific feedback
                         string statusQuery = @"
                 SELECT A.status
                 FROM Applications A
                 LEFT JOIN Interviews I ON I.ApplicationID = A.ApplicationID
                 WHERE A.Userid = @StudentId";

                         using (SqlCommand statusCmd = new SqlCommand(statusQuery, conn))
                         {
                             statusCmd.Parameters.AddWithValue("@StudentId", studentId);
                             using (SqlDataReader reader = statusCmd.ExecuteReader())
                             {
                                 if (reader.HasRows)
                                 {
                                     while (reader.Read())
                                     {
                                         string appStatus = reader["status"].ToString();
                                         //string interviewStatus = reader["Status"]?.ToString() ?? "No Interview";

                                         if (appStatus != "Interview Scheduled")
                                         {
                                             MessageBox.Show("Your application must be in 'Interview Scheduled' status, you need to provide interview first");
                                         }
                                         
                                     }
                                 }
                                 else
                                 {
                                     MessageBox.Show("No interview records found for your account");
                                 }
                             }
                         }
                         return false;
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show($"Error checking interview status: {ex.Message}");
                     return false;
                 }
             }
         }
        */

       



        // Check if the student has already submitted a review
        private bool HasAlreadyReviewed(string studentId, int recruiterId)
        {
            if (string.IsNullOrEmpty(studentId) || recruiterId <= 0)
            {
                // If we can't verify, assume they have reviewed to prevent duplicates
                return true;
            }

            string query = @"SELECT COUNT(*) FROM Reviews 
                   WHERE Userid = @StudentId 
                   AND Recruiterid = @Recruiterid
                   AND Comments IS NOT NULL"; // Only count reviews with actual comments

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@Recruiterid", recruiterId);

                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking for existing review: {ex.Message}");
                    return true; // Be conservative - assume review exists if we can't check
                }
            }
        }



        // Method to get the logged-in student's ID from the Student table
        // Retrieve the logged-in student's ID
        private string GetLoggedInStudentId(string email, string password)
        {
            string studentId = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Fetch UserId
                    string getUserIdQuery = "SELECT UserId FROM [User] WHERE Email = @Email AND Password = @Password AND IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(getUserIdQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        object userIdResult = cmd.ExecuteScalar();
                        if (userIdResult == null) return null; // User not found

                        // Fetch StudentId
                        string userId = userIdResult.ToString();
                        string getStudentIdQuery = "SELECT StudentId FROM Student WHERE UserId = @UserId";
                        using (SqlCommand cmd2 = new SqlCommand(getStudentIdQuery, conn))
                        {
                            cmd2.Parameters.AddWithValue("@UserId", userId);
                            object studentResult = cmd2.ExecuteScalar();
                            if (studentResult != null) studentId = studentResult.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving student ID: {ex.Message}");
                }
            }

            return studentId;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Hide the current form and show the dashboard
            this.Hide();
            StudentDashboard dashboard = new StudentDashboard();
            dashboard.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Input validation (unchanged)
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select both a recruiter and a rating");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter your review comments");
                return;
            }

            // Get values (unchanged)
            int recruiterId;
            int rating;
            try
            {
                recruiterId = Convert.ToInt32(comboBox1.SelectedItem);
                rating = Convert.ToInt32(comboBox2.SelectedItem);
            }
            catch
            {
                MessageBox.Show("Invalid recruiter or rating selection");
                return;
            }

            string comments = textBox1.Text.Trim();
            string studentId = GetLoggedInStudentId(Session.Email, Session.Password);

            if (string.IsNullOrEmpty(studentId))
            {
                MessageBox.Show("Unable to verify your student account");
                return;
            }

            // 1. Check for existing review
            if (HasAlreadyReviewed(studentId, recruiterId))
            {
                MessageBox.Show("You have already submitted a review for this recruiter!");
                return;
            }

            // 2. Strict interview status check
            string currentStatus = GetApplicationStatus(studentId);

            if (currentStatus != "Interview Scheduled")
            {
                MessageBox.Show($"Cannot submit review. Your application status is: {currentStatus ?? "No application found"}\n" +
                              "You must have 'Interview Scheduled' status to submit a review.");
                return;
            }
            // 3. If all checks pass, submit the review
            SubmitInterviewReview(studentId, rating, comments, recruiterId);
            MessageBox.Show("Review submitted successfully!");

        }


        // New method to get exact application status
        private string GetApplicationStatus(string studentId)
        {
            string query = "SELECT status FROM Applications WHERE Userid = @StudentId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result?.ToString(); // Returns null if no application found
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking application status: {ex.Message}");
                    return null;
                }
            }
        }


        }
}
