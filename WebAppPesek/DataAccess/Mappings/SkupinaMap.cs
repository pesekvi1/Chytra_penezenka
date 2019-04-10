using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class SkupinaMap : ClassMap<Skupina>
    {
        public SkupinaMap()
        {
            Table("skupina");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Nazev).Not.Nullable();
            References(x => x.Zakladatel).Not.Nullable();
            HasMany(x => x.Uzivatele).Cascade.SaveUpdate();
        }
    }
}
