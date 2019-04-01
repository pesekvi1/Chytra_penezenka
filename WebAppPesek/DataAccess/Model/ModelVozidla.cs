﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class ModelVozidla : IEntity
    {
        public int Id { get; set; }

        public string Nazev { get; set; }

        public Vyrobce Vyrobce { get; set; }
    }
}
