﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Entity.Base
{
    public class EntityBase
    {
        public long Id { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
