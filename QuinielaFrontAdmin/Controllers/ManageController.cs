using QuinielaFrontAdmin.DataAccess;
using QuinielaFrontAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuinielaFrontAdmin.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            try
            {
                bool authenticated = UserDAC.Login(UserName, Password);

                if (authenticated)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Usuario y/o Contraseña incorrectos";
                }
            }
            catch (Exception)
            {
                return View("Login");
            }
            return View("Login");
        }


        #region "Play"

        public ActionResult Play(PlayModel play)
        {
            try
            {
                PlayLoadCombos();

                List<PlayModel> oLst = PlayDAC.Get(play, false);

                return View(oLst);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult PlayQuini(long? Id)
        {
            PlayModel oPlay = PlayDAC.Get(new PlayModel { Id = Id }, true).FirstOrDefault();

            PlayLoadCombos(oPlay.Game.Id, oPlay.CreatedOn);

            return View(oPlay);
        }

        public ActionResult PlayLoto(long? Id)
        {
            PlayModel oPlay = PlayDAC.Get(new PlayModel { Id = Id }, true).FirstOrDefault();

            PlayLoadCombos(oPlay.Game.Id, oPlay.CreatedOn);

            return View(oPlay);
        }

        [HttpPost]
        public JsonResult StatusUpdate(PlayModel play)
        {
            play.ModifiedBy = 1;

            PlayModel playmodel = PlayDAC.Upsert(play, true);

            return Json(playmodel, JsonRequestBehavior.AllowGet);
        }

        private void PlayLoadCombos(int? GameId = null, DateTime? DateFrom = null)
        {
            List<PlayStatusModel> oStatus = PlayDAC.StatusGet();
            ViewBag.Status = oStatus;
            List<GameModel> oGames = PlayDAC.GamesGet();
            ViewBag.Games = oGames;
            if (GameId.HasValue && DateFrom.HasValue)
            {
                List<DrawModel> oDraws = DrawDAC.Get(new DrawModel { GameId = GameId, DateFrom = DateFrom });
                ViewBag.Draws = oDraws;
            }
        }

        #endregion

        #region "Customer"

        public ActionResult Customer(CustomerModel customer)
        {
            CustomerLoadCombos();

            List<CustomerModel> oLst = CustomerDAC.Get(customer);

            return View(oLst);
        }

        public ActionResult CustomerEdit(Int64? Id)
        {
            CustomerLoadCombos();

            CustomerModel oCustomer = CustomerDAC.Get(new CustomerModel { Id = Id }).FirstOrDefault();

            return View(oCustomer);
        }

        private void CustomerLoadCombos()
        {
            List<StateCityModel> oStateCities = CustomerDAC.StateCityGet();
            ViewBag.StateCities = oStateCities;
        }

        #endregion

        #region "Draw Dates"

        public ActionResult Draw(DrawModel draw)
        {
            DrawLoadCombos();

            if (!draw.DateFrom.HasValue && !draw.DateTo.HasValue)
            {
                draw.DateFrom = DateTime.Now.AddDays(-7);
                draw.DateTo = DateTime.Now.AddDays(7);
            }

            List<DrawModel> draws = DrawDAC.Get(draw);

            return View(draws);

        }

        [HttpPost]
        public JsonResult DrawUpdate(DrawModel draw)
        {
            DrawDAC.Upsert(draw);

            return Json(new DrawModel { }, JsonRequestBehavior.AllowGet);
        }

        private void DrawLoadCombos()
        {
            List<DrawStatusModel> oStatus = DrawDAC.StatusGet();
            ViewBag.Status = oStatus;
            List<GameModel> oGames = PlayDAC.GamesGet();
            ViewBag.Games = oGames;
        }

        #endregion

    }
}