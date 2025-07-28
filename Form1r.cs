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
    public partial class Form1r : Form
    {
        public Form1r()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

 

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            label1.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.TextAlign = ContentAlignment.TopLeft;
            label1.Text = "Account Registration";

            // Register Button Styling
            button1.Text = "Register";
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
            textBox4.PasswordChar = '*'; // Assuming textBox6 is Password

            // Emoji Labels for Icons
            label2.Text = "👤 First Name"; // First Name
            label2.Font = new Font("Segoe UI Emoji", 12);
            label2.Location = new Point(133, 78);

            label7.Text = "👤 Middle Name"; // Middle Name
            label7.Font = new Font("Segoe UI Emoji", 12);
            label7.Location = new Point(133, 118);

            label8.Text = "👤 Last Name"; // Last Name
            label8.Font = new Font("Segoe UI Emoji", 12);
            label8.Location = new Point(133, 158);

            label3.Text = "📧 Email"; // Email
            label3.Font = new Font("Segoe UI Emoji", 12);
            label3.Location = new Point(133, 198);

            label4.Text = "📞 Phone"; // Phone
            label4.Font = new Font("Segoe UI Emoji", 12);
            label4.Location = new Point(133, 238);

            label5.Text = "🔒 Password"; // Password
            label5.Font = new Font("Segoe UI Emoji", 12);
            label5.Location = new Point(133, 285);

            label6.Text = "🏢 Company"; // Company
            label6.Font = new Font("Segoe UI Emoji", 12);
            label6.Location = new Point(133, 330);

            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            string query1 = "SELECT CompanyID,Name FROM Companies";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query1, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "Name"; // What user sees
                comboBox1.ValueMember = "CompanyID";
            }


        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                // get values from textboxes
                string firstName = textBox1.Text;
                string midName = textBox6.Text;
                string lastName = textBox7.Text;
                string email = textBox2.Text;
                string password = textBox4.Text;
                string phone = textBox3.Text;
                int CompanyId = Convert.ToInt32(comboBox1.SelectedValue);
                // get max user id and increment
                string getMaxIdQuery = "SELECT ISNULL(MAX(UserId), 0) + 1 FROM [User]";
                SqlCommand cmdMaxId = new SqlCommand(getMaxIdQuery, conn);
                int newUserId = Convert.ToInt32(cmdMaxId.ExecuteScalar());

                // insert into User table
                string insertUserQuery = @"
        INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive)
        VALUES (@UserId, @FirstName, @MiddleName, @LastName, @Email, @Phone, @Password, @Role, @IsActive)";
                SqlCommand cmdInsertUser = new SqlCommand(insertUserQuery, conn);
                cmdInsertUser.Parameters.AddWithValue("@UserId", newUserId);
                cmdInsertUser.Parameters.AddWithValue("@FirstName", firstName);
                cmdInsertUser.Parameters.AddWithValue("@MiddleName", midName);
                cmdInsertUser.Parameters.AddWithValue("@LastName", lastName);
                cmdInsertUser.Parameters.AddWithValue("@Email", email);
                cmdInsertUser.Parameters.AddWithValue("@Phone", phone);
                cmdInsertUser.Parameters.AddWithValue("@Password", password);
                cmdInsertUser.Parameters.AddWithValue("@Role", "Recruiter"); // assuming role is BoothCoordinator
                cmdInsertUser.Parameters.AddWithValue("@IsActive", 1);

                int userInsert = cmdInsertUser.ExecuteNonQuery();

                // only insert into Booth_Coordinator if user inserted successfully
                if (userInsert > 0)
                {
                    // get max BoothCoordinatorId and increment
                    string getMaxBCIdQuery = "SELECT ISNULL(MAX(RecruiterId), 0) + 1 FROM Recruiter";
                    SqlCommand cmdMaxBCId = new SqlCommand(getMaxBCIdQuery, conn);
                    int newRecruiterId = Convert.ToInt32(cmdMaxBCId.ExecuteScalar());


                    // check if user or booth coordinator id already exists
                    string checkExistQuery = "SELECT COUNT(*) FROM Recruiter WHERE UserId = @UserId OR RecruiterId = @RecruiterId";
                    SqlCommand cmdCheck = new SqlCommand(checkExistQuery, conn);
                    cmdCheck.Parameters.AddWithValue("@UserId", newUserId);
                    cmdCheck.Parameters.AddWithValue("@RecruiterId", newRecruiterId);

                    int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (exists > 0)
                    {
                        MessageBox.Show("This user or Recruiter already exists in the Recruiter table.");
                        return;
                    }



                    // insert into Booth_Coordinator with NULL ShiftTimings
                    string insertBCQuery = @"
            INSERT INTO Recruiter (RecruiterId, UserId,is_approved,CompanyID)
            VALUES (@RecruiterId,@UserId,@is_approved,@CompanyID)";
                    SqlCommand cmdInsertBC = new SqlCommand(insertBCQuery, conn);
                    cmdInsertBC.Parameters.AddWithValue("@RecruiterId", newRecruiterId);
                    cmdInsertBC.Parameters.AddWithValue("@UserId", newUserId);
                    cmdInsertBC.Parameters.AddWithValue("@is_approved", "1");
                    cmdInsertBC.Parameters.AddWithValue("@CompanyID", CompanyId);


                    int bcInsert = cmdInsertBC.ExecuteNonQuery();

                    if (bcInsert > 0)
                    {
                        MessageBox.Show("User and Recruiter inserted successfully!");

                        this.Hide();
                        Form8r form8 = new Form8r(); // Create instance of the new form
                        form8.Show();
                    }
                    else
                    {
                        MessageBox.Show("User inserted, but Recruiter insert failed.");
                    }
                }
                else
                {
                    MessageBox.Show("User insert failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form7r form7 = new Form7r(); // Create instance of the new form
            form7.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
