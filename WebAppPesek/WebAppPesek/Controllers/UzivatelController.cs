using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace WebAppPesek.Controllers
{
    public class UzivatelController : BaseController
    {
        // GET: Uzivatel
        public ActionResult Index()
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetByLogin(User.Identity.Name);
            IList<Uzivatel> uzivatele = uzivatelDao.GetUsersForAdmin(uzivatel);
            ViewBag.Uzivatele = uzivatele;
            return View();
        }

        public ActionResult PridatUzivatele()
        {
            SkupinaDao skupinaDao = new SkupinaDao();
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetByLogin(User.Identity.Name);
            IList<Skupina> skupiny = skupinaDao.getMyGroups(uzivatel);
            ViewBag.Skupiny = skupiny;

            UzivatelskaRoleDao uzivatelskaRoleDao = new UzivatelskaRoleDao();
            IList<UzivatelskaRole> role = uzivatelskaRoleDao.GetAll();
            ViewBag.Role = role;

            return View();
        }

        [HttpPost]
        public ActionResult Vytvorit(Uzivatel uzivatel, int skupinaId)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            if (uzivatelDao.DoesUsernameExists(uzivatel.Login))
            {
                TempData["error-message"] = "Uživatel s tímto uživ. jménem již existuje";
                return RedirectToAction("PridatUzivatele");
            }

            SkupinaDao skupinaDao = new SkupinaDao();
            Skupina skupina = skupinaDao.GetById(skupinaId);
            skupinaDao.CloseSession();

            UzivatelskaRoleDao uzivatelskaRoleDao = new UzivatelskaRoleDao();
            UzivatelskaRole role = uzivatelskaRoleDao.GetRoleWithName("uzivatel");
            uzivatelskaRoleDao.CloseSession();

            Uzivatel admin = uzivatelDao.GetByLogin(User.Identity.Name);
            uzivatel.Skupina = skupina;
            uzivatel.Vytvoril = admin;
            uzivatel.Role = role;

            if (ModelState.IsValid)
            {
                uzivatelDao.CreateWithHashedPassword(uzivatel);
                TempData["message-success"] = "Uživatel úspěšne přidán";
            }

            return RedirectToAction("Index", "Uzivatel");
        }

        public ActionResult Odstranit(int id)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetById(id);
            uzivatelDao.Delete(uzivatel);
            TempData["message-success"] = "Uživatel byl úspěšně odstraněn";
            return RedirectToAction("Index", "Uzivatel");
        }
    }
}