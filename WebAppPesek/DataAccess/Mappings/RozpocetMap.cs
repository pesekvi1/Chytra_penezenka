﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class RozpocetMap : ClassMap<Rozpocet>
    {
        public RozpocetMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Nazev).Not.Nullable();
            Map(x => x.PlatnyOd).Not.Nullable();
            Map(x => x.PlatnyDo).Not.Nullable();
            HasMany(x => x.Polozky).ForeignKeyConstraintName("FK_rozpocet_polozky_id").Cascade.AllDeleteOrphan();
        }
    }
}
