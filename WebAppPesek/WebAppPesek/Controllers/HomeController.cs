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
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Uzivatel()
        {
            return View(LoggedUser);
        }

        public ActionResult Menu()
        {
            UzivatelskaRoleDao uzivatelskaRoleDao = new UzivatelskaRoleDao();
            UzivatelskaRole role = uzivatelskaRoleDao.GetById(LoggedUser.Role.Id);
            ViewBag.Role = role.Nazev;
            uzivatelskaRoleDao.CloseSession();
            return View();
        }

        public ActionResult Strankovani(int pages, int currentPage, object otherParams)
        {
            ViewBag.Pages = pages;
            ViewBag.CurrentPage = currentPage;
            if (otherParams != null)
            {
                ViewBag.OtherParams = otherParams;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}