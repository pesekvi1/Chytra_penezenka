using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class SkupinaDao : DaoBase<Skupina>
    {
        public IList<Skupina> GetMyGroupsPaged(Uzivatel Zakladatel, int count, int page, out int totalItems)
        {
            totalItems = session.CreateCriteria<Skupina>()
                .Add(Restrictions.Eq("Zakladatel", Zakladatel))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();

            return session.CreateCriteria<Skupina>()
                .Add(Restrictions.Eq("Zakladatel", Zakladatel))
                .SetFirstResult((page - 1) * count).SetMaxResults(count)
                .List<Skupina>();
        }

        public IList<Skupina> GetMyGroups(Uzivatel Zakladatel)
        {
            return session.CreateCriteria<Skupina>()
                .Add(Restrictions.Eq("Zakladatel", Zakladatel))
                .List<Skupina>();
        }
    }
}
