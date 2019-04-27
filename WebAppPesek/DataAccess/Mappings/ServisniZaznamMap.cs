using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class ServisniZaznamMap : ClassMap<ServisniZaznam>
    {
        public ServisniZaznamMap()
        {
            Id(x => x.Id);
            Map(x => x.Ucel).Not.Nullable();
            Map(x => x.Cena).Not.Nullable();
            References(x => x.Vozidlo).ForeignKey("FK_servisniZaznam_vozidlo_id").Not.Nullable().Cascade.SaveUpdate();
        }
    }
}
