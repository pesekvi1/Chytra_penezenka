using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class RozpocetDao : DaoBase<Rozpocet>
    {
        public IList<Rozpocet> MeRozpoctyPaged(Uzivatel uzivatel, int count, int page, out int totalItems)
        {
            totalItems = session.CreateCriteria<Rozpocet>()
                .Add(Restrictions.Eq("Vlastnik", uzivatel))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();

            return session.CreateCriteria<Rozpocet>()
                .Add(Restrictions.Eq("Vlastnik", uzivatel))
                .SetFirstResult((page - 1) * count).SetMaxResults(count)
                .List<Rozpocet>();
        }
    }
}
