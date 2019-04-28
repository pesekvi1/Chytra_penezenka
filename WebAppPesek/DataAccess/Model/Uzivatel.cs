using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Uzivatel : MembershipUser, IEntity
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Zadejte jméno")]
        public virtual string Jmeno { get; set; }

        [Required(ErrorMessage = "Zadejte příjmení")]
        public virtual string Prijmeni { get; set; }

        [Required(ErrorMessage = "Vyplňte přihlašovací jméno")]
        public virtual string Login { get; set; }

        [Required(ErrorMessage = "Zadejte heslo")]
        public virtual string Heslo { get; set; }

        public virtual UzivatelskaRole Role { get; set; }

        public virtual Skupina Skupina { get; set; }

        public virtual Uzivatel Vytvoril { get; set; }

        public virtual IList<Rozpocet> Rozpocty { get; set; }

        public virtual IList<Vozidlo> Vozidla { get; set; }
}
}
