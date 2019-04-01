using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class ModelVozidlaMap : ClassMap<Model.ModelVozidla>
    {
        public ModelVozidlaMap()
        {
            Table("model_vozidla");
            Table("model_vozidla");
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Nazev).Not.Nullable().CustomSqlType("varchar(55)");
            References(x => x.Vyrobce).Column("vyrobce").Not.Nullable();
        }
    }
}
