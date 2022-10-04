﻿
using Sales.Core.Dto;

namespace Sales.Core.Interfaces.Clients
{
    public interface IProductClient
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetProductByIdAsync(long productId);
    }
}
