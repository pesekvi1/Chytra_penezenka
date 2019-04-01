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
            References(x => x.Znacka).Column("znacka").Not.Nullable();
            References(x => x.Model).Column("model").Not.Nullable();
            Map(x => x.ObsahMotoru).Column("obsah_motoru").Not.Nullable();
            Map(x => x.PocetValcu).Column("pocet_valcu").Not.Nullable();
            Map(x => x.Spz).Column("spz").CustomSqlType("varchar(10)").Not.Nullable();
            Map(x => x.RokVyroby).Column("rok_vyroby").Not.Nullable();
        }
    }
}
