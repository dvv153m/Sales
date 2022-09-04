using Sales.Contracts.Entity.Product;
using Sales.Contracts.Models;
using Sales.Contracts.Request.Product;

namespace Sales.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDto> AddAsync(CreateProductRequest request);

        Task<IEnumerable<ProductEntity>> GetAll();

        Task<ProductDto> GetById(long id);

        Task UpdateAsync(UpdateProductRequest entity);
    }
}
