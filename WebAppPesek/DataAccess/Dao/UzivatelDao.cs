using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class UzivatelDao : DaoBase<Uzivatel>
    {
        public Uzivatel GetByLoginAndPassword(string login, string password)
        {
            Uzivatel uzivatel = GetByLogin(login);
            if (uzivatel != null)
            {
                if (Crypto.VerifyHashedPassword(uzivatel.Heslo, password))
                {
                    return uzivatel;
                }
            }
            

            return null;
        }

        public void CreateWithHashedPassword(Uzivatel uzivatel)
        {
            string sifrovaneHeslo = Crypto.HashPassword(uzivatel.Heslo);
            uzivatel.Heslo = sifrovaneHeslo;
            Create(uzivatel);
        }

        public Uzivatel GetByLogin(string login)
        {
            return session.CreateCriteria<Uzivatel>()
                .Add(Restrictions.Eq("Login", login))
                .UniqueResult<Uzivatel>();
        }

        public IList<Uzivatel> GetUsersForAdmin(Uzivatel vytvoril)
        {
            return session.CreateCriteria<Uzivatel>().Add(Restrictions.Eq("Vytvoril", vytvoril)).List<Uzivatel>();
        }

        public IList<Uzivatel> GetUsersForAdminPaged(Uzivatel vytvoril, int count, int page, out int totalItems)
        {
            totalItems = session.CreateCriteria<Uzivatel>()
                .Add(Restrictions.Eq("Vytvoril", vytvoril))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();

            return session.CreateCriteria<Uzivatel>()
                .Add(Restrictions.Eq("Vytvoril", vytvoril))
                .SetFirstResult((page - 1) * count).SetMaxResults(count)
                .List<Uzivatel>();
        }

        public IList<Uzivatel> GetUsersForGroupPaged(Skupina skupina, int count, int page, out int totalItems)
        {
            totalItems = session.CreateCriteria<Uzivatel>()
                .Add(Restrictions.Eq("Skupina", skupina))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();

            return session.CreateCriteria<Uzivatel>()
                .Add(Restrictions.Eq("Skupina", skupina))
                .SetFirstResult((page - 1) * count).SetMaxResults(count)
                .List<Uzivatel>();
        }

        public bool DoesUsernameExists(string username)
        {
            IList<Uzivatel> uzivatele = new UzivatelDao().GetAll();
            foreach (var uzivatel in uzivatele)
            {
                if (uzivatel.Login == username)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
