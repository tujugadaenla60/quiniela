using QuinielaResultsService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QuinielaResultsService.Helpers
{
    public class XMLUtils
    {
        public static object ReadFileFromRSS(ConstEnums.FileTypes file)
        {
            XmlDocument oXML = new XmlDocument();
            object oRes = new object();
            try
            {
                oXML.Load(ConfigurationManager.AppSettings[file.ToString()]);

                switch (file)
                {
                    case ConstEnums.FileTypes.Quini6_Tradicional:
                        oRes = MapFromXML(oXML);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oRes;
        }

        private static DrawResultQuiniModel MapFromXML(XmlDocument xml)
        {
            DrawResultQuiniModel oRes = new DrawResultQuiniModel();
            try
            {

                if (xml.DocumentElement.SelectSingleNode("juego").SelectSingleNode("dato").Attributes[0].Value.Contains("numeros"))
                {
                    XmlNode oNumeros = xml.DocumentElement.SelectSingleNode("juego").SelectSingleNode("dato");
                    oRes.Option1 = Convert.ToInt32(oNumeros.ChildNodes[0].LastChild.Value);
                    oRes.Option2 = Convert.ToInt32(oNumeros.ChildNodes[1].LastChild.Value);
                    oRes.Option3 = Convert.ToInt32(oNumeros.ChildNodes[2].LastChild.Value);
                    oRes.Option4 = Convert.ToInt32(oNumeros.ChildNodes[3].LastChild.Value);
                    oRes.Option5 = Convert.ToInt32(oNumeros.ChildNodes[4].LastChild.Value);
                    oRes.Option6 = Convert.ToInt32(oNumeros.ChildNodes[5].LastChild.Value);
                }
                if (xml.DocumentElement.SelectSingleNode("juego").SelectNodes("dato")[1].Attributes[0].Value.Contains("pozo_estimado_proxima_jugada"))
                {
                    oRes.EstimatedWell = Convert.ToDecimal(xml.DocumentElement.SelectSingleNode("juego").SelectNodes("dato")[1].FirstChild.LastChild.Value.ToString());
                }
                if (xml.DocumentElement.SelectSingleNode("juego").SelectNodes("premio")[0].Attributes[1].Value.Contains("6 aciertos"))
                {
                    XmlNode oPremio6Aciertos = xml.DocumentElement.SelectSingleNode("juego").SelectNodes("premio")[0];
                    oRes.Success6Status = oPremio6Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString().Contains("VACANTE") ? "VACANTE" : oPremio6Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString() + " GANADORES";
                    oRes.Success6Winners = Convert.ToInt32(!oPremio6Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString().Contains("VACANTE") ? oPremio6Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString() : "0");
                    oRes.Success6Amount = Convert.ToInt32(oPremio6Aciertos.SelectSingleNode("importe").FirstChild.Value.ToString());
                }
                if (xml.DocumentElement.SelectSingleNode("juego").SelectNodes("premio")[1].Attributes[1].Value.Contains("5 aciertos"))
                {
                    XmlNode oPremio5Aciertos = xml.DocumentElement.SelectSingleNode("juego").SelectNodes("premio")[1];
                    oRes.Success5Status = oPremio5Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString().Contains("VACANTE") ? "VACANTE" : oPremio5Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString() + " GANADORES";
                    oRes.Success5Winners = Convert.ToInt32(!oPremio5Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString().Contains("VACANTE") ? oPremio5Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString() : "0");
                    oRes.Success5Amount = Convert.ToInt32(oPremio5Aciertos.SelectSingleNode("importe").FirstChild.Value.ToString());
                }
                if (xml.DocumentElement.SelectSingleNode("juego").SelectNodes("premio")[2].Attributes[1].Value.Contains("4 aciertos"))
                {
                    XmlNode oPremio4Aciertos = xml.DocumentElement.SelectSingleNode("juego").SelectNodes("premio")[2];
                    oRes.Success4Status = oPremio4Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString().Contains("VACANTE") ? "VACANTE" : oPremio4Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString() + " GANADORES";
                    oRes.Success4Winners = Convert.ToInt32(!oPremio4Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString().Contains("VACANTE") ? oPremio4Aciertos.SelectSingleNode("ganadores").FirstChild.Value.ToString() : "0");
                    oRes.Success4Amount = Convert.ToInt32(oPremio4Aciertos.SelectSingleNode("importe").FirstChild.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oRes;
        }

    }
}
