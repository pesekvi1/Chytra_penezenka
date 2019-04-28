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
        public ActionResult Index(int? strana)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetByLogin(User.Identity.Name);

            int page = strana.HasValue ? strana.Value : 1;
            int totalItems;

            IList<Uzivatel> uzivatele = uzivatelDao.GetUsersForAdminPaged(LoggedUser, ItemsOnPage, page, out totalItems);

            ViewBag.Pages = (int)Math.Ceiling((double)totalItems / (double)ItemsOnPage);
            ViewBag.CurrentPage = page;

            return View(uzivatele);
        }

        public ActionResult PridatUzivatele()
        {
            SkupinaDao skupinaDao = new SkupinaDao();
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetByLogin(User.Identity.Name);
            IList<Skupina> skupiny = skupinaDao.GetMyGroups(uzivatel);
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

        public ActionResult Editace(int id)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel uzivatel = uzivatelDao.GetById(id);

            SkupinaDao skupinaDao = new SkupinaDao();
            IList<Skupina> skupiny = skupinaDao.GetMyGroups(LoggedUser);
            ViewBag.Skupiny = skupiny;
            return View(uzivatel);
        }

        public ActionResult ZmenitUzivatele(Uzivatel uzivatel, int skupinaId)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel staryUzivatel = uzivatelDao.GetById(uzivatel.Id);

            SkupinaDao skupinaDao = new SkupinaDao();
            Skupina skupina = skupinaDao.GetById(skupinaId);
            skupinaDao.CloseSession();
            staryUzivatel.Skupina = skupina;
            staryUzivatel.Jmeno = uzivatel.Jmeno;
            staryUzivatel.Prijmeni = uzivatel.Prijmeni;

            if (ModelState.IsValid)
            {
                uzivatelDao.Update(staryUzivatel);
                TempData["message-success"] = "Uživatel byl úspěšne upraven";
            }

            return RedirectToAction("Index", "Uzivatel");
        }

        public ActionResult ZmenaHesla()
        {
            return View();
        }

        public ActionResult ZmenitHeslo(string stareHeslo, string noveHeslo, string noveHesloZnovu)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();
            Uzivatel staryUzivatel = uzivatelDao.GetById(LoggedUser.Id);
            if (Crypto.VerifyHashedPassword(LoggedUser.Heslo, stareHeslo))
            {
                if (noveHeslo == noveHesloZnovu)
                {
                    staryUzivatel.Heslo = Crypto.HashPassword(noveHeslo);
                    uzivatelDao.Update(staryUzivatel);
                    Success("Heslo úspěšně změněno");
                }
                else
                {
                    Error("Nová hesla se neshodují");
                }
            }
            else
            {
                Error("Zadali jste špatné původní heslo");
            }

            return RedirectToAction("ZmenaHesla");
        }
    }
}