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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8(); // Create instance of the new form
            form8.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm tt"; // includes both date and time
            dateTimePicker1.ShowUpDown = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM/dd/yyyy hh:mm tt"; // includes both date and time
            dateTimePicker2.ShowUpDown = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True");
            conn.Open();
            MessageBox.Show("Connection Open");

            string getMaxIdQuery = "SELECT ISNULL(MAX(EventID), 0) + 1 FROM JobFairEvents";
            SqlCommand cmdMaxId = new SqlCommand(getMaxIdQuery, conn);
            int newEventId = Convert.ToInt32(cmdMaxId.ExecuteScalar());
            string Title = textBox1.Text;
            string Venue = textBox2.Text;
            string BoothSlots = textBox3.Text;
            bool is_publish=false ;
            DateTime StartDate = dateTimePicker1.Value;
            DateTime EndDate = dateTimePicker2.Value;
            if (EndDate <= StartDate)
            {
                MessageBox.Show("End time must be after start time.");
                return;
            }
            string query = @"INSERT INTO JobFairEvents
                 (EventID, Title, StartDate, EndDate, Venue, BoothSlots, is_publish)
                 VALUES 
                 (@EventID, @Title, @StartDate, @EndDate, @Venue, @BoothSlots, @is_publish)";

            SqlCommand cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@EventID", newEventId);
            cm.Parameters.AddWithValue("@Title", Title);
            cm.Parameters.AddWithValue("@StartDate", StartDate);
            cm.Parameters.AddWithValue("@EndDate", EndDate);
            cm.Parameters.AddWithValue("@Venue", Venue);
            cm.Parameters.AddWithValue("@BoothSlots", BoothSlots);
            cm.Parameters.AddWithValue("@is_publish", is_publish);
            cm.ExecuteNonQuery();
            cm.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string Title = textBox1.Text;
                conn.Open();
                    string updateQuery = @"
                    UPDATE JobFairEvents
                    SET is_publish = 1
                    WHERE Title=@Title
                    ";

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.ExecuteNonQuery();
                }

            MessageBox.Show("Event Published Successfully.");
        }
    }
}
