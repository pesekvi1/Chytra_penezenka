using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Vozidlo : IEntity
    {
        public virtual int Id { get; set; }

        public virtual Uzivatel Vlastnik { get; set; }

        [Required]
        public virtual string Nazev { get; set; }

        [Required]
        public virtual string Spz { get; set; }

        [Required]
        public virtual DateTime RokVyroby { get; set; }

        [Required]
        public virtual DateTime PlatnostSTK { get; set; }

        public virtual IList<ServisniZaznam> ServisniZaznamy { get; set; }

    }
}
