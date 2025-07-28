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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form9 form1 = new Form9();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True"); //Connection String
            conn.Open();
            MessageBox.Show("Connection Open");
            SqlCommand cm;
            string FirstName = textBox1.Text;
            string MiddleName = textBox2.Text;
            string LastName = textBox3.Text;
            string Phone = textBox4.Text;
            string Password = textBox6.Text;
            string query = @"UPDATE [User]
                         SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName,
                             Phone = @Phone, Password=@Password
                         WHERE Email = @Email";
            cm = new SqlCommand(query, conn);
            cm.Parameters.AddWithValue("@FirstName", FirstName);
            cm.Parameters.AddWithValue("@MiddleName", MiddleName);
            cm.Parameters.AddWithValue("@LastName", LastName);
            cm.Parameters.AddWithValue("@Phone", Phone);
            cm.Parameters.AddWithValue("@Email", comboBox1.SelectedValue);
            cm.Parameters.AddWithValue("@Password", Password);
            cm.ExecuteNonQuery();
            cm.Dispose();
            conn.Close();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-MVTQJK3\\SQLEXPRESS;Initial Catalog=jobfair;Integrated Security=True";
            string query1 = "SELECT Email FROM [User]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query1, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "Email"; // What user sees
                comboBox1.ValueMember = "Email";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
