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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;




namespace TPO
{
    public partial class Registration_booth : Form
    {
        public Registration_booth()
        {
            InitializeComponent();
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
          //  SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobFare2;Integrated Security=True;"); //Connection String
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True"); //Connection String
            conn.Open();
            MessageBox.Show("Connection Open");
            SqlCommand cm;
            string firstn = textBox1.Text;
            string lastn = textBox2.Text;
            string em = textBox3.Text;
            string pass = textBox4.Text;
            // string middlen = textBox5.Text;
            //string phone = textBox6.Text;
            // string query = "Insert into registration([First Name],[Last Name],Email,Password) values ('" + firstn + "','" + middlen + "','" + lastn + "','" + phone + "','" + em + "','" + pass + "')";
            string query = "Insert into registration([First Name],[Last Name],Email,Password) values ('" + firstn + "','" + lastn + "','" + em + "','" + pass + "')";
            cm = new SqlCommand(query, conn);
            cm.ExecuteNonQuery();
            cm.Dispose();
            conn.Close();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            //groupBox1.BackColor = Color.CornflowerBlue;
            label1.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.TextAlign = ContentAlignment.MiddleCenter;

            button1.Text = "Register";
            button1.BackColor = Color.RoyalBlue;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            textBox4.PasswordChar = '*';


            label13.Text = "👤";  // user silhouette emoji for role
            label13.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label13.Location = new Point(72, 20);  // adjust position as needed

            label14.Text = "👤";  // user silhouette emoji for role
            label14.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label14.Location = new Point(72, 47);  // adjust position as needed

            label15.Text = "👤";  // user silhouette emoji for role
            label15.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label15.Location = new Point(72, 73);  // adjust position as needed


            label6.Text = "📧";
            label6.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label6.Location = new Point(72, 112);  // Adjust this to move the label horizontally/vertically

            label10.Text = "📱"; // Adds phone emoji
            label10.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label10.Location = new Point(72, 139);

            // Position the Email TextBox

            label7.Text = "🔒";
            label7.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label7.Location = new Point(72, 169);



            /*label11.Text = "👤";  // user silhouette emoji for role
             label11.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
             label11.Location = new Point(77, 245);  // adjust position as needed

             */

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Login_booth f = new Login_booth();//form 2 is login
            f.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //string connString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
            string connString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                // get values from textboxes
                string firstName = textBox1.Text;
                string midName = textBox5.Text;
                string lastName = textBox2.Text;
                string email = textBox3.Text;
                string password = textBox4.Text;
                string phone = textBox6.Text;

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
                cmdInsertUser.Parameters.AddWithValue("@Role", "BoothCoordinator"); // assuming role is BoothCoordinator
                cmdInsertUser.Parameters.AddWithValue("@IsActive", 1);

                int userInsert = cmdInsertUser.ExecuteNonQuery();

                // only insert into Booth_Coordinator if user inserted successfully
                if (userInsert > 0)
                {
                    // get max BoothCoordinatorId and increment
                    string getMaxBCIdQuery = "SELECT ISNULL(MAX(BoothCoordinatorId), 0) + 1 FROM Booth_Coordinator";
                    SqlCommand cmdMaxBCId = new SqlCommand(getMaxBCIdQuery, conn);
                    int newBoothCoordinatorId = Convert.ToInt32(cmdMaxBCId.ExecuteScalar());


                    // check if user or booth coordinator id already exists
                    string checkExistQuery = "SELECT COUNT(*) FROM Booth_Coordinator WHERE UserId = @UserId OR BoothCoordinatorId = @BoothCoordinatorId";
                    SqlCommand cmdCheck = new SqlCommand(checkExistQuery, conn);
                    cmdCheck.Parameters.AddWithValue("@UserId", newUserId);
                    cmdCheck.Parameters.AddWithValue("@BoothCoordinatorId", newBoothCoordinatorId);

                    int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (exists > 0)
                    {
                        MessageBox.Show("This user or booth coordinator already exists in the Booth_Coordinator table.");
                        return;
                    }



                    // insert into Booth_Coordinator with NULL ShiftTimings
                    string insertBCQuery = @"
            INSERT INTO Booth_Coordinator (BoothCoordinatorId, UserId, ShiftTimings)
            VALUES (@BoothCoordinatorId, @UserId, @ShiftTimings)";
                    SqlCommand cmdInsertBC = new SqlCommand(insertBCQuery, conn);
                    cmdInsertBC.Parameters.AddWithValue("@BoothCoordinatorId", newBoothCoordinatorId);
                    cmdInsertBC.Parameters.AddWithValue("@UserId", newUserId);
                    cmdInsertBC.Parameters.AddWithValue("@ShiftTimings", DBNull.Value); // null for now

                    int bcInsert = cmdInsertBC.ExecuteNonQuery();

                    if (bcInsert > 0)
                    {
                        MessageBox.Show("User and Booth Coordinator inserted successfully!");
                        BoothCoordinatorDashboard dashboard = new BoothCoordinatorDashboard();
                        dashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("User inserted, but Booth Coordinator insert failed.");
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
