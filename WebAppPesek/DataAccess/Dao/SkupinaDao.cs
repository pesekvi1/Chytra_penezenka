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
        public IList<Skupina> getMyGroups(Uzivatel Zakladatel)
        {
            return session.CreateCriteria<Skupina>().Add(Restrictions.Eq("Zakladatel", Zakladatel)).List<Skupina>();
        }
    }
}
