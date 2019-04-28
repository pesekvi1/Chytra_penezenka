using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccess.Dao;
using DataAccess.Model;

namespace WebAppPesek.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string login, string password)
        {
            if (Membership.ValidateUser(login, password))
            {
                FormsAuthentication.SetAuthCookie(login, false);
                return RedirectToAction("Index", "Home");
            }

            UserError("Zadali jste špatné přihlašovací údaje");
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Uzivatel uzivatel)
        {
            UzivatelDao uzivatelDao = new UzivatelDao();

            if (!uzivatelDao.DoesUsernameExists(uzivatel.Login))
            {
                UzivatelskaRoleDao uzivatelskaRoleDao = new UzivatelskaRoleDao();
                UzivatelskaRole role = uzivatelskaRoleDao.GetRoleWithName("admin");
                uzivatelskaRoleDao.CloseSession();

                uzivatel.Skupina = null;
                uzivatel.Role = role;
                uzivatel.Vytvoril = null;

                if (ModelState.IsValid)
                {
                    try
                    {
                        uzivatelDao.CreateWithHashedPassword(uzivatel);
                        Success("Uživatel byl úspěšně vytvořen");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                TempData["user-exists"] = "Uživatelské jméno již existuje";
                return RedirectToAction("Index", "Login");
            }

            return RedirectToAction("Index", "Login");
        }
    }
}