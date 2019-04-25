using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class PolozkaRozpoctuDao : DaoBase<PolozkaRozpoctu>
    {
        public IList<PolozkaRozpoctu> GetPolozkyForRozpocetPaged(Rozpocet rozpocet, int count, int page, out int totalItems)
        {
            totalItems = session.CreateCriteria<PolozkaRozpoctu>()
                .Add(Restrictions.Eq("Rozpocet", rozpocet))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();

            return session.CreateCriteria<PolozkaRozpoctu>()
                .Add(Restrictions.Eq("Rozpocet", rozpocet))
                .SetFirstResult((page - 1) * count).SetMaxResults(count)
                .List<PolozkaRozpoctu>();
        }
    }
}
