using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class VozidloDao : DaoBase<Vozidlo>
    {
        public IList<Vozidlo> GetVozidlaPaged(Uzivatel vlastnik, int count, int page, out int totalItems)
        {
            totalItems = session.CreateCriteria<Vozidlo>()
                .Add(Restrictions.Eq("Vlastnik", vlastnik))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();

            return session.CreateCriteria<Vozidlo>()
                .Add(Restrictions.Eq("Vlastnik", vlastnik))
                .SetFirstResult((page - 1) * count).SetMaxResults(count)
                .List<Vozidlo>();
        }

        public IList<Vozidlo> GetUsersVozidlaForAdmin(Uzivatel vlastnik, int count, int page, out int totalItems)
        {
            totalItems = session.CreateCriteria<Vozidlo>()
                .CreateCriteria("Vlastnik")
                .Add(Restrictions.Eq("Vytvoril", vlastnik))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();

            return session.CreateCriteria<Vozidlo>()
                .CreateCriteria("Vlastnik")
                .Add(Restrictions.Eq("Vytvoril", vlastnik))
                .SetFirstResult((page - 1) * count).SetMaxResults(count)
                .List<Vozidlo>();
        }
    }
}
