using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TPO
{
    public partial class StudentDashboard : Form
    {
        public StudentDashboard()
        {
            InitializeComponent();
            CustomizeUI(); // Call the custom UI method
        }


        // Method to load an image from a URL

        public void LoadImageFromUrl(string url)
        {
            try
            {
                // Send a request to the URL and get the response
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();

                // Read the image stream from the response
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream != null && stream.Length > 0)
                    {
                        // Create a memory stream and copy the data
                        MemoryStream memoryStream = new MemoryStream();
                        stream.CopyTo(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin); // Set the position of the stream to the start

                        // Load the image from the memory stream
                        Image image = Image.FromStream(memoryStream);

                        // Set the image to the button (or other control)
                        button1.Image = image;
                    }
                    else
                    {
                        MessageBox.Show("Failed to load image: Stream is empty.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Display an error message if something goes wrong
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }


        // Custom UI Method for styling
        /*  private void CustomizeUI()
          {
              // Set a nice background color
              this.BackColor = Color.WhiteSmoke;

              // Set form title font and color
              this.Text = "Student Dashboard";
              this.Font = new Font("Segoe UI", 12);
              this.ForeColor = Color.DarkSlateGray;

              // Style the buttons
              foreach (Button btn in this.Controls.OfType<Button>())
              {
                  btn.FlatStyle = FlatStyle.Flat;
                  btn.FlatAppearance.BorderSize = 0;
                  btn.BackColor = Color.MediumSlateBlue;
                  btn.ForeColor = Color.White;
                  btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                  btn.Width = 200;
                  btn.Height = 40;
                  btn.Cursor = Cursors.Hand;

                  // Add hover effects
                  btn.MouseEnter += (sender, e) => { btn.BackColor = Color.MediumPurple; };
                  btn.MouseLeave += (sender, e) => { btn.BackColor = Color.MediumSlateBlue; };

                  // Add Tooltips
                  ToolTip tooltip = new ToolTip();
                  tooltip.SetToolTip(btn, btn.Text);
              }



          }*/

        private void CustomizeUI()
        {
            this.BackColor = Color.WhiteSmoke;
            this.Text = "Student Dashboard";
            this.Font = new Font("Segoe UI", 12);
            this.ForeColor = Color.DarkSlateGray;

            // setup tooltip once
            ToolTip tooltip = new ToolTip();

            foreach (Button btn in this.Controls.OfType<Button>())
            {
                // set flat style
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.MediumSlateBlue;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btn.Width = 220;
                btn.Height = 45;
                btn.Cursor = Cursors.Hand;

                // slight rounded corners
                btn.Region = System.Drawing.Region.FromHrgn(
                    CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 20, 20)
                );

                // add nice hover effect
                btn.MouseEnter += (sender, e) =>
                {
                    btn.BackColor = Color.FromArgb(123, 104, 238); // a little lighter
                    btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                };
                btn.MouseLeave += (sender, e) =>
                {
                    btn.BackColor = Color.MediumSlateBlue;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                };

                // set tooltip
                tooltip.SetToolTip(btn, btn.Text);

                // you can add emojis based on button text
                if (btn.Text.Contains("Profile"))
                    btn.Text = "👤 " + btn.Text;
                else if (btn.Text.Contains("Job") || btn.Text.Contains("Jobs"))
                    btn.Text = "💼 " + btn.Text;
                else if (btn.Text.Contains("Companies"))
                    btn.Text = "🏢 " + btn.Text;
                else if (btn.Text.Contains("Skills"))
                    btn.Text = "🛠️ " + btn.Text;
                else if (btn.Text.Contains("Certificates"))
                    btn.Text = "📜 " + btn.Text;
                else if (btn.Text.Contains("Interviews"))
                    btn.Text = "📝 " + btn.Text;
                else if (btn.Text.Contains("Reviews"))
                    btn.Text = "⭐ " + btn.Text;
                else if (btn.Text.Contains("Applications"))
                    btn.Text = "📄 " + btn.Text;
                else if (btn.Text.Contains("Visits"))
                    btn.Text = "🎪 " + btn.Text;
                else if (btn.Text.Contains("Academic"))
                    btn.Text = "🎓 " + btn.Text;
                else if (btn.Text.Contains("Log Out"))
                    btn.Text = "🚪 " + btn.Text;
            }
        }

        // you need this function to make buttons rounded
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Create a helper method to set the active button style and show content
        private void SetActiveButton(Button activeButton, Panel activePanel)
        {
            // Reset all buttons to default style
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button btn = (Button)control;
                    btn.BackColor = Color.LightGray; // Default background color
                    btn.ForeColor = Color.Black; // Default text color
                }
            }
            // Set the selected button style
            activeButton.BackColor = Color.Blue; // Active button color
            activeButton.ForeColor = Color.White; // Active button text color

            // Hide all panels
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    control.Visible = false;
                }
            }

            // Show the panel related to the active button
            activePanel.Visible = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ViewUpdateProfile viewProfile = new ViewUpdateProfile(); // create object
            viewProfile.Show(); // open that form
            this.Hide(); // hide current form (dashboard), optional
       
        }
        ///
        private void button9_Click(object sender, EventArgs e)
        {
            ViewJobFairs viewProfile = new ViewJobFairs(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SearchCompanies viewProfile = new SearchCompanies(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }
        //
        private void button7_Click(object sender, EventArgs e)
        {
            ApplyForJobs viewProfile = new ApplyForJobs(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }
        //
        private void button6_Click(object sender, EventArgs e)
        {
            MyApplications viewProfile = new MyApplications(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InterviewsSchedule viewProfile = new InterviewsSchedule(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }

        private void button3_Click(object sender, EventArgs e)
        {
            viewSkills viewProfile = new viewSkills(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Certificates viewProfile = new Certificates(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }

        private void button11_Click(object sender, EventArgs e)
        {
            BoothVisits viewProfile = new BoothVisits(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }

        private void button10_Click(object sender, EventArgs e)
        {
            giveReview viewProfile = new giveReview(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }

        private void button12_Click(object sender, EventArgs e)
        {
            LogOut viewProfile = new LogOut(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        
    }

        private void button2_Click(object sender, EventArgs e)
        {
            AcademicRecord viewProfile = new AcademicRecord(); // create object
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form1 viewProfile = new Form1(); // form1 is manageInterviews
            viewProfile.Show(); // correct: use the object, not the class
            this.Hide(); // hide current form (optional)
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void StudentDashboard_Load(object sender, EventArgs e)
        {
            button1.Size = new Size(button1.Width + 20, button1.Height + 10);

        }
    }
}
