using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;
using NHibernate.Hql.Ast;
using NHibernate.Loader.Custom;

namespace DataAccess.Model
{
    public class PolozkaRozpoctu : IEntity
    {
        public virtual int Id { get; set; }

        //[RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Cena může obsahovat maximálně 2 desetinné čárky")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Cena je vyžadována")]
        [RegularExpression(@"\b\d[\d,]*\b", ErrorMessage = "Desetinné číslo musí obsahovat čárku, ne tečku.")]
        public virtual double Cena { get; set; }
        [Required(ErrorMessage = "Pole je vyžadováno")]
        public virtual string Ucel { get; set; }
        public virtual Rozpocet Rozpocet { get; set; }
    }
}
