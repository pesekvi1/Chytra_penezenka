using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class ServisniZaznam : IEntity
    {
        public virtual int Id { get; set; }

        public virtual Vozidlo Vozidlo { get; set; }

        [Required(ErrorMessage = "Popis servisního zásahu je vyžadován")]
        public virtual string Ucel { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Cena je vyžadována")]
        [RegularExpression(@"\b\d[\d,]*\b", ErrorMessage = "Desetinné číslo musí obsahovat čárku, ne tečku.")]
        public virtual decimal Cena { get; set; }
    }
}
