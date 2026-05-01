using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LostAndFoundManagementSystem
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            txtPass.UseSystemPasswordChar = true;
        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !checkBoxPass.Checked;
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtEmail.Clear();
            txtPass.Clear();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string id = txtIDD.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPass.Text;

            if (email == "" || password == "")
            {
                MessageBox.Show("Please enter email and password.");
                return;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            try
            {
                using (SqlConnection con = Database.GetConnection())
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("This email already exists.");
                        return;
                    }

                    string insertQuery = @"INSERT INTO Users
                    (UniversityID, Email, Password, UserRole)
                    VALUES
                    (@ID, @Email, @Password, 'Student')";

                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Account created successfully!");

                    txtIDD.Clear();
                    txtEmail.Clear();
                    txtPass.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
