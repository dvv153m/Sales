using Sales.Contracts.Entity.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDetailEntity> GetAll();
    }
}
