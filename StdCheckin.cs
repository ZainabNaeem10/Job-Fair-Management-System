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
    public partial class StdCheckin : Form
    {
        public StdCheckin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //back
            BoothCoordinatorDashboard dashboard = new BoothCoordinatorDashboard();
            dashboard.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //verify
            /*string fastId = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(fastId))
            {
                MessageBox.Show("Please enter FAST ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // connection string - update according to your database
            string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobFare2;Integrated Security=True;Trust Server Certificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Name FROM Student WHERE FAST_ID = @StudentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", fastId);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string studentName = reader["Name"].ToString();

                            // display the student name
                            label3.Text = "Student Name: " + studentName;

                            // log and display check-in time
                            DateTime checkInTime = DateTime.Now;
                            label4.Text = "Check-in Time: " + checkInTime.ToString("yyyy-MM-dd hh:mm:ss tt");
                        }
                        else
                        {
                            MessageBox.Show("Invalid FAST ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/



            //string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            string studentId = textBox1.Text.Trim();
            if (studentId.Length < 8) return; // basic length check

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // query to check student and get degree program and userid
                string studentQuery = "SELECT DegreeProgram, UserId FROM Student WHERE StudentId = @StudentId";
                using (SqlCommand cmd = new SqlCommand(studentQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string degreeProgram = reader["DegreeProgram"].ToString();
                            int userId = Convert.ToInt32(reader["UserId"]);

                            label6.Text = "✔ Valid Student ID";
                            label6.ForeColor = Color.Green;
                            label7.Text = degreeProgram;

                            reader.Close();

                            // now get booth check-in time using UserId
                            string boothQuery = "SELECT TOP 1 check_intime FROM Booth WHERE UserId = @UserId ORDER BY check_intime DESC";
                            using (SqlCommand boothCmd = new SqlCommand(boothQuery, conn))
                            {
                                boothCmd.Parameters.AddWithValue("@UserId", userId);
                                object result = boothCmd.ExecuteScalar();
                                if (result != null)
                                {
                                    label8.Text = Convert.ToDateTime(result).ToString("g"); // "g" = short date + time
                                }
                                else
                                {
                                    label8.Text = "Not checked in";
                                }
                            }
                        }
                        else
                        {
                            label6.Text = "❌ Invalid Student ID";
                            label6.ForeColor = Color.Red;
                            label7.Text = "";
                            label8.Text = "";
                        }
                    }
                }
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {
            //fetched std name
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //fetched check-in time
        }

        private void StdCheckin_Load(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.CornflowerBlue;
            groupBox2.BackColor = Color.CornflowerBlue;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }
    }
}
