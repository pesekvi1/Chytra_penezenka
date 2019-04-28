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
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Ucel).Not.Nullable();
            Map(x => x.Cena).Not.Nullable();
            References(x => x.Vozidlo).ForeignKey("FK_vozidlo_polozky_id").Nullable().Cascade.SaveUpdate();
        }
    }
}
