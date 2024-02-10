using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BloodBankManagementSystem.DAL;
using BloodBankManagementSystem.UI;

namespace BloodBankManagementSystem
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        //Create the Object of Donor Dal
        donorDAL dal = new donorDAL();


        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers users = new frmUsers();
            users.Show();
        }

        private void donorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open Manage Donors Form
            frmDonors donors = new frmDonors();
            donors.Show();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            //Load all the Blood Groups Count When Form is Loaded 
            //Call allDonorsCountMethod
            allDonorCount();

            //Display all the Donors
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

            //Display the username of Logged in User 
            lblUser.Text = frmLogin.loggedInUser;
        }

        public void allDonorCount()
        {
          //Get the Donor Count from Database and Set in respective label
            lblOpositiveCount.Text = dal.countDonors("O+");
            lblOnegativeCount.Text = dal.countDonors("O-");
            lblApositiveCount.Text = dal.countDonors("A+");
            lblAnegativeCount.Text = dal.countDonors("A-");
            lblBpositiveCount.Text = dal.countDonors("B+");
            lblBnegativeCount.Text = dal.countDonors("B-");
            lblABpositiveCount.Text = dal.countDonors("AB+");
            lblABnegativeCount.Text = dal.countDonors("AB-");
        }

        private void frmHome_Activated(object sender, EventArgs e)
        {
            //Call allDonorCount Method
            allDonorCount();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the Keywords from the search TextBox
            string keywords = txtSearch.Text;

            //Check whether the TextBox is Empty or Not 
            if (keywords != null)
            {
                //Filter the Donors based on Keywords
                DataTable dt = dal.Search(keywords);
                dgvDonors.DataSource = dt;
            }
            else
            {
                 //Display all the Donors
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
        }
    }
}
