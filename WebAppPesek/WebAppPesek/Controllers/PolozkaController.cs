using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;
using WebAppPesek.Class;

namespace WebAppPesek.Controllers
{
    public class PolozkaController : BaseController
    {
        // GET: Polozka
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail (PolozkaRozpoctu polozka, int rozpocetId)
        {
            if (polozka != null)
            {
                ViewBag.RozpocetId = rozpocetId;
                return View(polozka);
            }

            return null;
        }

        public ActionResult PridatPolozku(int rozpocetId)
        {
            ViewBag.Rozpocet = rozpocetId;
            return View();
        }

        public ActionResult Vytvorit(PolozkaRozpoctu polozka, int rozpocetId)
        {
            RozpocetDao rozpocetDao = new RozpocetDao();
            Rozpocet rozpocet = rozpocetDao.GetById(rozpocetId);

            if (rozpocet.Vlastnik.Login != LoggedUser.Login)
            {
                Error("Nejste vlastníkem rozpočtu");
                return RedirectToAction("Detail", "Rozpocet", new { id = rozpocet.Id});
            }

            polozka.Rozpocet = rozpocet;

            PolozkaRozpoctuDao polozkaRozpoctuDao = new PolozkaRozpoctuDao();
            if (Utils.JeRozpocetAktivni(rozpocet))
            {
                if (Utils.JeVRozpoctuVolno(rozpocet, polozka.Cena))
                {
                    if (ModelState.IsValid)
                    {
                        rozpocetDao.CloseSession();
                        polozkaRozpoctuDao.Create(polozka);
                        Success("Polozka uspesne pridana");
                    }
                }
                else
                {
                    rozpocetDao.CloseSession();
                    polozkaRozpoctuDao.Create(polozka);
                }
               
            }
            else
            {
                Error("Rozpočet je již ukončen");
            }


            return RedirectToAction("Detail", "Rozpocet", new { id = rozpocet.Id });
        }

        public ActionResult Odstranit(int id, int rozpocetId)
        {
            PolozkaRozpoctuDao polozkaRozpoctuDao = new PolozkaRozpoctuDao();
            PolozkaRozpoctu polozka = polozkaRozpoctuDao.GetById(id);
            polozkaRozpoctuDao.Delete(polozka);

            Success("Polozka uspesne odstranena");
            return RedirectToAction("Detail", "Rozpocet", new { id = rozpocetId });
        }
    }
}