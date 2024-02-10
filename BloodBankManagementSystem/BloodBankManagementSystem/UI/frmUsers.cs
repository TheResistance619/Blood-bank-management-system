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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

       //Create  Object of userBLL and userDAL
        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        string imageName = "no-image-jpg";

        //Global variable for the image to delete 
        string rowHeaderImage;

        private void lblFormTitle_Click(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblFullName_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Add functionality  to close this form 
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Stop it get thr values from UI
            u.full_name = txtFullName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.added_date = DateTime.Now;
            u.image_name = imageName;

            //Step 2 : Adding the values from UI to the Database
            //Create a Boolean variable to check whether the data is inserted successfully or not
            bool success = dal.Insert(u);

            //Step 3 : Chech whether the Data is Inseted Successfully or Not
            if (success == true)
            {
                //Data or User Added succesffuly
                MessageBox.Show("New User Added Succesfully");

                //Display the User in Datagrid view
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                //Clear TextBoxes
                Clear();
            }
            else
            {
                //Failed to Add User 
                MessageBox.Show("Failed To Add New User.");
            }

        }

        //Method or Function to Clear TextBoxes
        public void Clear()
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtPassword.Text = "";
            txtUserID.Text = "";
            //Path to destination Folder
            //Get the Image Path
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            string imagePath = paths + "\\images\\no-image.jpg";
            //Display in Picture Box
            pictureBoxProfilePicture.Image = new Bitmap(imagePath);
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Find the Row Index of the Row Clicked on Users Data Field View
            int RowIndex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[RowIndex].Cells[0].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[RowIndex].Cells[1].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[RowIndex].Cells[2].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[RowIndex].Cells[3].Value.ToString();
            txtFullName.Text = dgvUsers.Rows[RowIndex].Cells[4].Value.ToString();
            txtContact.Text = dgvUsers.Rows[RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[RowIndex].Cells[6].Value.ToString();
            imageName = dgvUsers.Rows[RowIndex].Cells[8].Value.ToString();

            //update the value of global variable rowHeaderImage
            rowHeaderImage = imageName;

            //Display The Image of Selected User
            //Get The Image Path
            string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
          if (imageName != "no-image.jpg")
            {
                //Path to destination Folder
                string imagePath = paths + "\\images\\" + imageName;
                //Display in Picture Box
               pictureBoxProfilePicture.Image = new Bitmap(imagePath);
            }
            else 
            {
                //Path to destination Folder
                string imagePath = paths + "\\images\\no-image.jpg";
                //Display in Picture Box
                pictureBoxProfilePicture.Image = new Bitmap(imagePath);
            }
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            //Display the USers in DataGrid View when the FORM is Loaded
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void tnUpdate_Click(object sender, EventArgs e)
        {
            //Step1 : Get The Values from UI
            u.user_id = int.Parse(txtUserID.Text);
            u.full_name = txtFullName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.added_date = DateTime.Now;
            u.image_name = imageName;

            //Step 2 : Create a Boolean variable to check whether the data is updated successfully or not 
            bool success = dal.Update(u);

            //Lets's Check whether the data is updated or not
            if (success == true)
            {
                //Data Updated Successfully
                MessageBox.Show("User Updated Successfully");

                //Refresh DataGridView
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                //Clear the TextBoxes
                Clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Step 1 : Get the UserID from Text box to Delete the User 
            u.user_id = int.Parse(txtUserID.Text);

            //Remove the physical file of the user profile
            if (rowHeaderImage != "no-image.jpg")
            {
                //Path of the project folder
               string paths = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
               
                //Give the path to the image folder
               string imagePath = paths + "\\images\\" + rowHeaderImage;

                //Call clear function to clear all the textboxes and PictureBox
               Clear();

               //Call Garbage  collection function
               GC.Collect();
               GC.WaitForPendingFinalizers();

              //Delete the physical file of the user profile
              File.Delete(imagePath);
            }

            //Step Create thr Boolean value to check whether the user deleted or not 
            bool success = dal.Delete(u);

            //Let's check whether the user is deleted or not 
            if (success == true)
            {
                //User Deleted Successfully
                MessageBox.Show("User Deleted Successfully");

                //Refresh DataGrid View
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

                //Clear the TextBoxes
                Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Call the user function
            Clear();
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            //Write the code to upload the Image of User
            //Open Dialog Box Select Image
            OpenFileDialog open = new OpenFileDialog();

            //Filter the file Type, only Allow Image File Types
            open.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.PNG; *.gif;)|*.jpg; *.jpeg; *.png; *.PNG; *.gif;";

            //Check if the file is selected or Not
            if (open.ShowDialog() == DialogResult.OK)
            {
                //Check if the file exists or not
                if (open.CheckFileExists)
                {
                    pictureBoxProfilePicture.Image = new Bitmap(open.FileName);

                    //Rename the Image we Selected 
                    //1. Get the Extention of Image
                    string ext = Path.GetExtension(open.FileName);

                    //2. Generate Random Integer
                    Random random = new Random();
                    int RandInt = random.Next(0, 1000);

                    //3.Rename the Image
                    imageName = "Blood_Bank_MS_" + RandInt + ext;


                    //4. Get the Path of Selected Image
                    string sourcePath = open.FileName;

                    //5. Get the Path of Destination
                    string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

                    //Paths to Destination Folder 
                    string destinationPath = paths + "\\images\\" + imageName;

                    //6. Copy Image to the Destination Folder
                    File.Copy(sourcePath, destinationPath);

                    //7. Display Message
                    MessageBox.Show("Image Succefully Uploaded.");

                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Write the code to get the user Based Keyboards
            //1. Get the Keyboards from the Textbox
            String keywords = txtSearch.Text;

            //Check whether the textbox is empty or not
            if (keywords != null)
            {
                //Textbox is not empty, display users on Data Grid View based on the keywords
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else 
            {
                //Textbox is Empty and display all user on Data Grid View 
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
        }
    }
}
