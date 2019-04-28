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

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime PlatnyOd { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime PlatnyDo { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Velikost je vyžadována")]
        [RegularExpression(@"\b\d[\d,]*\b", ErrorMessage = "Desetinné číslo musí obsahovat čárku, ne tečku.")]
        public virtual double Velikost { get; set; }

        public virtual IList<PolozkaRozpoctu> Polozky { get; set; }
    }
}
