using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Vozidlo : IEntity
    {
        public virtual int Id { get; set; }

        public virtual Vyrobce Znacka { get; set; }

        public virtual ModelVozidla Model { get; set; }

        public virtual string Spz { get; set; }

        public virtual DateTime RokVyroby { get; set; }

        public virtual int ObsahMotoru { get; set; }

        public virtual int PocetValcu { get; set; }
    }
}
