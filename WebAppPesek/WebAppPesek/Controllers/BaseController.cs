using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace WebAppPesek.Controllers
{
    public class BaseController : Controller
    {
        private Uzivatel _loggedUser;

        protected Uzivatel LoggedUser
        {
            get
            {
                if (_loggedUser == null)
                {
                    UzivatelDao userDao = new UzivatelDao();
                    _loggedUser = userDao.GetByLogin(User.Identity.Name);
                }

                return _loggedUser;
            }
            set => _loggedUser = value;
        }

        protected int ItemsOnPage
        {
            get => 5;
        }

        protected int DaysToExpire
        {
            get => 60;
        }

        public void Success(string text)
        {
            TempData["message-success"] = text;
        }

        public void Error(string text)
        {
            TempData["error-message"] = text;
        }

        public void UserError(string text)
        {
            TempData["user-error-message"] = text;
        }
    }
}