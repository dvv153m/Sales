using Sales.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Services
{
    public interface IProductClient
    {
        Task<ProductDto> GetProductV1(long productId);
    }
}
