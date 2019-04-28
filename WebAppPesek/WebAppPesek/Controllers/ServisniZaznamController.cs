using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace WebAppPesek.Controllers
{
    public class ServisniZaznamController : BaseController
    {
        // GET: ServisniZaznam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PridatZaznam(int vozidloId)
        {
            ViewBag.Vozidlo = vozidloId;
            return View();
        }

        public ActionResult Vytvorit(ServisniZaznam zaznam, int vozidloId)
        {
            VozidloDao vozidloDao = new VozidloDao();
            Vozidlo vozidlo = vozidloDao.GetById(vozidloId);

            if (vozidlo.Vlastnik.Login != LoggedUser.Login)
            {
                Error("Nejste vlastníkem vozidla");
                return RedirectToAction("Detail", "Vozidlo", new { id = vozidloId });
            }

            zaznam.Vozidlo = vozidlo;
            if (ModelState.IsValid)
            {
                vozidloDao.CloseSession();
                ServisniZaznamDao servisniZaznamDao = new ServisniZaznamDao();
                servisniZaznamDao.Create(zaznam);
                Success("Servisní záznam úspěšně přidán");
            }

            return RedirectToAction("Detail", "Vozidlo", new {id = vozidloId});
        }

        public ActionResult Detail(ServisniZaznam zaznam, int vozidloId)
        {
            ViewBag.VozidloId = vozidloId;
            return View(zaznam);
        }

        public ActionResult Odstranit(int id, int vozidloId)
        {
            ServisniZaznamDao servisniZaznamDao = new ServisniZaznamDao();
            ServisniZaznam servisniZaznam = servisniZaznamDao.GetById(id);
            servisniZaznamDao.Delete(servisniZaznam);

            Success("Položka úspěšně odstraněna");
            return RedirectToAction("Detail", "Vozidlo", new { id = vozidloId });
        }
    }
}