using QuinielaFrontAdmin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace QuinielaFrontAdmin.DataAccess
{
    public class CustomerDAC
    {
        public static CustomerModel Create(CustomerModel customer)
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            CustomerModel oCustomer = new CustomerModel();
            Int64 CustomerId;
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "Customer_Upsert";
                oCmd.CommandType = CommandType.StoredProcedure;

                if (customer.Id.HasValue)
                    oCmd.Parameters.AddWithValue("@Id", customer.Id);
                if (!String.IsNullOrEmpty(customer.Email))
                    oCmd.Parameters.AddWithValue("@Email", customer.Email.Trim());
                if (!String.IsNullOrEmpty(customer.Password))
                    oCmd.Parameters.AddWithValue("@Password", customer.Password.Trim());
                if (!String.IsNullOrEmpty(customer.Name))
                    oCmd.Parameters.AddWithValue("@Name", customer.Name);
                if (!String.IsNullOrEmpty(customer.IdentificationNumber))
                    oCmd.Parameters.AddWithValue("@IdentificationNumber", customer.IdentificationNumber);
                if (!String.IsNullOrEmpty(customer.PhoneNumber))
                    oCmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                if (customer.StateId.HasValue)
                    oCmd.Parameters.AddWithValue("@StateId", customer.StateId);
                if (customer.StateCityId.HasValue)
                    oCmd.Parameters.AddWithValue("@StateCityId", customer.StateCityId);

                CustomerId = Convert.ToInt64(oCmd.ExecuteScalar());

                oCustomer = Get(new CustomerModel { Id = CustomerId }).FirstOrDefault();

                return oCustomer;
                
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
        }

        public static List<CustomerModel> Get(CustomerModel customer)
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            DataTable oDT = new DataTable();
            List<CustomerModel> oLst = new List<CustomerModel>();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "Customer_Get";
                oCmd.CommandType = CommandType.StoredProcedure;

                if (customer.Id.HasValue)
                    oCmd.Parameters.AddWithValue("@Id", customer.Id);
                if (!String.IsNullOrEmpty(customer.Name))
                    oCmd.Parameters.AddWithValue("@Name", customer.Name);
                if (!String.IsNullOrEmpty(customer.IdentificationNumber))
                    oCmd.Parameters.AddWithValue("@IdentificationNumber", customer.IdentificationNumber);
                if (!String.IsNullOrEmpty(customer.PhoneNumber))
                    oCmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                if (customer.StateId.HasValue)
                    oCmd.Parameters.AddWithValue("@StateId", customer.StateId);
                if (customer.StateCityId.HasValue)
                    oCmd.Parameters.AddWithValue("@StateCityId", customer.StateCityId);
                if (customer.PositiveBalance.HasValue && customer.PositiveBalance.Value)
                    oCmd.Parameters.AddWithValue("@PositiveBalance", customer.PositiveBalance);

                oDT.Load(oCmd.ExecuteReader());
                oCmd.Dispose();

                if (oDT.Rows.Count > 0)
                {
                    foreach (DataRow item in oDT.Rows)
                    {
                        CustomerModel oCustomer = new CustomerModel
                        {
                            Id = Convert.ToInt64(item["Id"].ToString()),
                            Name = item["Name"].ToString(),
                            IdentificationNumber = item["IdentificationNumber"].ToString(),
                            PhoneNumber = item["PhoneNumber"].ToString(),
                            Email = item["Email"].ToString(),
                            Password = item["Password"].ToString(),
                            StateId = (!String.IsNullOrEmpty(item["StateId"].ToString())) ? Convert.ToInt32(item["StateId"].ToString()) : 0,
                            State = (!String.IsNullOrEmpty(item["StateId"].ToString())) ? new StateModel { Id = Convert.ToInt32(item["StateId"].ToString()), Name = item["StateName"].ToString() } : null,
                            StateCityId = (!String.IsNullOrEmpty(item["StateCityId"].ToString())) ? Convert.ToInt32(item["StateCityId"].ToString()) : 0,
                            StateCity = (!String.IsNullOrEmpty(item["StateCityId"].ToString())) ? new StateCityModel { Id = Convert.ToInt32(item["StateCityId"].ToString()), Name = item["StateCityName"].ToString() } : null,
                            Balance = Convert.ToDecimal(item["Balance"].ToString())
                        };
                        oLst.Add(oCustomer);
                    }
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
            return oLst;
        }

        public static List<StateModel> StateGet()
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            DataTable oDT = new DataTable();
            List<StateModel> oLst = new List<StateModel>();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "State_Get";
                oCmd.CommandType = CommandType.StoredProcedure;

                oDT.Load(oCmd.ExecuteReader());
                oCmd.Dispose();

                if (oDT.Rows.Count > 0)
                {
                    foreach (DataRow item in oDT.Rows)
                    {
                        StateModel oState = new StateModel
                        {
                            Id = Convert.ToInt32(item["Id"].ToString()),
                            Name = item["Name"].ToString()
                        };
                        oLst.Add(oState);
                    }
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
            return oLst;
        }


        public static List<StateCityModel> StateCityGet()
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            DataTable oDT = new DataTable();
            List<StateCityModel> oLst = new List<StateCityModel>();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "StateCity_Get";
                oCmd.CommandType = CommandType.StoredProcedure;

                oDT.Load(oCmd.ExecuteReader());
                oCmd.Dispose();

                if (oDT.Rows.Count > 0)
                {
                    foreach (DataRow item in oDT.Rows)
                    {
                        StateCityModel oStateCity = new StateCityModel
                        {
                            Id = Convert.ToInt32(item["Id"].ToString()),
                            StateId = Convert.ToInt32(item["StateId"].ToString()),
                            State = new StateModel { Id = Convert.ToInt32(item["StateId"].ToString()), Name = item["StateName"].ToString() },
                            Name = item["Name"].ToString()
                        };
                        oLst.Add(oStateCity);
                    }
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
            return oLst;
        }

    }
}