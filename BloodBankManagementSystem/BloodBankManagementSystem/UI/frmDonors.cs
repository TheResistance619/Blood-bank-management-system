using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BloodBankManagementSystem.BLL;
using BloodBankManagementSystem.DAL;

namespace BloodBankManagementSystem.UI
{
    public partial class frmDonors : Form
    {
        public frmDonors()
        {
            InitializeComponent();
        }
        //Create object of Donor BLL and Donor DAL
        donorBLL d = new donorBLL();
        donorDAL dal = new donorDAL();
        userDAL udal = new userDAL();

        //Global variable for Image
        string imageName = "no-image.jpg";

        string rowHeaderImage;

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Close this form
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //We will write the code to Add new Donor
            //Step 1 . Get the Data from Manage Donors Form
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;  
            d.contact = txtContact.Text;
            d.address = txtAddress.Text;
            d.added_date = DateTime.Now;

            //Get the ID of Logged In User
            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = udal.GetIDFromUsername(loggedInUser); 

            d.added_by = usr.user_id;  //TODO: GET the ID of logged  in User

            d.image_name = imageName;

            //Step 2 : Inserting Data into Database
            //Create a Boolean Variable to Insert Data into Database and Check whether the data inserted succesfully of not
            bool isSuccess = dal.Insert(d);

            //if the Data is inserted  successfully then the values of isSuccess will be True else is will be false
            if (isSuccess == true)
            {
                //Data Inserted Successfully
                MessageBox.Show("New Donor Added Succesfully");

                //Refresh Datagrid view
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;



                //Clear all the TextBoxes
                Clear();
            }
            else 
            {
                //Failed to insert Data
                MessageBox.Show("Failed to Add new Donor.");
            }
        }
        ///Create a method to clear all the TextBoxes
        public void Clear() 
        {
            //Clear all the TextBox
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtDonorID.Text = "";
            cmbGender.Text = "";
            cmbBloodGroup.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";

            //Clear the PictureBox
            //First we need to get the image path 
            string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 10);

            string imagepath = path + "\\images\\no-image.jpg";

                //Display Image in PictureBox
                pictureBoxProfilePicture.Image = new Bitmap(imagepath);
        }

        private void frmDonors_Load(object sender, EventArgs e)
        {
            //Display Donors in Data Grid View
            DataTable dt = dal.Select();
            dgvDonors.DataSource = dt;

            //First we need to get the image path 
            string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 10);

            string imagepath = path + "\\images\\no-image.jpg";

            //Display Image in PictureBox
            pictureBoxProfilePicture.Image = new Bitmap(imagepath);
        }

        private void dgvDonors_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Select the Data from Data grid View and Display in our Form

            //1. Find the Row Selected
            int RowIndex = e.RowIndex;

            txtDonorID.Text = dgvDonors.Rows[RowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvDonors.Rows[RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvDonors.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDonors.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDonors.Rows[RowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvDonors.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvDonors.Rows[RowIndex].Cells[6].Value.ToString();
            cmbBloodGroup.Text = dgvDonors.Rows[RowIndex].Cells[7].Value.ToString();

            imageName = dgvDonors.Rows[RowIndex].Cells[9].Value.ToString();

            //Update the value of rowHeaderImage
             rowHeaderImage = imageName;            

            //Display the Image of Selected Donor
            //Get the Image path
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 10);
            string imagePath = paths + "\\images\\" + imageName;

            //Display the image of selected User
            pictureBoxProfilePicture.Image = new Bitmap(imagePath);


        }

        private void tnUpdate_Click(object sender, EventArgs e)
        {
            //Add the functionality to Update the Donors
            //1. Get the Values from Form
            d.donor_id = int.Parse(txtDonorID.Text);
            d.first_name = txtFirstName.Text;
            d.last_name = txtLastName.Text;
            d.email = txtEmail.Text;
            d.gender = cmbGender.Text;
            d.blood_group = cmbBloodGroup.Text;
            d.contact = txtContact.Text;
            d.address = txtAddress.Text;
            //Get the ID of Logged In User
            string loggedInUser = frmLogin.loggedInUser;
            userBLL usr = udal.GetIDFromUsername(loggedInUser);

            d.added_by = usr.user_id;  //TODO: GET the ID of logged  in User
            d.image_name = imageName;

            //Create a Boolean Variable to check whether the data is updated Succesfully or not
            bool isSuccess = dal.Update(d);

            //If the data updated succesfully then the value of isSuccess will be true else it will be false
            if (isSuccess == true)
            {
                //Donor Updated Succesfully
                MessageBox.Show("Donor Updated Succesfully");
                Clear();

                //Refresh DataGridView
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
            else 
            {
                //Failed to update
                MessageBox.Show("Failed to Update Donors");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the Value from Form
            d.donor_id = int.Parse(txtDonorID.Text);

            //Check wether the donor has profile picture or not
            if(rowHeaderImage !="no-name.jpg")
            {
                //Only runs if user has image
                //Get the path to the root folder of the project
                string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length) - 10);

                //Get the path of the image
                string imagePath = path + "\\images\\" + rowHeaderImage;

                //Call  clear  function 
                Clear();

                //Call garbage collection
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //Delete the Physical image file of donors
                File.Delete(imagePath);
            }

            //Create a Boolean Variable to Check whether the Donor deleted or not
            bool isSuccess = dal.Delete(d);

            if (isSuccess == true)
            {
                //Donor Deleted Succesfully
                MessageBox.Show("Donor Deleted Successfully");

                Clear();

                //Refresh Datagrid View
                DataTable dt = dal.Select();
                dgvDonors.DataSource = dt;
            }
            else
            {
                //Failed to Delete User
                MessageBox.Show("Failed to Delete Donor");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clear the TextBoxes
            Clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            //Code to select Image and Upload
            //1. Open the Dialog Box to Select Image
            OpenFileDialog open = new OpenFileDialog();

            //2. Filter the file Type (allow only Image Files)
            open.Filter = "Image Files Only (*.jpg; *.jpeg; *.png; *.gif| *.jpg; *.jpeg; *.png; *.gif)";

            //3. Check Whether the Image is Selected or not
            if (open.ShowDialog() == DialogResult.OK)
            {
                //Check if the file exists or not
                if (open.CheckFileExists)
                {
                    //Dipslay the Selected Image in PictureBox
                    pictureBoxProfilePicture.Image = new Bitmap(open.FileName);

                    //Rename the Selected Image in PictureBox
                    string ext = Path.GetExtension(open.FileName);

                    string name = Path.GetFileNameWithoutExtension(open.FileName);

                    //Generate Random but Globally Unique Identifier
                    Guid g = new Guid();
                    g = Guid.NewGuid();

                    //Finally Rename Our Image
                    imageName = "Blood_Bank_MS_" + name + g + ext;

                    //Get the Source Path (Path Of Image)
                    string sourcePath = open.FileName;

                    //Get the Destination Path
                    string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                    //Path to Destination
                    string destinationPath = paths + "\\images\\" + imageName;

                    //Upload the Image to Destination Folder
                    File.Copy(sourcePath, destinationPath);

                    //Display Message after the image is upload successfully
                    MessageBox.Show("Image Successfully Uploaded.");
                }
            }
        }

        private void dgvDonors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Let's Add The Functionality to search the donors

            //1. Get the keywords Typed on the search text box
            string keywords = txtSearch.Text;

            //Check whether the searxh TextBox is empty or not
            if (keywords != null)
            {
                //Display the information of donors Based on keywords
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


		
