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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8(); // Create instance of the new form
            form8.Show();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            // Only add columns if not already added
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "UserID",
                    HeaderText = "User ID"
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "UserName",
                    HeaderText = "User Name"
                });

                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Email",
                    HeaderText = "Interview Result"
                });

                dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "Role",
                    HeaderText = "Role"
                });
                dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "IsActive",
                    HeaderText = "IsActive"
                }

               );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            string firstName = textBox1.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT  
                u.UserId as [User ID],
                u.FirstName + ' ' + ISNULL(u.MiddleName + ' ', '') + u.LastName AS [User Name], 
                u.Email AS Email, 
                u.Role AS Role,
                u.IsActive as IsActive
            FROM [User] u
            WHERE u.FirstName LIKE @FirstName + '%' AND u.Role in ('Student','Recruiter')";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@FirstName", firstName);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Clear only rows, not columns
                dataGridView1.Rows.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        dr["User ID"].ToString(),
                        dr["User Name"].ToString(),
                        dr["Email"].ToString(),
                        dr["Role"].ToString(),
                        dr["IsActive"].ToString()
                    );
                }
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
                    string fullName = row.Cells["UserName"].Value.ToString().Trim();

                    // Split name to match with FirstName and LastName (you can improve this if needed)
                    string[] nameParts = fullName.Split(' ');
                    string firstName = nameParts[0];
                    string lastName = nameParts.Length > 1 ? nameParts[nameParts.Length - 1] : "";

                    // Update Hire = 1 for matching student
                    string updateQuery = @"
                    UPDATE Student
                    SET is_approved = 1
                    WHERE UserId IN (
                        SELECT UserId FROM [User]
                        WHERE FirstName = @FirstName AND LastName = @LastName and Role='Student'
                    )
                    
                    UPDATE Recruiter
                    SET is_approved = 1
                    WHERE UserId IN (
                        SELECT UserId FROM [User]
                        WHERE FirstName = @FirstName AND LastName = @LastName and Role='Recruiter'
                    )


                    ";

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("User Approved Successfully.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Skip the new row placeholder
                    if (row.IsNewRow) continue;
                    string fullName = row.Cells["UserName"].Value.ToString().Trim();

                    // Split name to match with FirstName and LastName (you can improve this if needed)
                    string[] nameParts = fullName.Split(' ');
                    string firstName = nameParts[0];
                    string lastName = nameParts.Length > 1 ? nameParts[nameParts.Length - 1] : "";

                    // Update Hire = 1 for matching student
                    string updateQuery = @"
                    UPDATE Student
                    SET is_approved = 0
                    WHERE UserId IN (
                        SELECT UserId FROM [User]
                        WHERE FirstName = @FirstName AND LastName = @LastName and Role='Student'
                    )
                    
                    UPDATE Recruiter
                    SET is_approved = 0
                    WHERE UserId IN (
                        SELECT UserId FROM [User]
                        WHERE FirstName = @FirstName AND LastName = @LastName and Role='Recruiter'
                    )


                    ";

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("User Rejected Successfully.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Skip the new row placeholder
                    if (row.IsNewRow) continue;
                    string fullName = row.Cells["UserName"].Value.ToString().Trim();

                    // Split name to match with FirstName and LastName (you can improve this if needed)
                    string[] nameParts = fullName.Split(' ');
                    string firstName = nameParts[0];
                    string lastName = nameParts.Length > 1 ? nameParts[nameParts.Length - 1] : "";

                    // Update Hire = 1 for matching student
                    string updateQuery = @"
                    UPDATE [User]
                    SET IsActive = 0
                    WHERE UserId IN (
                        SELECT UserId FROM [User]
                        WHERE FirstName = @FirstName AND LastName = @LastName and Role='Student'
                    )
                    
                    UPDATE [User]
                    SET IsActive = 0
                    WHERE UserId IN (
                        SELECT UserId FROM [User]
                        WHERE FirstName = @FirstName AND LastName = @LastName and Role='Recruiter'
                    )


                    ";

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("User Deactivated Successfully.");
        }
    }
}
