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
        public int Id { get; set; }

        public Vyrobce Znacka { get; set; }

        public ModelVozidla Model { get; set; }

        public string Spz { get; set; }

        public DateTime RokVyroby { get; set; }

        public int ObsahMotoru { get; set; }

        public int PocetValcu { get; set; }
    }
}
