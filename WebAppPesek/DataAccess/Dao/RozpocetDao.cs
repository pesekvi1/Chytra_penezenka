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
        public IList<Rozpocet> MeRozpocty(Uzivatel uzivatel)
        {
            return session.CreateCriteria<Rozpocet>().Add(Restrictions.Eq("Vlastnik", uzivatel)).List<Rozpocet>();
        }
    }
}
