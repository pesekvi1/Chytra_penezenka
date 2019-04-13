using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using NHibernate.Criterion;

namespace DataAccess.Dao
{
    public class UzivatelskaRoleDao : DaoBase<UzivatelskaRole>
    {
        public UzivatelskaRole GetRoleWithName(string SkupinaName)
        {
            return session.CreateCriteria<UzivatelskaRole>().Add(Restrictions.Eq("Nazev", SkupinaName)).UniqueResult<UzivatelskaRole>();
        }
    }
}
