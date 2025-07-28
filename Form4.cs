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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True"); //Connection String
            conn.Open();
            MessageBox.Show("Connection Open");
            string getMaxIdQuery = "SELECT ISNULL(MAX(BoothID), 0) + 1 FROM Booth";
            SqlCommand cmdMaxId = new SqlCommand(getMaxIdQuery, conn);
            int newBoothId = Convert.ToInt32(cmdMaxId.ExecuteScalar());
            int CompanyID = Convert.ToInt32(comboBox3.SelectedValue);
            int EventId = Convert.ToInt32(comboBox1.SelectedValue);
            int BoothCoordinatorID = Convert.ToInt32(comboBox4.SelectedValue);
            string location = Convert.ToString(comboBox2.SelectedValue);
            SqlCommand cm;
            SqlCommand cm1;
            string query = @"Insert into Booth (BoothID,CompanyID,EventId,location) values (@BoothId,@CompanyID,@EventId,@location)";
            cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@BoothID", newBoothId);
            cm.Parameters.AddWithValue("@CompanyID", CompanyID);
            cm.Parameters.AddWithValue("@EventId", EventId);
            cm.Parameters.AddWithValue("@location",location);
            cm.ExecuteNonQuery();
            cm.Dispose();

            string query2 = @"Insert into Monitors (booth_id,Userid) values (@BoothID,@BoothCoordinatorID)";
            cm1 = new SqlCommand(query2, conn);
            cm1.Parameters.AddWithValue("@BoothID", newBoothId);
            cm1.Parameters.AddWithValue("@BoothCoordinatorID", BoothCoordinatorID);
            cm1.ExecuteNonQuery();
            cm1.Dispose();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8(); // Create instance of the new form
            form8.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            string query1 = "SELECT EventID,Title from JobFairEvents";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query1, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "Title"; // What user sees
                comboBox1.ValueMember = "EventID";
            }

            string query2 = "SELECT BoothID,location FROM Booth";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query2, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "location"; // What user sees
                comboBox2.ValueMember = "location";
            }

            string query3 = "SELECT CompanyID,Name FROM Companies";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query3, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "Name"; // What user sees
                comboBox3.ValueMember = "CompanyID";
            }

            string query4 = "SELECT b.BoothCoordinatorId,u.FirstName+''+u.MiddleName+''+u.LastName as Coordinator FROM Booth_Coordinator as b join [User] as u on b.UserId=u.UserId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query4, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox4.DataSource = dt;
                comboBox4.DisplayMember = "Coordinator"; // What user sees
                comboBox4.ValueMember = "b.BoothCoordinatorID";
            }
        }
    }
}
