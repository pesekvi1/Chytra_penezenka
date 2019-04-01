using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Person : IEntity
    {
        public virtual int Id { get; set; }

        public virtual int Age { get; set; }

        public virtual string Name { get; set; }
        
        public virtual string Surname { get; set; }

        public virtual string Nationality { get; set; }

    }
}
