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
    public class RozpocetController : BaseController
    {
        // GET: Rozpocet
        public ActionResult Index()
        {
            RozpocetDao rozpocetDao = new RozpocetDao();
            IList<Rozpocet> rozpocty = rozpocetDao.MeRozpocty(LoggedUser);
            string[] pole = new string[rozpocty.Count];
            for (int i = 0; i < rozpocty.Count; i++)
            {
                if (!RozpocetHelper.JeRozpocetAktivni(rozpocty[i]))
                {
                    pole[i] = "alert-danger";
                }
                else
                {
                    pole[i] = "";
                }
            }

            ViewBag.Active = pole;
            return View(rozpocty);
        }

        public ActionResult VytvoreniRozpoctu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pridani(Rozpocet rozpocet)
        {
            rozpocet.Vlastnik = LoggedUser;
            if (ModelState.IsValid)
            {
                if (RozpocetHelper.ZvalidujCas(rozpocet.PlatnyOd, rozpocet.PlatnyDo))
                {
                    RozpocetDao rozpocetDao = new RozpocetDao();
                    rozpocetDao.Create(rozpocet);
                    TempData["message-success"] = "Rozpočet byl úspěšně přidán";
                }
                else
                {
                    TempData["error-message"] = "Platnost do musí být větší než platnost od";
                    return RedirectToAction("VytvoreniRozpoctu");
                }

            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            RozpocetDao rozpocetDao = new RozpocetDao();
            Rozpocet rozpocet = rozpocetDao.GetById(id);
            IList<PolozkaRozpoctu> polozky = rozpocet.Polozky;
            ViewBag.Zaplnenost = RozpocetHelper.VypoctiPercentRozpoctu(rozpocet);
            ViewBag.Polozky = polozky;
            ViewBag.Celkem = RozpocetHelper.SpocitejRozpocet(rozpocet);
            return View(rozpocet);
        }

        public ActionResult Editace(int id)
        {
            RozpocetDao rozpocetDao = new RozpocetDao();
            Rozpocet rozpocet = rozpocetDao.GetById(id);

            return View(rozpocet);
        }

        public ActionResult ZmenaRozpoctu(Rozpocet rozpocet)
        {
            if (RozpocetHelper.ZvalidujCas(rozpocet.PlatnyOd, rozpocet.PlatnyDo))
            {

                RozpocetDao rozpocetDao = new RozpocetDao();
                Rozpocet staryRozpocet = rozpocetDao.GetById(rozpocet.Id);

                if (RozpocetHelper.JeNovaVelikostDostacujici(staryRozpocet, rozpocet.Velikost ))
                {
                    staryRozpocet.PlatnyOd = rozpocet.PlatnyOd;
                    staryRozpocet.PlatnyDo = rozpocet.PlatnyDo;
                    staryRozpocet.Nazev = rozpocet.Nazev;
                    staryRozpocet.Velikost = rozpocet.Velikost;

                    rozpocetDao.Update(staryRozpocet);
                    Success("Rozpočet " + rozpocet.Nazev + " úspěšně upraven");
                }
                else
                {
                    Error("Nová velikost není pro již existující položky dostačující");
                }

            }
            else
            {
                TempData["error-message"] = "Platnost do musí být větší než platnost od";
                return RedirectToAction("Editace", new { id = rozpocet.Id });
            }

            return RedirectToAction("Index");
        }

        public ActionResult Odstranit(int id)
        {
            RozpocetDao rozpocetDao = new RozpocetDao();
            Rozpocet rozpocet = rozpocetDao.GetById(id);
            rozpocetDao.Delete(rozpocet);
            TempData["message-success"] = "Rozpocet byl uspesne odstranen";

            return RedirectToAction("Index");
        }
    }
}