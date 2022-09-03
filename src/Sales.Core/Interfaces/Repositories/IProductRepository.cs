﻿using Sales.Contracts.Entity;
using Sales.Contracts.Entity.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<ProductEntity> GetAll();
    }
}
