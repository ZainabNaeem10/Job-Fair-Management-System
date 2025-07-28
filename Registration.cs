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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //registration title
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //first name
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //last anme
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //email
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //password
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
            label1.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.TextAlign = ContentAlignment.MiddleCenter;

           // groupBox1.BackColor = Color.CornflowerBlue;
        

        button1.Text = "Register";
            button1.BackColor = Color.RoyalBlue;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            textBox4.PasswordChar = '*';


            label13.Text = "👤";  // user silhouette emoji for role
            label13.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label13.Location = new Point(77, 20);  // adjust position as needed

            label14.Text = "👤";  // user silhouette emoji for role
            label14.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label14.Location = new Point(77, 47);  // adjust position as needed

            label15.Text = "👤";  // user silhouette emoji for role
            label15.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label15.Location = new Point(77, 73);  // adjust position as needed


            label6.Text = "📧";
            label6.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label6.Location = new Point(77, 112);  // Adjust this to move the label horizontally/vertically

            label10.Text = "📱"; // Adds phone emoji
            label10.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label10.Location = new Point(77, 139);

            // Position the Email TextBox

            label7.Text = "🔒";
            label7.Font = new Font("Segoe UI Emoji", 14, FontStyle.Regular);
            label7.Location = new Point(77, 169);

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           // Login f = new Login();//form 2 is login
           // f.Show();
          //  this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
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

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
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

                    // insert into User
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
                    cmdInsertUser.Parameters.AddWithValue("@Role", "Student"); // or whatever role
                    cmdInsertUser.Parameters.AddWithValue("@IsActive", 1);     // assuming new user is active

                    int userInsert = cmdInsertUser.ExecuteNonQuery();


                  


                    // get max StudentId and generate new formatted ID
                    string getMaxStudentIdQuery = "SELECT MAX(CAST(SUBSTRING(StudentId, 5, LEN(StudentId)) AS INT)) FROM Student WHERE StudentId LIKE '23i-%'";
                    SqlCommand cmdMaxStudentId = new SqlCommand(getMaxStudentIdQuery, conn);
                    object result = cmdMaxStudentId.ExecuteScalar();

                    int nextStudentNumber = 1;
                    if (result != DBNull.Value && result != null)
                    {
                        nextStudentNumber = Convert.ToInt32(result) + 1;
                    }

                    string newStudentId = "23i-" + nextStudentNumber.ToString("D4"); // e.g., 23i-3031



                    // insert into Student
                    string insertStudentQuery = @"
            INSERT INTO Student (StudentId, DegreeProgram, CurrentSemester, cgpa, UserId)
            VALUES (@StudentId, @DegreeProgram, @Semester, @GPA, @UserId)";
                    SqlCommand cmdInsertStudent = new SqlCommand(insertStudentQuery, conn);
                    cmdInsertStudent.Parameters.AddWithValue("@StudentId", newStudentId);
                    cmdInsertStudent.Parameters.AddWithValue("@DegreeProgram", DBNull.Value);
                    cmdInsertStudent.Parameters.AddWithValue("@Semester", DBNull.Value);
                    cmdInsertStudent.Parameters.AddWithValue("@GPA", DBNull.Value);
                    cmdInsertStudent.Parameters.AddWithValue("@UserId", newUserId);

                    int studentInsert = cmdInsertStudent.ExecuteNonQuery();


                 
                    //insert into student skills
                    string insertSkillsQuery = @"
                INSERT INTO StudentSkills (Userid, skills)
                VALUES (@Userid, @Skills)";
                    SqlCommand cmdInsertSkills = new SqlCommand(insertSkillsQuery, conn);
                    cmdInsertSkills.Parameters.AddWithValue("@Userid", newStudentId);
                    cmdInsertSkills.Parameters.AddWithValue("@Skills", DBNull.Value);

                    int skillInsert = cmdInsertSkills.ExecuteNonQuery();


                    // insert into student certificates
                    string insertCertificatesQuery = @"
                INSERT INTO Student_Certificates (Userid, CertificateName)
                VALUES (@Userid, @Certificate)";
                    SqlCommand cmdInsertCertificate = new SqlCommand(insertCertificatesQuery, conn);
                    cmdInsertCertificate.Parameters.AddWithValue("@Userid", newStudentId);
                    cmdInsertCertificate.Parameters.AddWithValue("@Certificate", DBNull.Value);
                    int certificateInsert = cmdInsertCertificate.ExecuteNonQuery();

                    if (userInsert > 0 && studentInsert > 0 && skillInsert > 0 && certificateInsert > 0)
                    {
                        MessageBox.Show("User, Student, Skills, and Certificates inserted successfully!");
                    Form2 dashboard = new Form2();
                    dashboard.Show();
                    this.Hide();

                }
                    else
                    {
                        MessageBox.Show("Insert failed.");
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

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 f = new Form2();//form 2 is login
            f.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    
}
