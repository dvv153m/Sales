using Sales.Contracts.Entity.Product;
using Sales.Contracts.Request.Product;

namespace Sales.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task AddAsync(CreateProductRequest request);

        Task<IEnumerable<ProductEntity>> GetAll();
    }
}
