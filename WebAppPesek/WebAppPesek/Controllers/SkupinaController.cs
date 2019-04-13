using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace WebAppPesek.Controllers
{
    [Authorize]
    public class SkupinaController : Controller
    {
        // GET: Skupina
        public ActionResult Index()
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetByLogin(User.Identity.Name);
            SkupinaDao skupinaDao = new SkupinaDao();
            //IList<Skupina> skupiny = skupinaDao.getMyGroups(uzivatel);
            IList<Skupina> skupiny = skupinaDao.getMyGroups(uzivatel);
            ViewBag.ListJePrazdny = false;

            if (skupiny.Count == 0)
            {
                ViewBag.ListJePrazdny = true;
            }

            ViewBag.MeSkupiny = skupiny;

            return View();
        }

        public ActionResult Detail(int id)
        {
            SkupinaDao skupinaDao = new SkupinaDao();
            Skupina skupina = skupinaDao.GetById(id);
            ViewBag.Uzivatele = skupina.Uzivatele;
            return View();
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
    }
}