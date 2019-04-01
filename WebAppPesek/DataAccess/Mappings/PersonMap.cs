using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table("person");
            Id(x => x.Id).Column("id").GeneratedBy.Native();
            Map(x => x.Name).Column("name").Not.Nullable();
            Map(x => x.Surname).Column("surname").Not.Nullable();
            Map(x => x.Age).Column("age").Not.Nullable();
            Map(x => x.Nationality).Column("nationality").Not.Nullable();
        }
    }
}
