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
    public partial class ViewUpdateProfile : Form
    {
        public ViewUpdateProfile()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ViewUpdateProfile_Load(object sender, EventArgs e)
        {
            groupBox1.BackColor = Color.CornflowerBlue;
            groupBox2.BackColor = Color.CornflowerBlue;
            groupBox3.BackColor = Color.CornflowerBlue;

            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // back to dashboard
            this.Hide();
            StudentDashboard dashboard = new StudentDashboard();
            dashboard.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string connString = "Data Source=DESKTOP-M9OMVKI\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True;";
            string connString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                // get values from textboxes
                string firstName = textBox1.Text;
                string midName = textBox10.Text;
                string lastName = textBox4.Text;
                string email = textBox3.Text;
                string password = textBox2.Text;
                string phone = textBox5.Text;
                string degreeProgram = textBox8.Text;
                string semester = textBox6.Text;
                string gpa = textBox7.Text;

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


                string degree = textBox8.Text;
                string sem = textBox6.Text;
                string cgpa = textBox7.Text;


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
                cmdInsertStudent.Parameters.AddWithValue("@DegreeProgram", degreeProgram);
                cmdInsertStudent.Parameters.AddWithValue("@Semester", semester);
                cmdInsertStudent.Parameters.AddWithValue("@GPA", gpa);
                cmdInsertStudent.Parameters.AddWithValue("@UserId", newUserId);

                int studentInsert = cmdInsertStudent.ExecuteNonQuery();


                string skills = textBox11.Text;
                string certificates = textBox9.Text.Trim();

                //insert into student skills
                string insertSkillsQuery = @"
                INSERT INTO StudentSkills (Userid, skills)
                VALUES (@Userid, @Skills)";
                SqlCommand cmdInsertSkills = new SqlCommand(insertSkillsQuery, conn);
                cmdInsertSkills.Parameters.AddWithValue("@Userid", newStudentId);
                cmdInsertSkills.Parameters.AddWithValue("@Skills", skills);
               
                int skillInsert = cmdInsertSkills.ExecuteNonQuery();

             
                // insert into student certificates
                string insertCertificatesQuery = @"
                INSERT INTO Student_Certificates (Userid, CertificateName)
                VALUES (@Userid, @Certificate)";
                SqlCommand cmdInsertCertificate = new SqlCommand(insertCertificatesQuery, conn);
                cmdInsertCertificate.Parameters.AddWithValue("@Userid", newStudentId);
                cmdInsertCertificate.Parameters.AddWithValue("@Certificate", certificates);
                int certificateInsert = cmdInsertCertificate.ExecuteNonQuery();

                if (userInsert > 0 && studentInsert > 0 && skillInsert > 0 && certificateInsert > 0)
                {
                    MessageBox.Show("User, Student, Skills, and Certificates inserted successfully!");
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


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}
