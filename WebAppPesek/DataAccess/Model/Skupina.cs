using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Skupina : IEntity
    {
        public virtual int Id { get; set; }
        public virtual Uzivatel Zakladatel { get; set; }
        public virtual string Nazev { get; set; }
        public virtual IList<Uzivatel> Uzivatele { get; set; }

    }
}
