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
    class userDAL
    {
        //Create a static string to connect database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT data from database
        public DataTable Select()
        {
            //Create an Object to create Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create a DataTable to Hold the Data from Database
            DataTable dt = new DataTable();

            try
            {
                // Write SQl Query to get data from Database
                 String sql = "SELECT * FROM tbl_users";

                //create  SQl Command to Execute  Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Crate Sql Data Adapter to hold the data from the database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Transfer Data from sqlData Adapter to DataTable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Display Error Message if there's any exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return dt;
        }

        #endregion

        #region Insert Data into Database for user Module

        public bool Insert(userBLL u)
        {
            //Create  a boolean variable and set its default value to false
            bool isSuccess = false;

            //Create an Object of SqlConnection to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a String Variable to Store the Insert Query
                String sql = "INSERT INTO tbl_users(username, email, password, full_name, contact, address, added_date, image_name) VALUES (@username, @email, @password, @full_name, @contact, @address, @added_date, @image_name)";

                //Create a SQl command to pass the value in our query 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create the parameter to pass get the value from UI and pass it on SQl Query above
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@image_name", u.image_name);

                //Open Database Connection
                conn.Open();

                //Create an Integer Varaible to hold the value after the query is executed
                int rows = cmd.ExecuteNonQuery();

                //The Value of rows will be greater than 0 i fthe query is executed successfully
                //Else it'll be 0

                if (rows > 0)
                {
                    //Query Executed Successfully 
                    isSuccess = true;
                }
                else 
                {
                    //Failed to execute Query 
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                //Display Error Message if there's any exceptional errors
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

        #region UPDATE data in database (User Module)
        public bool Update(userBLL u)
        {
            //Create a Boolean variable and set its default value to false
            bool isSuccess = false;

            //Create an object for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a astring Variable to hold the SQl query
                string sql ="UPDATE tbl_users SET username=@username, email=@email, password=@password, full_name=@full_name, contact=@contact,  address=@address, added_date=@added_date, image_name=@image_name WHERE user_id=@user_id" ;

                //Create sql Command to execute query and also pass the values to sql query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Now Pass the values to SQl query
                cmd. Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@full_name", u.full_name);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@image_name", u.image_name);
                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                //Open DataBase Connection
                conn.Open();

                //Create an Integer variable o hold the value after the query is executed

                int rows = cmd.ExecuteNonQuery();

                //if the query is executed succesfullt then the value of rows will be greater than 0
                
                if(rows>0)
                {
                    isSuccess =  true;
                }
                else
                {
                    //Failed to Execute query
                    isSuccess =  false;
                }
            }
            catch(Exception ex)
            {
                //Display error message if there's any exceptional error 
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

        #region Delete Data from Database (User Module)
        public bool Delete(userBLL u)
        {
            //Create a bloolean variable and set its default value to false
            bool isSuccess = false;

            //Create an obkject foe SqlConnection 
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create a string variable to hold the SQl query to delete data
                String sql = "DELETE FROM tbl_users WHERE user_id=@user_id";

                //Create Sql Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Pass the value through Parameters
                cmd.Parameters.AddWithValue("@user_id", u.user_id);

                //Open the Database Connection 
                conn.Open();
 
                //Create an Integer  variable to hold the value after is executed
                int rows = cmd.ExecuteNonQuery();

                //If the query is executed Successfully than the value of rows will be greater than Zero(0)
                //Else it'll be zero(0)

                if (rows > 0)
                {
                    //Query executed Successfully
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
                //Display Error Message is there's any Exceptional errors
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

        #region SEARCH
        public DataTable Search(string keywords)
        {
            //1. Create a SQl Connection database
            SqlConnection conn = new SqlConnection(myconnstrng);

            // 2. Create Data Table to hold the data from database temporarily
            DataTable dt = new DataTable();
            
            //Write the code to search the User
            try
            {
                //Write the SQL Query  to Search the User from Database
                String sql = "SELECT * FROM tbl_users WHERE user_id LIKE '%" + keywords + "%' OR full_name LIKE '%" + keywords + "%' OR address LIKE '%" + keywords + "%'"; 

                //Create SQL command to Execute the Query 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create SQL Data Adapter to Get The Data frrom Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database Connection
                conn.Open();

                //Pass the Data from adapter to DataTable
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                //Display Error Messages if there's amy exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion

        #region
        public userBLL GetIDFromUsername(string username)
        {
            userBLL u = new userBLL();

            //Create SQl Connection to Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //DataTable to hold the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //SQL Query to get ID from USERNAME
                string sql = "SELECT user_id FROM tbl_users WHERE username='"+"'";

                //Create SQl Data Adapter
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //Open Database Copnnection
                conn.Open();

                //Fill the data in DataTable from Adapter 
                adapter.Fill(dt);

                //If there's user based on the username then get the user_id
                if (dt.Rows.Count > 0)
                {
                    u.user_id = int.Parse(dt.Rows[0]["user_id"].ToString());
                }
            }

            catch (Exception ex)
            {
                //Display Error Message if There's any Exceptional errors
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

            return u;
        }
        #endregion

    }
}
