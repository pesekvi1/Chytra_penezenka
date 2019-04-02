using System;
using System.Collections.Generic;
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

        public virtual string Jmeno { get; set; }

        public virtual string Prijmeni { get; set; }

        public virtual string Login { get; set; }

        public virtual string Heslo { get; set; }

        public virtual UzivatelskaRole Role { get; set; }
    }
}
