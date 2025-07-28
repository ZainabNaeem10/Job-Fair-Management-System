using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TPO
{
    public partial class Form3r : Form
    {
        public Form3r()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True");
            conn.Open();
            MessageBox.Show("Connection Open");

            string getMaxIdQuery = "SELECT ISNULL(MAX(JobID), 0) + 1 FROM JobPostings";
            SqlCommand cmdMaxId = new SqlCommand(getMaxIdQuery, conn);
            int newJobId = Convert.ToInt32(cmdMaxId.ExecuteScalar());
            string Title = textBox1.Text;
            string Salary = textBox2.Text;
            string Description = textBox3.Text;
            string Street = textBox4.Text;
            string City = textBox5.Text;
            string Country = textBox6.Text;
            string Type = textBox7.Text;
            string RequiredSkills = textBox8.Text;
            int CompanyId = Convert.ToInt32(comboBox1.SelectedValue);
            int EventId = Convert.ToInt32(comboBox2.SelectedValue);


            // Correct INSERT with parameters
            string query = @"INSERT INTO Jobpostings
                 (JobID, Title, Salary, Description, Street, City, Country, CompanyID, EventId)
                 VALUES 
                 (@JobID, @Title, @Salary, @Description, @Street, @City, @Country,@CompanyID, @EventId)";

            SqlCommand cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@JobID", newJobId);
            cm.Parameters.AddWithValue("@Title", Title);
            cm.Parameters.AddWithValue("@Salary", Salary);
            cm.Parameters.AddWithValue("@Description", Description);
            cm.Parameters.AddWithValue("@Street", Street);
            cm.Parameters.AddWithValue("@City", City);
            cm.Parameters.AddWithValue("@Country", Country);
            cm.Parameters.AddWithValue("@CompanyID", CompanyId);
            cm.Parameters.AddWithValue("@EventId", EventId);
            cm.ExecuteNonQuery();
            cm.Dispose();

            string query3 = @"INSERT INTO Jobpostings_Type
                 (Job_id, Type) VALUES (@Job_id, @Type)";
            SqlCommand cm1 = new SqlCommand(query3, conn);
            cm1.Parameters.AddWithValue("@Job_id", newJobId);
            cm1.Parameters.AddWithValue("@Type", Type);
            cm1.ExecuteNonQuery();
            cm1.Dispose();

            string query4 = @"INSERT INTO Jobpostings_RequiredSkills
                 (Job_id, RequiredSkills) VALUES (@Job_id, @RequiredSkills)";
            SqlCommand cm2 = new SqlCommand(query4, conn);
            cm2.Parameters.AddWithValue("@Job_id", newJobId);
            cm2.Parameters.AddWithValue("@RequiredSkills", RequiredSkills);
            cm2.ExecuteNonQuery();
            cm2.Dispose();


        }

        private void Form3_Load(object sender, EventArgs e)
        {
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

   
            

            using (SqlConnection conn = new SqlConnection(connectionString))
            
            {
                string query2 = "SELECT EventID,Title FROM JobFairEvents";
                SqlDataAdapter da = new SqlDataAdapter(query2, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "Title"; // What user sees
                comboBox2.ValueMember = "EventID";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
