using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class PolozkaRozpoctuMap : ClassMap<PolozkaRozpoctu>
    {
        public PolozkaRozpoctuMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Cena).Not.Nullable();
            Map(x => x.Ucel).Not.Nullable();
            References(x => x.Rozpocet).ForeignKey("FK_polozkaRozpoctu_rozpocet_id").Not.Nullable();
        }
    }
}
