using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class VozidloMap : ClassMap<Vozidlo>
    {
        public VozidloMap()
        {
            Table("vozdilo");
            Id(x => x.Id).Column("id").GeneratedBy.Native();
            Map(x => x.Nazev).Not.Nullable();
            Map(x => x.Spz).Column("spz").CustomSqlType("varchar(15)").Not.Nullable();
            Map(x => x.RokVyroby).Column("rok_vyroby").Not.Nullable();
            Map(x => x.PlatnostSTK).Not.Nullable();
            References(x => x.Vlastnik).Not.Nullable();
            HasMany(x => x.ServisniZaznamy).ForeignKeyConstraintName("FK_vozidlo_polozky_id").Cascade.AllDeleteOrphan();
        }
    }
}
