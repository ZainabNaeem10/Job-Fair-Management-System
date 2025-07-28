using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TPO
{
    public partial class Form7r : Form
    {
        public Form7r()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.TextAlign = ContentAlignment.TopLeft;
            label1.Text = "Login";

            // Register Button Styling
            button1.Text = "Login";
            button1.BackColor = Color.RoyalBlue;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            // Cancel Button Styling
            button2.Text = "Cancel";
            button2.BackColor = Color.LightGray;
            button2.ForeColor = Color.Black;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;

            // Secure password input
            textBox2.PasswordChar = '*'; // Assuming textBox6 is Password
            label2.Text = "📧 Email"; // Email
            label2.Font = new Font("Segoe UI Emoji", 12);
            label2.Location = new Point(133, 98);
            label3.Text = "🔒 Password"; // Password
            label3.Font = new Font("Segoe UI Emoji", 12);
            label3.Location = new Point(133, 158);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1r form1 = new Form1r(); // Create instance of the new form
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Session.Email = textBox1.Text.Trim();//
            Session.Password = textBox2.Text;//


            string un = textBox1.Text;
            string pass = textBox2.Text;

            //// string hashedPassword = HashPassword(pass); // hash input to compare

            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";

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
                    this.Hide();
                    Form8r form8 = new Form8r(); // Create instance of the new form
                    form8.Show();

                    // open student dashboard

                }
                else
                {
                    MessageBox.Show("Invalid Email or password.");
                }
            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
