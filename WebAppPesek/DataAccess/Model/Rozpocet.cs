using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class Rozpocet : IEntity
    {
        public int Id { get; set; }

        public string Nazev { get; set; }

        public DateTime PlatnyOd { get; set; }

        public DateTime PlatnyDo { get; set; }

        public List<PolozkaRozpoctu> Polozky { get; set; }
    }
}
