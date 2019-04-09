using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    class UzivatelMap : ClassMap<Uzivatel>
    {
        public UzivatelMap()
        {
            Not.LazyLoad();

            Table("uzivatel");
            Id(x => x.Id).Column("user_id").GeneratedBy.Native().Not.Nullable();
            Map(x => x.Jmeno).Column("jmeno").Not.Nullable();
            Map(x => x.Prijmeni).Column("prijmeni");
            Map(x => x.Login).Column("login");
            Map(x => x.Heslo).Column("heslo");
            References(x => x.Role).ForeignKey("FK_uzivatel_uzivatel_role_id").Column("role_id");
            References(x => x.Skupina).ForeignKey("FK_uzivatel_skupina_id").Nullable().Cascade.SaveUpdate();
            References(x => x.Vytvoril).ForeignKey("FK_uzivatel_uzivatel_id").Nullable();
        }
    }
}
