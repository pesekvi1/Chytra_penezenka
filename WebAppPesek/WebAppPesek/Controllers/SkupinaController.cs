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
            Uzivatel uzivatel = uzivatelDao.getByLogin(User.Identity.Name);
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
            Uzivatel uzivatel = uzivatelDao.getByLogin(User.Identity.Name);

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

        
        public ActionResult VytvoreniSkupiny()
        {
            return View();
        }
    }
}