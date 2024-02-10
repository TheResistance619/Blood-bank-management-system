using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BloodBankManagementSystem.BLL;
using BloodBankManagementSystem.DAL;

namespace BloodBankManagementSystem.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        //Create the Object of BLL And DAL
        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();

        //Create a Satatic string method to save the username
       public static string loggedInUser;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Write the Code to close the Apllication

            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //write the Code to Login Application
            //1. get the username and password from login form
            l.username = txtUsername.Text;
            l.password = txtPassword.Text;

            //Check the login Credentials
            bool isSuccess = dal.loginCheck(l);

            //Check whether the login is success or not
            //If login is success then isSuccess will be true else it will be false
            if (isSuccess == true)
            {
                //Login Success
                //Display Success Message
                MessageBox.Show("Login Successful");

                //Save the username in loggedInUser Static Method
                loggedInUser = l.username;


                //Display home form
                frmHome home = new frmHome();
                home.Show();
                this.Hide();  //To close login form
            }
            else
            {
                //Login Failed
                //Display the Error Message 
                MessageBox.Show("Login Failed, Try Again.");
            }
        }
    }
}
