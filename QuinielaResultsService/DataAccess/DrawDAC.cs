using QuinielaResultsService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuinielaResultsService.DataAccess
{
    public class DrawDAC
    {
        public static void ResultQuiniUpsert(DrawResultQuiniModel drawResult)
        {
            SqlConnection oCon = new SqlConnection();
            SqlCommand oCmd = new SqlCommand();
            try
            {
                oCon.ConnectionString = ConfigurationManager.ConnectionStrings["Quiniela"].ToString();
                oCon.Open();
                oCmd.Connection = oCon;

                oCmd.CommandText = "DrawResultQuini_Upsert";
                oCmd.CommandType = CommandType.StoredProcedure;

                if (drawResult.Id.HasValue)
                    oCmd.Parameters.AddWithValue("@Id", drawResult.Id);
                if (drawResult.DrawId.HasValue)
                    oCmd.Parameters.AddWithValue("@DrawId", drawResult.DrawId);
                if (drawResult.GameModeId.HasValue)
                    oCmd.Parameters.AddWithValue("@GameModeId", drawResult.GameModeId);
                if (drawResult.Option1.HasValue)
                    oCmd.Parameters.AddWithValue("@Option1", drawResult.Option1);
                if (drawResult.Option2.HasValue)
                    oCmd.Parameters.AddWithValue("@Option2", drawResult.Option2);
                if (drawResult.Option3.HasValue)
                    oCmd.Parameters.AddWithValue("@Option3", drawResult.Option3);
                if (drawResult.Option4.HasValue)
                    oCmd.Parameters.AddWithValue("@Option4", drawResult.Option4);
                if (drawResult.Option5.HasValue)
                    oCmd.Parameters.AddWithValue("@Option5", drawResult.Option5);
                if (drawResult.Option6.HasValue)
                    oCmd.Parameters.AddWithValue("@Option6", drawResult.Option6);
                if (drawResult.Option7.HasValue)
                    oCmd.Parameters.AddWithValue("@Option7", drawResult.Option7);
                if (drawResult.Option8.HasValue)
                    oCmd.Parameters.AddWithValue("@Option8", drawResult.Option8);
                if (drawResult.Option9.HasValue)
                    oCmd.Parameters.AddWithValue("@Option9", drawResult.Option9);
                if (drawResult.Option10.HasValue)
                    oCmd.Parameters.AddWithValue("@Option10", drawResult.Option10);
                if (drawResult.Option11.HasValue)
                    oCmd.Parameters.AddWithValue("@Option11", drawResult.Option11);
                if (drawResult.Option12.HasValue)
                    oCmd.Parameters.AddWithValue("@Option12", drawResult.Option12);
                if (drawResult.Option13.HasValue)
                    oCmd.Parameters.AddWithValue("@Option13", drawResult.Option13);
                if (drawResult.Option14.HasValue)
                    oCmd.Parameters.AddWithValue("@Option14", drawResult.Option14);
                if (drawResult.Option15.HasValue)
                    oCmd.Parameters.AddWithValue("@Option15", drawResult.Option15);
                if (drawResult.Option16.HasValue)
                    oCmd.Parameters.AddWithValue("@Option16", drawResult.Option16);
                if (drawResult.Option17.HasValue)
                    oCmd.Parameters.AddWithValue("@Option17", drawResult.Option17);
                if (drawResult.Option18.HasValue)
                    oCmd.Parameters.AddWithValue("@Option18", drawResult.Option18);
                if (drawResult.EstimatedWell.HasValue)
                    oCmd.Parameters.AddWithValue("@EstimatedWell", drawResult.EstimatedWell);
                if (!String.IsNullOrEmpty(drawResult.Success6Status))
                    oCmd.Parameters.AddWithValue("@Success6Status", drawResult.Success6Status);
                if (drawResult.Success6Winners.HasValue)
                    oCmd.Parameters.AddWithValue("@Success6Winners", drawResult.Success6Winners);
                if (drawResult.Success6Amount.HasValue)
                    oCmd.Parameters.AddWithValue("@Success6Amount", drawResult.Success6Amount);
                if (!String.IsNullOrEmpty(drawResult.Success5Status))
                    oCmd.Parameters.AddWithValue("@Success5Status", drawResult.Success5Status);
                if (drawResult.Success5Winners.HasValue)
                    oCmd.Parameters.AddWithValue("@Success5Winners", drawResult.Success5Winners);
                if (drawResult.Success5Amount.HasValue)
                    oCmd.Parameters.AddWithValue("@Success5Amount", drawResult.Success5Amount);
                if (!String.IsNullOrEmpty(drawResult.Success4Status))
                    oCmd.Parameters.AddWithValue("@Success4Status", drawResult.Success4Status);
                if (drawResult.Success4Winners.HasValue)
                    oCmd.Parameters.AddWithValue("@Success4Winners", drawResult.Success4Winners);
                if (drawResult.Success4Amount.HasValue)
                    oCmd.Parameters.AddWithValue("@Success4Amount", drawResult.Success4Amount);

                oCmd.ExecuteNonQuery();

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
    }
}
