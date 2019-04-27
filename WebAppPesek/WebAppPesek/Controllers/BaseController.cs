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
                    UzivatelDao uzivatelDao = new UzivatelDao();
                    _loggedUser = uzivatelDao.GetByLogin(User.Identity.Name);
                }
                return _loggedUser;
            }
            set => _loggedUser = value;
        }

        protected int ItemsOnPage
        {
            get => 5;
        }

        protected int MontsToExpire
        {
            get => 3;
        }

        public void Success(string text)
        {
            TempData["message-success"] = text;
        }

        public void Error(string text)
        {
            TempData["error-message"] = text;
        }
    }
}