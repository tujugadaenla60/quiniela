using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using System.Xml;
using QuinielaResultsService.Models;
using QuinielaResultsService.DataAccess;

namespace QuinielaResultsService
{
    public partial class MainService : ServiceBase
    {
        Timer timer = new Timer();
        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["Intervalo"].ToString()) * 6000;
            timer.Elapsed += new ElapsedEventHandler(VerificarTareasPendientes);
            timer.Start();
        }

        protected override void OnStop()
        {
        }

        private void VerificarTareasPendientes(object sender, ElapsedEventArgs args)
        {
            timer.Stop();
            try
            {
                //DataAccess.SystemLoggerDAC.Create(0, "Agent Service", "Verificando Objetivos...", DateTime.Now.ToString(), "");
                object oMapped = Helpers.XMLUtils.ReadFileFromRSS(Helpers.ConstEnums.FileTypes.Quini6_Tradicional);

                DrawDAC.ResultQuiniUpsert((DrawResultQuiniModel)oMapped);

            }
            catch (Exception ex)
            {
                //DataAccess.SystemLoggerDAC.Create(0, "Agent Service", "VerificarTareasPendientes", string.Empty, ex.StackTrace);
                throw ex;
            }
            timer.Start();
        }

    }
}
