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
    public partial class Form2r : Form
    {
        public Form2r()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
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

            // Get new CompanyID
            string getMaxIdQuery = "SELECT ISNULL(MAX(CompanyID), 0) + 1 FROM Companies";
            SqlCommand cmdMaxId = new SqlCommand(getMaxIdQuery, conn);
            int newCompanyId = Convert.ToInt32(cmdMaxId.ExecuteScalar());
            string Name = textBox1.Text;
            string Sector = textBox2.Text;
            string City = textBox3.Text;
            string Street = textBox4.Text;
            string Country = textBox6.Text;
            string ContactInfo = textBox5.Text;

            // Correct INSERT with parameters
            string query = @"INSERT INTO Companies
                 (CompanyID, Name, Sector, City, Street, Country, ContactInfo) 
                 VALUES 
                 (@CompanyID, @Name, @Sector, @City, @Street, @Country, @ContactInfo)";

            SqlCommand cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@CompanyID", newCompanyId);
            cm.Parameters.AddWithValue("@Name", Name);
            cm.Parameters.AddWithValue("@Sector", Sector);
            cm.Parameters.AddWithValue("@City", City);
            cm.Parameters.AddWithValue("@Street", Street);
            cm.Parameters.AddWithValue("@Country", Country);
            cm.Parameters.AddWithValue("@ContactInfo", ContactInfo);

            cm.ExecuteNonQuery();
            cm.Dispose();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
