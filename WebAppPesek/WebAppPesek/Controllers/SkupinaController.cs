using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace WebAppPesek.Controllers
{
    [Authorize(Roles = "admin")]
    public class SkupinaController : BaseController
    {
        public ActionResult Index(int? strana)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetByLogin(User.Identity.Name);
            SkupinaDao skupinaDao = new SkupinaDao();

            int page = strana.HasValue ? strana.Value : 1;
            int totalItems;

            IList<Skupina> skupiny = skupinaDao.GetMyGroupsPaged(LoggedUser, ItemsOnPage, page, out totalItems);

            ViewBag.Pages = (int)Math.Ceiling((double)totalItems / (double)ItemsOnPage);
            ViewBag.CurrentPage = page;

            ViewBag.ListJePrazdny = false;

            if (skupiny.Count == 0)
            {
                ViewBag.ListJePrazdny = true;
            }

            ViewBag.MeSkupiny = skupiny;

            return View();
        }

        public ActionResult Detail(int id, int? strana)
        {
            SkupinaDao skupinaDao = new SkupinaDao();
            Skupina skupina = skupinaDao.GetById(id);
            skupinaDao.CloseSession();

            int page = strana.HasValue ? strana.Value : 1;
            int totalItems;

            UzivatelDao uzivatelDao = new UzivatelDao();

            IList<Uzivatel> uzivatele = uzivatelDao.GetUsersForGroupPaged(skupina, ItemsOnPage, page, out totalItems);
            uzivatelDao.CloseSession();

            ViewBag.Pages = (int)Math.Ceiling((double)totalItems / (double)ItemsOnPage);
            ViewBag.CurrentPage = page;
            ViewBag.SkupinaId = skupina.Id;

            return View(uzivatele);
        }

        public ActionResult PridaniSkupiny(Skupina skupina)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetByLogin(User.Identity.Name);

            skupina.Zakladatel = uzivatel;
            
            if (ModelState.IsValid)
            {
                SkupinaDao skupinaDao = new SkupinaDao();
                skupinaDao.Create(skupina);
                TempData["message-success"] = "Skupina byla uspesne pridana";

                return RedirectToAction("Index", "Skupina");
            }
            else
            {
                return View("VytvoreniSkupiny", skupina);
            }
        }

        public ActionResult Editace(int id)
        {
            SkupinaDao skupinaDao = new SkupinaDao();
            Skupina skupina = skupinaDao.GetById(id);
            IList<Uzivatel> uzivatele = skupina.Uzivatele;
            ViewBag.Uzivatele = uzivatele;

            return View(skupina);
        }

        public ActionResult UpraveniSkupiny(Skupina skupina)
        {
            try
            {
                SkupinaDao skupinaDao = new SkupinaDao();

                Skupina staraSkupina = skupinaDao.GetById(skupina.Id);
                staraSkupina.Nazev = skupina.Nazev;


                skupinaDao.Update(staraSkupina);
                TempData["message-success"] = "Skupina " + skupina.Nazev + " byla úspěšně upravena";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index", "Skupina");
        }

        public ActionResult Odstranit(int id)
        {
            try
            {
                SkupinaDao skupinaDao = new SkupinaDao();
                Skupina skupina = skupinaDao.GetById(id);
                skupinaDao.Delete(skupina);
                TempData["message-success"] = "Skupina " + skupina.Nazev + " byla odstraněna";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index", "Skupina");
        }
        
        public ActionResult VytvoreniSkupiny()
        {
            return View();
        }

        public ActionResult OdstranitUzivateleZeSkupiny(int uzivatelId, int skupinaId)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetById(uzivatelId);
            uzivatel.Skupina = null;
            uzivatelDao.Update(uzivatel);
            TempData["message-success"] =
                "Uživatel " + uzivatel.Jmeno + " " + uzivatel.Prijmeni + " byl úspěšně odebrán ze skupiny";
        return RedirectToAction("Detail", new {id = skupinaId});
        }
    }
}