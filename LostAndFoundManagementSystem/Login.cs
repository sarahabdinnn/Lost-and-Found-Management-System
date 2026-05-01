using Microsoft.Win32;
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
using System.Xml.Linq;

namespace LostAndFoundManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtPass.UseSystemPasswordChar = true;
        }

        private void bttnLogin_click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Please Enter Your ID and Password");
                return;
            }
            try
            {
                using (SqlConnection con = Database.GetConnection())
                {
                    con.Open();

                    string query = "SELECT UserRole FROM Users WHERE UniversityID = @ID AND Password = @Password";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPass.Text);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string role = result.ToString();

                        MessageBox.Show("Login Successful!");

                        if (role == "Admin")
                        {
                            // AdminDashboard admin = new AdminDashboard();
                            // admin.Show();
                            // this.Hide();
                        }
                        else
                        {
                            // StudentDashboard student = new StudentDashboard();
                            // student.Show();
                            // this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid ID or Password");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;

            try
            {
                using (SqlConnection con = Database.GetConnection())
                {
                    con.Open();
                    MessageBox.Show("Database connected successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtPass.Clear();
        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Applicaton", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();
            this.Hide();
        }
    }
}



