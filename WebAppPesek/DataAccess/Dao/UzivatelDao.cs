using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class UzivatelDao : DaoBase<Uzivatel>
    {
        public Uzivatel getByLoginAndPassword(string login, string password)
        {
            return session.CreateCriteria<Uzivatel>()
                .Add(Restrictions.Eq("Login", login))
                .Add(Restrictions.Eq("Heslo", password))
                .UniqueResult<Uzivatel>();
        }

        public Uzivatel getByLogin(string login)
        {
            return session.CreateCriteria<Uzivatel>()
                .Add(Restrictions.Eq("Login", login))
                .UniqueResult<Uzivatel>();
        }

        public IList<Uzivatel> GetUsersForAdmin(Uzivatel vytvoril)
        {
            return session.CreateCriteria<Uzivatel>().Add(Restrictions.Eq("Vytvoril", vytvoril)).List<Uzivatel>();
        }
    }
}
