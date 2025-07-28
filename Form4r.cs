using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPO
{
    public partial class Form4r : Form
    {
        public Form4r()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string connStr = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            string query = "SELECT s.StudentId, u.FirstName + ' ' + ISNULL(u.MiddleName + ' ', '') + u.LastName AS FullName FROM [User] as u join Student as s on u.UserId=s.UserId";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "FullName";  // What user sees
                comboBox1.ValueMember = "StudentId";      // What you use in code
            }

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8r form8 = new Form8r(); // Create instance of the new form
            form8.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label10.Text = "";
            label8.Text = "";
            label9.Text = "";
            string selectedUserId = Convert.ToString(comboBox1.SelectedValue);


            string connStr = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

            string query = "SELECT a.ApplicationID, j.Title, a.[Date Applied] FROM Applications as a join Jobpostings as j on a.JobID=j.JobID WHERE a.UserId = @UserId";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", selectedUserId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    label10.Text = reader["ApplicationID"].ToString();
                    label8.Text = reader["Title"].ToString();
                    label9.Text = reader["Date Applied"].ToString();
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
                reader.Close();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(label10.Text, out int applicationId))
            {
                MessageBox.Show("Invalid or missing Application ID. Please search for a student first.");
                return;
            }
            string status = "";

            if (checkBox1.Checked && checkBox2.Checked)
            {
                MessageBox.Show("You cannot select both Shortlisted and Rejected.");
                return;
            }
            else if (checkBox1.Checked)
            {
                status = "Shortlisted";
            }
            else if (checkBox2.Checked)
            {
                status = "Rejected";
            }
            else
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            string connStr = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            string query = "UPDATE Applications SET Status = @Status WHERE ApplicationID = @ApplicationID";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@ApplicationID", applicationId);


                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show(rows > 0 ? "Status updated." : "Update failed.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
