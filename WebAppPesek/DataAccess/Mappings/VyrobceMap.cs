using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class VyrobceMap : ClassMap<Vyrobce>
    {
        public VyrobceMap()
        {
            Table("vyrobce");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Nazev).Column("nazev").Not.Nullable().CustomSqlType("varchar(100)");
            Map(x => x.Popis).Column("popis").Not.Nullable().CustomSqlType("varchar(1500)");
        }
    }
}
