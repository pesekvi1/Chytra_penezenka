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
    public class VozidloController : BaseController
    {
        // GET: Vozidlo
        public ActionResult Index(int? strana)
        {
            VozidloDao vozidloDao = new VozidloDao();

            int page = strana != null && strana.HasValue ? strana.Value : 1;
            int totalItems;

            IList<Vozidlo> vozidla = vozidloDao.GetVozidlaPaged(LoggedUser, ItemsOnPage, page, out totalItems);

            ViewBag.Pages = (int)Math.Ceiling((double)totalItems / (double)ItemsOnPage);
            ViewBag.CurrentPage = page;

            string[] pole = Utils.zvalidujStk(vozidla, DaysToExpire);
            ViewBag.Active = pole;
            return View(vozidla);
        }

        [Authorize(Roles = "admin")]
        public ActionResult VozidlaProMeUzivatele(int? strana)
        {
            VozidloDao vozidloDao = new VozidloDao();

            int page = strana != null && strana.HasValue ? strana.Value : 1;
            int totalItems;

            UzivatelDao uzivatelDao = new UzivatelDao();

            IList<Vozidlo> vozidla = vozidloDao.GetUsersVozidlaForAdmin(LoggedUser, ItemsOnPage, page, out totalItems);
            
            ViewBag.Pages = (int)Math.Ceiling((double)totalItems / (double)ItemsOnPage);
            ViewBag.CurrentPage = page;

            string[] pole = Utils.zvalidujStk(vozidla, DaysToExpire);
            ViewBag.Active = pole;

            ViewBag.Admin = true;

            return View(vozidla);
        }

        public ActionResult PridatVozidlo()
        {
            return View();
        }

        public ActionResult Vytvorit(Vozidlo vozidlo, int rok)
        {
            DateTime rokvyroby = new DateTime(rok, 01, 01);
            vozidlo.RokVyroby = rokvyroby;
            vozidlo.Vlastnik = LoggedUser;
            if (ModelState.IsValid)
            {
                VozidloDao vozidloDao = new VozidloDao();
                vozidloDao.Create(vozidlo);
                Success("Vozidlo úspěšně přidáno");
            }

            return RedirectToAction("Index", "Vozidlo");
        }

        public ActionResult Detail(int id, int? strana, bool? admin)
        {
            VozidloDao vozidloDao = new VozidloDao();
            Vozidlo vozidlo = vozidloDao.GetById(id);

            int page = strana != null && strana.HasValue ? strana.Value : 1;
            int totalItems;

            if (Utils.JeStkBlizkoKExpiraci(vozidlo.PlatnostSTK, DaysToExpire))
            {
                Error("Platnost STK končí k " + vozidlo.PlatnostSTK.ToShortDateString() + ". Zařiďte si prosím obnovení platnosti.");
            }

            ServisniZaznamDao servisniZaznamDao = new ServisniZaznamDao();
            IList<ServisniZaznam> zaznamy =
                servisniZaznamDao.GetZaznamyForVozidloPaged(vozidlo, ItemsOnPage, page, out totalItems);

            ViewBag.Pages = (int)Math.Ceiling((double)totalItems / (double)ItemsOnPage);
            ViewBag.CurrentPage = page;
            ViewBag.Zaznamy = zaznamy;

            ViewBag.VozidloId = vozidlo.Id;
            ViewBag.Naklady = Utils.SpocitejNakladyNaVozidlo(vozidlo);

            if (admin == true)
            {
                ViewBag.Admin = true;
            }

            return View(vozidlo);
        }

        public ActionResult Odstranit(int id)
        {
            VozidloDao vozidloDao = new VozidloDao();
            Vozidlo vozidlo = vozidloDao.GetById(id);

            vozidloDao.Delete(vozidlo);
            Success("Vozidlo úspěšně ostraněno");
            return RedirectToAction("Index", "Vozidlo");
        }

        public ActionResult Editace(int id)
        {
            VozidloDao vozidloDao = new VozidloDao();
            Vozidlo vozidlo = vozidloDao.GetById(id);

            return View(vozidlo);
        }

        public ActionResult ZmenaVozidla(Vozidlo vozidlo, int rok)
        {
            VozidloDao vozidloDao = new VozidloDao();
            Vozidlo stareVozidlo = vozidloDao.GetById(vozidlo.Id);
            if (stareVozidlo.RokVyroby.Year != rok)
            {
                stareVozidlo.RokVyroby = new DateTime(rok, 1, 1);
            }

            stareVozidlo.Nazev = vozidlo.Nazev;
            stareVozidlo.Spz = vozidlo.Spz;
            stareVozidlo.PlatnostSTK = vozidlo.PlatnostSTK;

            if (ModelState.IsValid)
            {
                vozidloDao.Update(stareVozidlo);
                Success("Vozidlo úspěšně změněno");
            }

            return RedirectToAction("Index", "Vozidlo");
        }
    }


}