using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;
using NHibernate.Loader.Custom;

namespace DataAccess.Model
{
    public class PolozkaRozpoctu : IEntity
    {
        public virtual int Id { get; set; }
        public virtual double Cena { get; set; }
        public virtual string Ucel { get; set; }
        public virtual Rozpocet Rozpocet { get; set; }
    }
}
