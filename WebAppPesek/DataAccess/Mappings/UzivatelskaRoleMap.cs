using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    class UzivatelskaRoleMap : ClassMap<UzivatelskaRole>
    {
        public UzivatelskaRoleMap()
        {
            Table("role");
            Id(x => x.Id).Column("role_id").GeneratedBy.Native().Not.Nullable();
            Map(x => x.Popis).Column("popis").Not.Nullable();
            Map(x => x.Nazev).Column("nazev").Not.Nullable();
        }
    }
}
