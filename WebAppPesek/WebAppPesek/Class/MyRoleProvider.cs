﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DataAccess.Dao;
using DataAccess.Model;

namespace WebAppPesek.Class
{
    public class MyRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            UzivatelDao userDao = new UzivatelDao();
            Uzivatel user = userDao.GetByLogin(username);

            if (user == null)
            {
                return false;
            }

            return user.Role.Nazev == roleName;
        }

        public override string[] GetRolesForUser(string username)
        {
            UzivatelDao userDao = new UzivatelDao();
            Uzivatel user = userDao.GetByLogin(username);
            if (user == null)
            {
                return new string[] { };
            }

            return new string[] { user.Role.Nazev };

        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}