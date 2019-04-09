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

        [Required]
        public virtual string Jmeno { get; set; }

        [Required]
        public virtual string Prijmeni { get; set; }

        [Required]
        public virtual string Login { get; set; }

        [Required]
        public virtual string Heslo { get; set; }

        public virtual UzivatelskaRole Role { get; set; }

        public virtual Skupina Skupina { get; set; }

        public virtual Uzivatel Vytvoril { get; set; }
}
}
