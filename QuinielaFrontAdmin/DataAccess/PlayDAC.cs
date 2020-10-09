using Microsoft.Ajax.Utilities;
using QuinielaFrontAdmin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.ModelBinding;

namespace QuinielaFrontAdmin.DataAccess
{
    public class PlayDAC
    {
        public static List<PlayModel> Get(PlayModel play, bool LoadDetail)
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            DataTable oDT = new DataTable();
            List<PlayModel> oLst = new List<PlayModel>();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "Play_Get";
                oCmd.CommandType = CommandType.StoredProcedure;

                if (play.Id.HasValue)
                    oCmd.Parameters.AddWithValue("@Id", play.Id);
                if (play.CustomerId.HasValue)
                    oCmd.Parameters.AddWithValue("@CustomerId", play.CustomerId);
                if (play.StatusId.HasValue && play.StatusId.Value > 0)
                    oCmd.Parameters.AddWithValue("@StatusId", play.StatusId);
                if (play.GameId.HasValue && play.GameId.Value > 0)
                    oCmd.Parameters.AddWithValue("@GameId", play.GameId);
                if (play.DrawId.HasValue && play.DrawId.Value > 0)
                    oCmd.Parameters.AddWithValue("@DrawId", play.DrawId);

                oDT.Load(oCmd.ExecuteReader());
                oCmd.Dispose();

                if (oDT.Rows.Count > 0)
                {
                    foreach (DataRow item in oDT.Rows)
                    {
                        PlayModel oPlay = new PlayModel
                        {
                            Id = Convert.ToInt64(item["Id"].ToString()),
                            CustomerId = Convert.ToInt64(item["CustomerId"].ToString()),
                            Customer = new CustomerModel { Id = Convert.ToInt64(item["CustomerId"].ToString()), Name = item["CustomerName"].ToString() },
                            GameId = Convert.ToInt32(item["GameId"].ToString()),
                            Game = new GameModel { Id = Convert.ToInt32(item["GameId"].ToString()), Name = item["GameName"].ToString() },
                            Draw = new DrawModel { Id = Convert.ToInt64(item["DrawId"].ToString()), Date = Convert.ToDateTime(item["Date"].ToString()), Number = Convert.ToInt32(item["Number"].ToString()) },
                            GameMode = new GameModeModel { Id = Convert.ToInt32(item["ModeId"].ToString()), GameId = Convert.ToInt32(item["GameId"].ToString()), Name = item["ModeName"].ToString() },
                            Status = new PlayStatusModel { Id = Convert.ToInt32(item["StatusId"].ToString()), Name = item["StatusName"].ToString() },
                            Total = Convert.ToDecimal(item["Total"].ToString()),
                            OfficialTicket = item["OfficialTicket"].ToString(),
                            Delivered = Convert.ToBoolean( item["Delivered"].ToString()),
                            Winner = Convert.ToBoolean( item["Winner"].ToString()),
                            Prize = Convert.ToDecimal(item["Prize"].ToString()),
                            CreatedOn = Convert.ToDateTime(item["CreatedOn"].ToString())
                        };

                        if (LoadDetail)
                        {
                            switch (oPlay.GameId)
                            {
                                case 1: //Quini 6
                                    if (!String.IsNullOrEmpty(item["QuiniId"].ToString()))
                                    {
                                        oPlay.QuiniDetail = new PlayQuniModel
                                        {
                                            Id = Convert.ToInt64(item["QuiniId"].ToString()),
                                            Option1 = Convert.ToInt32(item["QuiniOption1"].ToString()),
                                            Option2 = Convert.ToInt32(item["QuiniOption2"].ToString()),
                                            Option3 = Convert.ToInt32(item["QuiniOption3"].ToString()),
                                            Option4 = Convert.ToInt32(item["QuiniOption4"].ToString()),
                                            Option5 = Convert.ToInt32(item["QuiniOption5"].ToString()),
                                            Option6 = Convert.ToInt32(item["QuiniOption6"].ToString()),
                                        };
                                    }
                                    break;
                                case 2: //Loto
                                    oPlay.LotoDetail = new PlayLotoModel
                                    {
                                        Id = Convert.ToInt64(item["LotoId"].ToString()),
                                        Option1 = Convert.ToInt32(item["LotoOption1"].ToString()),
                                        Option2 = Convert.ToInt32(item["LotoOption2"].ToString()),
                                        Option3 = Convert.ToInt32(item["LotoOption3"].ToString()),
                                        Option4 = Convert.ToInt32(item["LotoOption4"].ToString()),
                                        Option5 = Convert.ToInt32(item["LotoOption5"].ToString()),
                                        Option6 = Convert.ToInt32(item["LotoOption6"].ToString()),
                                        Jack1 = Convert.ToInt32(item["LotoJack1"].ToString()),
                                        Jack2 = Convert.ToInt32(item["LotoJack2"].ToString()),
                                    };
                                    break;
                                default:
                                    break;
                            }
                        }

                        oLst.Add(oPlay);
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

        public static PlayModel Upsert(PlayModel play, bool loadResult)
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            PlayModel oPlay = new PlayModel();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "Play_Upsert";
                oCmd.CommandType = CommandType.StoredProcedure;

                if (play.Id.HasValue)
                    oCmd.Parameters.AddWithValue("@Id", play.Id);
                if (play.CustomerId.HasValue)
                    oCmd.Parameters.AddWithValue("@CustomerId", play.CustomerId);
                if (play.GameId.HasValue)
                    oCmd.Parameters.AddWithValue("@GameId", play.GameId);
                if (play.DrawId.HasValue)
                    oCmd.Parameters.AddWithValue("@DrawId", play.DrawId);
                if (play.ModeId.HasValue)
                    oCmd.Parameters.AddWithValue("@ModeId", play.ModeId);
                if (play.StatusId.HasValue)
                    oCmd.Parameters.AddWithValue("@StatusId", play.StatusId);
                if (play.Total.HasValue)
                    oCmd.Parameters.AddWithValue("@Total", play.Total);
                if (!string.IsNullOrEmpty(play.OfficialTicket))
                    oCmd.Parameters.AddWithValue("@OfficialTicket", play.OfficialTicket);
                if (play.ModifiedBy.HasValue)
                    oCmd.Parameters.AddWithValue("@ModifiedBy", play.ModifiedBy);
                if (play.ModifiedOn.HasValue)
                    oCmd.Parameters.AddWithValue("@ModifiedOn", play.ModifiedOn);
                if (play.DeletedOn.HasValue)
                    oCmd.Parameters.AddWithValue("@DeletedOn", play.DeletedOn);

                Int64 Id = Convert.ToInt64( oCmd.ExecuteScalar());
                
                oCmd.Dispose();

                if (loadResult)
                    oPlay = Get(new PlayModel { Id = Id }, loadResult).FirstOrDefault();

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
            return oPlay;
        }

        public static List<PlayStatusModel> StatusGet()
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            DataTable oDT = new DataTable();
            List<PlayStatusModel> oLst = new List<PlayStatusModel>();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "PlayStatus_Get";
                oCmd.CommandType = CommandType.StoredProcedure;

                oDT.Load(oCmd.ExecuteReader());
                oCmd.Dispose();

                if (oDT.Rows.Count > 0)
                {
                    foreach (DataRow item in oDT.Rows)
                    {
                        PlayStatusModel oStatus = new PlayStatusModel
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

        public static List<GameModel> GamesGet()
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            DataTable oDT = new DataTable();
            List<GameModel> oLst = new List<GameModel>();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "Game_Get";
                oCmd.CommandType = CommandType.StoredProcedure;

                oDT.Load(oCmd.ExecuteReader());
                oCmd.Dispose();

                if (oDT.Rows.Count > 0)
                {
                    foreach (DataRow item in oDT.Rows)
                    {
                        GameModel oGame = new GameModel
                        {
                            Id = Convert.ToInt32(item["Id"].ToString()),
                            Name = item["Name"].ToString()
                        };
                        oLst.Add(oGame);
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