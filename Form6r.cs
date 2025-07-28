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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TPO
{
    public partial class Form6r : Form
    {
        public Form6r()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8r form8 = new Form8r(); // Create instance of the new form
            form8.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            string firstName = textBox1.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT  
                u.FirstName + ' ' + ISNULL(u.MiddleName + ' ', '') + u.LastName AS [Student Name], 
                j.Title AS [Job Title], 
                i.Result AS [Interview Result]
            FROM [User] u
            JOIN Student s ON u.UserId = s.UserId 
            JOIN Applications a ON s.StudentId = a.UserId 
            JOIN JobPostings j ON a.JobID = j.JobID 
            JOIN Interviews i ON a.ApplicationID = i.ApplicationID  
            WHERE u.FirstName LIKE @FirstName + '%' AND i.Result = 'Passed'";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@FirstName", firstName);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Clear only rows, not columns
                dataGridView1.Rows.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        dr["Student Name"].ToString(),
                        dr["Job Title"].ToString(),
                        dr["Interview Result"].ToString(),
                        false
                    );
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            // Only add columns if not already added
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "StudentName",
                    HeaderText = "Student Name"
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "JobTitle",
                    HeaderText = "Job Title"
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "InterviewResult",
                    HeaderText = "Interview Result"
                });

                dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "Hire",
                    HeaderText = "Hire"
                });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Skip the new row placeholder
                    if (row.IsNewRow) continue;

                    bool isHired = Convert.ToBoolean(row.Cells["Hire"].Value);

                    if (isHired)
                    {
                        string fullName = row.Cells["StudentName"].Value.ToString().Trim();

                        // Split name to match with FirstName and LastName (you can improve this if needed)
                        string[] nameParts = fullName.Split(' ');
                        string firstName = nameParts[0];
                        string lastName = nameParts.Length > 1 ? nameParts[nameParts.Length - 1] : "";

                        // Update Hire = 1 for matching student
                        string updateQuery = @"
                    UPDATE Student
                    SET Hire = 1
                    WHERE UserId IN (
                        SELECT UserId FROM [User]
                        WHERE FirstName = @FirstName AND LastName = @LastName
                    )";

                        SqlCommand cmd = new SqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Hiring results finalized and saved to database.");
            }
        }
    }
}
