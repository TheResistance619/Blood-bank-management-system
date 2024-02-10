using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BloodBankManagementSystem.BLL;

namespace BloodBankManagementSystem.DAL
{
    class donorDAL
    {
        //Create a a connection String to Connect Database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region SELECT to display data in DataGridView from database
        public DataTable Select()
        {
            // Create object to Datable to hold the data from database and return it 
            DataTable dt = new DataTable();

            //Create object of SQL Connection to Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Write SQl Query to select the data from Database
                string sql = "SELECT * FROM tbl_donors";

                //Create the SQLCommand to execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create SQl Data Adapter to Hold the Data Temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open Database Connection
                conn.Open();

                //Pass the Data From adapter to DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Display Message if there's any exceptional Errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //CLose Database Connection
                conn.Close();
            }


            return dt;
        }
        #endregion
        #region INSERT data to database
        public bool Insert(donorBLL d)
        {
            //Create a boolean Variable and Set its Deault value to false
            bool isSuccess = false;
            
            //Create SqlConnection to Conncet Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Write  the Query to Insert data into database
                string sql = "INSERT INTO tbl_donors (first_name, last_name, email, contact, gender, address, blood_group, added_date, image_name, added_by) VALUES (@first_name, @last_name, @email, @contact, @gender, @address, @blood_group, @added_date, @image_name, @added_by)";

                //Create SQl Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Pass the Value to SQl Query
                cmd.Parameters.AddWithValue("@first_name", d.first_name);
                cmd.Parameters.AddWithValue("@last_name", d.last_name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@gender", d.gender);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@blood_group", d.blood_group);
                cmd.Parameters.AddWithValue("@added_date", d.added_date);
                cmd.Parameters.AddWithValue("@image_name", d.image_name);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);

                //Open Databse Connection
                conn.Open();

                //Create an Integer Variable to check wheter the Query was executed Succesfully or Not
                int rows = cmd.ExecuteNonQuery();

                //If  the Query is Executed Succesfully the value of rows will be greater than zero else it will be zero 
                if (rows > 0)
                {
                    //Query Executed Succesfully
                    isSuccess = true;
                }
                else 
                {
                    //Failed  to Execute Query 
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                //Diplay Error Message if there's any Exceptional Errors 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Databse Connection
                conn.Close();
 
            }


            return isSuccess;

        }
        #endregion
        #region UPDATE donors in Database
        public bool Update(donorBLL d)
        {
            //Create a Boolean Variable and Set its Default value to False
            bool isSuccess = false;

            //Create SQLConnection to Connect Databse
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a SQL query to Update Donors
                string sql = "UPDATE tbl_donors SET first_name=@first_name, last_name=@last_name, email=@email, contact=@contact, gender=@gender, address=@address, blood_group=@blood_group, image_name=@image_name, added_by=@added_by WHERE donor_id=@donor_id";

                //Create SQL Command Here
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Pass the Value to SQL Command Here
                cmd.Parameters.AddWithValue("@first_name", d.first_name);
                cmd.Parameters.AddWithValue("@last_name", d.last_name);
                cmd.Parameters.AddWithValue("@email", d.email);
                cmd.Parameters.AddWithValue("@contact", d.contact);
                cmd.Parameters.AddWithValue("@gender", d.gender);
                cmd.Parameters.AddWithValue("@address", d.address);
                cmd.Parameters.AddWithValue("@blood_group", d.blood_group);
                cmd.Parameters.AddWithValue("@image_name", d.image_name);
                cmd.Parameters.AddWithValue("@added_by", d.added_by);
                cmd.Parameters.AddWithValue("@donor_id", d.donor_id);

                //Open Database Connection
                conn.Open();

                //Create an Integer variable to check whether the query executed Succesfully or not
                int rows = cmd.ExecuteNonQuery();
                 
                //if the query is executed Succesfully then it's value will be greater than 0 else it will be 0
                if (rows > 0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //Display Error if theer's any Eceptional Errors 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
            
        }

        #endregion
        #region DELETE donors from Database
        public bool Delete(donorBLL d)
        {
            //Create a Boolean variable and set its default value to false
            bool isSuccess = false;
            //Create SqlConnection to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Write the Query to Delete Donors from Database
                string sql = "DELETE FROM tbl_donors WHERE donor_id=@donor_id;";

                //Create SqlConnection to Connect Database
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Pass the value to Sql Query using Parameters
                cmd.Parameters.AddWithValue("@donor_id",d.donor_id);

                //Open Database Connection
                conn.Open();

                //Create an Integer Variable to check whether the query executed Successfully or not
                int rows  = cmd.ExecuteNonQuery();

                //If the query executed Succesfully then the value of rows will be greater than 0 else it will be 0
                if(rows > 0)
                {
                    //Query Executed Succefully
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //Display Error Message if there's any Exceptional Errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }
            return isSuccess;
        }
        #endregion

        #region Count Donors for Specific Blood Group
        public string countDonors(string blood_group)
        {
            //Create SQL Connection for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create a string variable for donor and set its default value to 0
            string donors = "0";

            try
            {
                //SQl Query to Count donors for Specific Blood Group
                string sql = "SELECT * FROM tbl_donors WHERE blood_group = '"+ blood_group +"'";

                //Sql Command to Execute Query 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SQL Data Adapter to get data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //DataTable to Hold the data Temporarily
                DataTable dt = new DataTable();

                //Pass the value from SQlDataAdapter to Datatable
                adapter.Fill(dt);

                //Get the Total Number of Donors Based on Blood Group
                donors = dt.Rows.Count.ToString();

            }
            catch (Exception ex)
            {
                //Display error Message if there's any 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Datbase Connection
                conn.Close();
            }
            return donors;
        }
        #endregion

        #region Method to Search Donors
        public DataTable Search(string keywords)
        {
            //1. SQL Connection to Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //2. create Datatable to hold the Data Temporarirly
            DataTable dt = new DataTable();

            try
            {
                //Write the Code to Search Donors based on Keyboards Typed on TextBox
                //Write SQl Query to search Donors 
                string sql = "SELECT * FROM tbl_donors WHERE donor_id LIKE '%" + keywords +"%' OR first_name LIKE '%"+ keywords +"%' OR last_name LIKE '" + keywords +"' OR email LIKE '%" + keywords + "%' OR blood_group LIKE '" + keywords +"'";

                //Create SQL Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SQLDataAdapter to Save The Data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Transfer the Data from SQL Data adapter to DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Display Error Message if There's any Exceptional Errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close that Database Connection
                conn.Close();
            }
            return dt;

        }
        #endregion
    }
}
