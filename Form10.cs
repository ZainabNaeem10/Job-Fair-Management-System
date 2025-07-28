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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form10_Load(object sender, EventArgs e)
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
            textBox6.PasswordChar = '*'; // Assuming textBox6 is Password

            // Emoji Labels for Icons
            label2.Text = "👤 First Name"; // First Name
            label2.Font = new Font("Segoe UI Emoji", 12);
            label2.Location = new Point(133, 88);

            label7.Text = "👤 Middle Name"; // Middle Name
            label7.Font = new Font("Segoe UI Emoji", 12);
            label7.Location = new Point(133, 138);

            label8.Text = "👤 Last Name"; // Last Name
            label8.Font = new Font("Segoe UI Emoji", 12);
            label8.Location = new Point(133, 190);

            label3.Text = "📧 Email"; // Email
            label3.Font = new Font("Segoe UI Emoji", 12);
            label3.Location = new Point(133, 248);

            label4.Text = "📞 Phone"; // Phone
            label4.Font = new Font("Segoe UI Emoji", 12);
            label4.Location = new Point(133, 298);

            label5.Text = "🔒 Password"; // Password
            label5.Font = new Font("Segoe UI Emoji", 12);
            label5.Location = new Point(133, 352);

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
                string midName = textBox2.Text;
                string lastName = textBox3.Text;
                string email = textBox4.Text;
                string password = textBox6.Text;
                string phone = textBox5.Text;

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
                cmdInsertUser.Parameters.AddWithValue("@Role", "TPO"); // assuming role is BoothCoordinator
                cmdInsertUser.Parameters.AddWithValue("@IsActive", 1);

                int userInsert = cmdInsertUser.ExecuteNonQuery();

                // only insert into Booth_Coordinator if user inserted successfully
                if (userInsert > 0)
                {
                    // get max BoothCoordinatorId and increment
                    string getMaxBCIdQuery = "SELECT ISNULL(MAX(TpoId), 0) + 1 FROM TPO";
                    SqlCommand cmdMaxBCId = new SqlCommand(getMaxBCIdQuery, conn);
                    int newTpoId = Convert.ToInt32(cmdMaxBCId.ExecuteScalar());


                    // check if user or booth coordinator id already exists
                    string checkExistQuery = "SELECT COUNT(*) FROM TPO WHERE UserId = @UserId OR TpoId = @TpoId";
                    SqlCommand cmdCheck = new SqlCommand(checkExistQuery, conn);
                    cmdCheck.Parameters.AddWithValue("@UserId", newUserId);
                    cmdCheck.Parameters.AddWithValue("@TpoId", newTpoId);

                    int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (exists > 0)
                    {
                        MessageBox.Show("This user or TPO already exists in the TPO table.");
                        return;
                    }



                    // insert into Booth_Coordinator with NULL ShiftTimings
                    string insertBCQuery = @"
            INSERT INTO TPO (TpoId,Office_location, UserId)
            VALUES (@TpoId,@Office_location, @UserId)";
                    SqlCommand cmdInsertBC = new SqlCommand(insertBCQuery, conn);
                    cmdInsertBC.Parameters.AddWithValue("@TpoId", newTpoId);
                    cmdInsertBC.Parameters.AddWithValue("@Office_location", DBNull.Value); // nu
                    cmdInsertBC.Parameters.AddWithValue("@UserId", newUserId);
                 

                    int bcInsert = cmdInsertBC.ExecuteNonQuery();

                    if (bcInsert > 0)
                    {
                        MessageBox.Show("User and TPO inserted successfully!");

                        this.Hide();
                        Form8 form8 = new Form8(); // Create instance of the new form
                        form8.Show();
                    }
                    else
                    {
                        MessageBox.Show("User inserted, but TPO insert failed.");
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
            Form1 form1 = new Form1(); // Create instance of the new form
            form1.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
