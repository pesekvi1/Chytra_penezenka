﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;

namespace DataAccess.Model
{
    public class PolozkaRozpoctu : IEntity
    {
        public virtual int Id { get; set; }
    }
}
