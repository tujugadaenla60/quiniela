using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuinielaFrontAdmin.DataAccess
{
    public class UserDAC
    {
        public static bool Login(string UserName, string Password)
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            DataTable oDT = new DataTable();
            bool res = false;
            try
            {
                oCon.ConnectionString = ConfigurationManager.AppSettings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "User_Login";
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.AddWithValue("@Email", UserName);
                oCmd.Parameters.AddWithValue("@Password", Password);

                oDT.Load(oCmd.ExecuteReader());
                oCmd.Dispose();

                if (oDT.Rows.Count > 0)
                {
                    res = true;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oCon.Close();
                oCon.Dispose();
            }

            return res;

        }


    }
}