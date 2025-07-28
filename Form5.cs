using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TPO
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8(); // Create instance of the new form
            form8.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True"))
            {
                conn.Open();
                MessageBox.Show("Connection Open");
                int EventId = Convert.ToInt32(comboBox1.SelectedValue);
                string query = @"SELECT j.Title, j.StartDate, j.EndDate, j.BoothSlots 
                     FROM JobFairEvents AS j 
                     WHERE j.EventId = @EventId";
                using (SqlCommand cm = new SqlCommand(query, conn))
                {
                    cm.Parameters.AddWithValue("@EventId", EventId);
                    using (SqlDataReader reader = cm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            label10.Text = reader["Title"].ToString();
                            label12.Text = Convert.ToDateTime(reader["StartDate"]).ToString("yyyy-MM-dd");
                            label13.Text = Convert.ToDateTime(reader["EndDate"]).ToString("yyyy-MM-dd");
                            label11.Text = reader["BoothSlots"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No event found with the selected ID.");
                            return;
                        }
                    }
                }

                
                string query1 = @"SELECT COUNT(DISTINCT M.Userid) AS AvailableStaff
                      FROM Monitors M
                      JOIN Booth B ON B.BoothID = M.booth_id
                      WHERE B.EventId = @EventId";
                using (SqlCommand cm1 = new SqlCommand(query1, conn))
                {
                    cm1.Parameters.AddWithValue("@EventId", EventId);
                    object staffResult = cm1.ExecuteScalar();
                    label14.Text = staffResult != null ? staffResult.ToString() : "0";
                }

                
                string query2 = @"SELECT 
                        (SELECT COUNT(*) FROM Booth WHERE EventId = @EventId) AS UsedBooths,
                        (SELECT BoothSlots FROM JobFairEvents WHERE EventId = @EventId) AS TotalBoothSlots";
                using (SqlCommand cm2 = new SqlCommand(query2, conn))
                {
                    cm2.Parameters.AddWithValue("@EventId", EventId);
                    using (SqlDataReader reader2 = cm2.ExecuteReader())
                    {
                        if (reader2.Read())
                        {
                            int used = Convert.ToInt32(reader2["UsedBooths"]);
                            int total = Convert.ToInt32(reader2["TotalBoothSlots"]);
                            if (total > 0)
                            {
                                double percentage = (used / (double)total) * 100;
                                label15.Text = $"{percentage:F2}% booths used";
                            }
                            else
                            {
                                label15.Text = "0% booths used";
                            }
                        }
                    }
                }
            }

        }

        private void Form5_Load(object sender, EventArgs e)
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
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
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
    }
}
