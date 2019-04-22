using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Rozpocet : IEntity
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual string Nazev { get; set; }

        public virtual Uzivatel Vlastnik { get; set; }

        public virtual DateTime PlatnyOd { get; set; }

        public virtual DateTime PlatnyDo { get; set; }

        public virtual IList<PolozkaRozpoctu> Polozky { get; set; }
    }
}
