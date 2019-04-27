using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class ServisniZaznamDao : DaoBase<ServisniZaznam>
    {
        public IList<ServisniZaznam> GetZaznamyForVozidloPaged(Vozidlo vozidlo, int count, int page, out int totalItems)
        {
            totalItems = session.CreateCriteria<ServisniZaznam>()
                .Add(Restrictions.Eq("Vozidlo", vozidlo))
                .SetProjection(Projections.RowCount()).UniqueResult<int>();

            return session.CreateCriteria<ServisniZaznam>()
                .Add(Restrictions.Eq("Vozidlo", vozidlo))
                .SetFirstResult((page - 1) * count).SetMaxResults(count)
                .List<ServisniZaznam>();
        }
    }
}
