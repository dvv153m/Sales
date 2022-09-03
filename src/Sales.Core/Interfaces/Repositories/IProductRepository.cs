using Sales.Contracts.Entity.Product;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(ProductEntity entity); 

        Task<IEnumerable<ProductEntity>> GetAll();
    }
}
