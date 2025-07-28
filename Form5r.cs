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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TPO
{
    public partial class Form5r : Form
    {
        public Form5r()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            string selectedUserId = Convert.ToString(comboBox1.SelectedValue);


            string connStr = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

            string query = "SELECT a.ApplicationID, j.Title, f.Title AS FairTitle  FROM Applications as a join Jobpostings as j on a.JobID=j.JobID join Interviews as i on a.ApplicationID=i.ApplicationID join JobFairEvents as f on i.EventId=f.EventID WHERE a.UserId = @UserId";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", selectedUserId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    label10.Text = reader["ApplicationID"].ToString();
                    label11.Text = reader["Title"].ToString();
                    label12.Text = reader["FairTitle"].ToString();
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
                reader.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8r form8 = new Form8r(); // Create instance of the new form
            form8.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
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
            if (checkBox1.Checked)
            {
                status = "Scheduled";
            }
            DateTime startTime = dateTimePicker1.Value;
            DateTime endTime = dateTimePicker2.Value;

            if (startTime.Date != endTime.Date)
            {
                MessageBox.Show("Start and End times must be on the same date.");
                return;
            }

            // Check if end time is after start time
            if (endTime <= startTime)
            {
                MessageBox.Show("End time must be after start time.");
                return;
            }
            string connStr = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            string query = "UPDATE Interviews SET start_time = @StartTime, end_time = @EndTime, Status = @Status WHERE ApplicationID = @ApplicationID";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ApplicationID", applicationId);
                cmd.Parameters.AddWithValue("@StartTime", startTime);
                cmd.Parameters.AddWithValue("@EndTime", endTime);
                cmd.Parameters.AddWithValue("@Status", status);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show(rows > 0 ? "Slot allocated." : "Slot allocation failed.");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy"; // includes both date and time
            dateTimePicker1.ShowUpDown = true;

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM/dd/yyyy"; // includes both date and time
            dateTimePicker2.ShowUpDown = true;

        }
    }
}
