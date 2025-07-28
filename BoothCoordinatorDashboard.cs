using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPO
{
    public partial class BoothCoordinatorDashboard : Form
    {
        // Connection string to your database
       // private string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobFare2;Integrated Security=True;";
        private string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
        private int boothCoordinatorId; // Store logged-in Booth Coordinator ID



        // Constructor that takes the logged-in booth coordinator ID
        public BoothCoordinatorDashboard(int boothCoordinatorId)
        {
            InitializeComponent();
            this.boothCoordinatorId = boothCoordinatorId; // Initialize boothCoordinatorId
          //  ApplyButtonStyles();  // apply UI/UX when form initializes
           LoadBoothCoordinatorDetails();
        }


        // Method to apply styles to the buttons
   

        public BoothCoordinatorDashboard()
        {
            InitializeComponent();
            
    }
        
        private void label2_Click(object sender, EventArgs e)
        {
            //welcome text
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.CornflowerBlue;
            groupBox2.BackColor = Color.CornflowerBlue;
            // Fetch and display the Booth Coordinator details on load
            // LoadBoothCoordinatorDetails();
            label1.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.TextAlign = ContentAlignment.MiddleCenter;

            // Loop through all buttons and change their color to purple
            foreach (Button btn in this.Controls.OfType<Button>())
            {
               
                if (btn != null && btn != button4 && btn!=button3)
                {
                    // Apply purple color to buttons
                    btn.BackColor = Color.Purple;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.Violet; // Lighter purple on hover
                    btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;

                    // Add symbols (emojis) to the buttons based on their name
                    if (btn.Name == "Student Check-In")  // Customize button with name "Student Check-In"
                    {
                        btn.Text = "📋 " + btn.Text;  // Clipboard emoji for "Student Check-In"
                    }
                    else if (btn.Name == "Traffic")  // Customize button with name "Traffic"
                    {
                        btn.Text = "🚦 " + btn.Text;  // Traffic light emoji for "Traffic"
                    }
                    else
                    {
                        btn.Text = "🔵 " + btn.Text;  // Default symbol (circle) for other buttons
                    }
                }
            }

          







        }


        // Method to style individual buttons



        // Method to load the Booth Coordinator details (Booth Coordinator ID and Shift Timings)
        private void LoadBoothCoordinatorDetails()
        {

            try

            {
                //string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
                string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();



                    // Step 1: Find UserId from Email and Password
                    string userQuery = "SELECT UserId FROM [User] WHERE Email = @Email AND Password = @Password AND IsActive = 1";
                    SqlCommand userCmd = new SqlCommand(userQuery, connection);
                    userCmd.Parameters.AddWithValue("@Email", Session.Email.Trim());
                    userCmd.Parameters.AddWithValue("@Password", Session.Password.Trim());

                    object userIdResult = userCmd.ExecuteScalar();

                    if (userIdResult != null)
                    {
                        
                        int userId = Convert.ToInt32(userIdResult);

                        // Step 2: Get BoothCoordinatorId and ShiftTimings from Booth_Coordinator
                        string boothQuery = "SELECT BoothCoordinatorId, ShiftTimings FROM Booth_Coordinator WHERE UserId = @UserId";
                        SqlCommand boothCmd = new SqlCommand(boothQuery, connection);
                        boothCmd.Parameters.AddWithValue("@UserId", userId);

                        SqlDataReader reader = boothCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int boothCoordinatorId = Convert.ToInt32(reader["BoothCoordinatorId"]);
                            string shiftTimings = reader["ShiftTimings"].ToString();

                            // Display in labels
                            label4.Text = "  " + boothCoordinatorId.ToString();
                            label5.Text = " " + shiftTimings;
                        }
                        else
                        {
                            MessageBox.Show("Booth Coordinator record not found.");
                        }
                        reader.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid user credentials.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //timings text
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //stdcheckin

            StdCheckin dashboard = new StdCheckin();
            dashboard.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //booth traffic monitoring
            BoothTrafficMonitoring dashboard = new BoothTrafficMonitoring();
            dashboard.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightSkyBlue;
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.DodgerBlue;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try

            {
                //string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
                string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                   

                    // Step 1: Find UserId from Email and Password
                    string userQuery = "SELECT UserId FROM [User] WHERE Email = @Email AND Password = @Password AND IsActive = 1";
                    SqlCommand userCmd = new SqlCommand(userQuery, connection);
                    userCmd.Parameters.AddWithValue("@Email", Session.Email.Trim());
                    userCmd.Parameters.AddWithValue("@Password", Session.Password.Trim());

                    object userIdResult = userCmd.ExecuteScalar();

                    if (userIdResult != null )
                    {
                        // Debugging: Check email and password values
                        MessageBox.Show($"Email: {Session.Email}, Password: {Session.Password}");
                        int userId = Convert.ToInt32(userIdResult);

                        // Step 2: Get BoothCoordinatorId and ShiftTimings from Booth_Coordinator
                        string boothQuery = "SELECT BoothCoordinatorId, ShiftTimings FROM Booth_Coordinator WHERE UserId = @UserId";
                        SqlCommand boothCmd = new SqlCommand(boothQuery, connection);
                        boothCmd.Parameters.AddWithValue("@UserId", userId);

                        SqlDataReader reader = boothCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int boothCoordinatorId = Convert.ToInt32(reader["BoothCoordinatorId"]);
                            string shiftTimings = reader["ShiftTimings"].ToString();

                            // Display in labels
                            label4.Text = "  "+boothCoordinatorId.ToString();
                            label5.Text = " " + shiftTimings;
                        }
                        else
                        {
                            MessageBox.Show("Booth Coordinator record not found.");
                        }
                        reader.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid user credentials.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //stdcheckin

            StdCheckin dashboard = new StdCheckin();
            dashboard.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //booth traffic monitoring
            BoothTrafficMonitoring dashboard = new BoothTrafficMonitoring();
            dashboard.Show();
            this.Hide();

        }
    }
}
