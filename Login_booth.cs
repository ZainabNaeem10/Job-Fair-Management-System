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
    public partial class Login_booth : Form
    {
        public Login_booth()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.CornflowerBlue;

            label1.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.TextAlign = ContentAlignment.MiddleCenter;

            button1.Text = "Login";
            button1.BackColor = Color.RoyalBlue;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            textBox2.PasswordChar = '*';

            label6.Text = "👤";  // Username symbol
            label6.Font = new Font("Segoe UI Emoji", 14);  // Emoji font
            label6.BackColor = Color.Transparent;  // Transparent background        
            label6.Location = new Point(74, 38);  // Position the label for username symbol

            label7.Text = "🔒";
            label7.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label7.Location = new Point(74, 90);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* string un = textBox1.Text;
           string pass = textBox2.Text;

           //// string hashedPassword = HashPassword(pass); // hash input to compare

           string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobFare2;Integrated Security=True;";

           string loginQuery = "SELECT COUNT(*) FROM registration WHERE Email = @UserName AND Password = @Password";

           using (SqlConnection conn = new SqlConnection(connectionString))
           using (SqlCommand cmd = new SqlCommand(loginQuery, conn))
           {
               cmd.Parameters.AddWithValue("@UserName", un);
               cmd.Parameters.AddWithValue("@Password", pass);

               conn.Open();
               int count = (int)cmd.ExecuteScalar();

               if (count > 0)
               {
                   MessageBox.Show("Login successful!");
                   // open student dashboard
                   StudentDashboard dashboard = new StudentDashboard();
                   dashboard.Show();
                   this.Hide();
               }
               else
               {
                   MessageBox.Show("Invalid username or password.");
               }
           }*/




            Session.Email = textBox1.Text.Trim();//
            Session.Password = textBox2.Text;//


            string un = textBox1.Text;
            string pass = textBox2.Text;

            //// string hashedPassword = HashPassword(pass); // hash input to compare

            //string connectionString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

            string loginQuery = "SELECT COUNT(*) FROM [USER] WHERE Email = @UserName AND Password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(loginQuery, conn))
            {
                cmd.Parameters.AddWithValue("@UserName", un);
                cmd.Parameters.AddWithValue("@Password", pass);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Login successful!");
                    // open student dashboard
                    BoothCoordinatorDashboard dashboard = new BoothCoordinatorDashboard();
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }


          /*  BoothCoordinatorDashboard dashboard = new BoothCoordinatorDashboard();
            dashboard.Show();
            this.Hide();*/
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration_booth dashboard = new Registration_booth();
            dashboard.Show();
            this.Hide();
        }
    }
}
