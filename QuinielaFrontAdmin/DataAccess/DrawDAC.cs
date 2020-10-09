using QuinielaFrontAdmin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuinielaFrontAdmin.DataAccess
{
    public class DrawDAC
    {
        public static void Upsert(DrawModel draw)
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "Draw_Upsert";
                oCmd.CommandType = CommandType.StoredProcedure;

                if (draw.Id.HasValue)
                    oCmd.Parameters.AddWithValue("@Id", draw.Id);
                if (draw.GameId.HasValue)
                    oCmd.Parameters.AddWithValue("@GameId", draw.GameId);
                if (draw.GameModeId.HasValue)
                    oCmd.Parameters.AddWithValue("@GameModeId", draw.GameModeId);
                if (draw.Date.HasValue)
                    oCmd.Parameters.AddWithValue("@Date", draw.Date);
                if (draw.Number.HasValue)
                    oCmd.Parameters.AddWithValue("@Number", draw.Number);
                if (draw.StatusId.HasValue)
                    oCmd.Parameters.AddWithValue("@StatusId", draw.StatusId);

                Int64 Id = Convert.ToInt64(oCmd.ExecuteScalar());

                oCmd.Dispose();

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

        public static List<DrawModel> Get(DrawModel draw)
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            DataTable oDT = new DataTable();
            List<DrawModel> oLst = new List<DrawModel>();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "Draw_Get";
                oCmd.CommandType = CommandType.StoredProcedure;

                if (draw.Id.HasValue)
                    oCmd.Parameters.AddWithValue("@Id", draw.Id);
                if (draw.GameId.HasValue && draw.GameId.Value > 0)
                    oCmd.Parameters.AddWithValue("@GameId", draw.GameId);
                if (draw.GameModeId.HasValue && draw.GameModeId.Value > 0)
                    oCmd.Parameters.AddWithValue("@GameModeId", draw.GameModeId);
                if (draw.DateFrom.HasValue)
                    oCmd.Parameters.AddWithValue("@DateFrom", draw.DateFrom);
                if (draw.DateTo.HasValue)
                    oCmd.Parameters.AddWithValue("@DateTo", draw.DateTo);
                if (draw.Number.HasValue)
                    oCmd.Parameters.AddWithValue("@Number", draw.Number);
                if (draw.StatusId.HasValue && draw.StatusId.Value > 0)
                    oCmd.Parameters.AddWithValue("@StatusId", draw.StatusId);

                oDT.Load(oCmd.ExecuteReader());
                oCmd.Dispose();

                if (oDT.Rows.Count > 0)
                {
                    foreach (DataRow item in oDT.Rows)
                    {
                        DrawModel oDraw = new DrawModel
                        {
                            Id = Convert.ToInt64(item["Id"].ToString()),
                            Game = new GameModel { Id = Convert.ToInt32(item["GameId"].ToString()), Name = item["GameName"].ToString()},
                            GameMode = new GameModeModel { Id = Convert.ToInt32(item["GameModeId"].ToString()), Name = item["GameModeName"].ToString() },
                            Date = Convert.ToDateTime(item["Date"].ToString()), 
                            Number = Convert.ToInt32(item["Number"].ToString()), 
                            Status = new DrawStatusModel { Id = Convert.ToInt32(item["StatusId"].ToString()), Name = item["DrawStatusName"].ToString() }
                        };
                        oLst.Add(oDraw);
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

        public static List<DrawStatusModel> StatusGet()
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            DataTable oDT = new DataTable();
            List<DrawStatusModel> oLst = new List<DrawStatusModel>();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "DrawStatus_Get";
                oCmd.CommandType = CommandType.StoredProcedure;

                oDT.Load(oCmd.ExecuteReader());
                oCmd.Dispose();

                if (oDT.Rows.Count > 0)
                {
                    foreach (DataRow item in oDT.Rows)
                    {
                        DrawStatusModel oStatus = new DrawStatusModel
                        {
                            Id = Convert.ToInt32(item["Id"].ToString()),
                            Name = item["Name"].ToString()
                        };
                        oLst.Add(oStatus);
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