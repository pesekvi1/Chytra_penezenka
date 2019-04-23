﻿using System;
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

        public ActionResult PridatPolozku(int rozpocetId)
        {
            /*RozpocetDao rozpocetDao = new RozpocetDao();
            Rozpocet rozpocet = rozpocetDao.GetById(rozpocetId);
            PolozkaRozpoctu polozka = new PolozkaRozpoctu {Rozpocet = rozpocet};
            */
            ViewBag.Rozpocet = rozpocetId;
            return View();
        }

        public ActionResult Vytvorit(PolozkaRozpoctu polozka, int rozpocetId)
        {
            RozpocetDao rozpocetDao = new RozpocetDao();
            Rozpocet rozpocet = rozpocetDao.GetById(rozpocetId);
            polozka.Rozpocet = rozpocet;

            PolozkaRozpoctuDao polozkaRozpoctuDao = new PolozkaRozpoctuDao();
            if (RozpocetHelper.JeRozpocetAktivni(rozpocet))
            {
                if (RozpocetHelper.JeVRozpoctuVolno(rozpocet, polozka.Cena))
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
                    Error("Položka je mimo velikost rozpočtu!!");
                }
               
            }
            else
            {
                Error("Rozpočet je již ukončen");
            }


            return RedirectToAction("Detail", "Rozpocet", new { id = polozka.Rozpocet.Id });
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