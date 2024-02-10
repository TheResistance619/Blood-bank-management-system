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
    class loginDAL
    {
        //Create Static String to Connect Database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public bool loginCheck(loginBLL l)
    {
        //Create a Boolean variable and Set its default value to false
        bool isSuccess = false;

        //Connecting Database
        SqlConnection conn = new SqlConnection(myconnstrng);

        try
        {
            //SQl Query to Check Login Based on Username and Password
            string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password";

            //Create SQL Command to Pass the Value to SQL Query
            SqlCommand cmd = new SqlCommand(sql, conn);

            //Pass the value to SQL Query Using Parameters
            cmd.Parameters.AddWithValue("@username", l.username);
            cmd.Parameters.AddWithValue("@password" ,l.password);

            //SQl Data Adapter to get the Data from database
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            //DataTable to Hold the Data from Database temporarily
            DataTable dt = new DataTable();

            //Fill the data from adapter to dt
            adapter.Fill(dt);

            //Check whether user exits or not
            if (dt.Rows.Count > 0)
            {
                //User Exists and Login Succesfully
                isSuccess = true;
            }
            else
            {
                //Login Failed 
                isSuccess = false;
            }
        }
        catch (Exception ex)
        {
            //Display Error Message if There's any Exceptional Errors
            MessageBox.Show(ex.Message);
        }
        finally
        {
            //Close Database Connection
            conn.Close();
        }

        
        return isSuccess;
        }
    }  
}
