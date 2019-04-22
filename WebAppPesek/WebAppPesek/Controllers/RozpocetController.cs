using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace WebAppPesek.Controllers
{
    public class RozpocetController : BaseController
    {
        // GET: Rozpocet
        public ActionResult Index()
        {
            RozpocetDao rozpocetDao = new RozpocetDao();
            IList<Rozpocet> rozpocty = rozpocetDao.MeRozpocty(LoggedUser);
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
                RozpocetDao rozpocetDao = new RozpocetDao();
                rozpocetDao.Create(rozpocet);
                TempData["message-success"] = "Rozpočet byl úspěšně přidán";
            }

            return RedirectToAction("Index");
        }
    }
}