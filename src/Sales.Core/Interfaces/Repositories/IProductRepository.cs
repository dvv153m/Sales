using Sales.Contracts.Entity.Product;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> AddAsync(ProductEntity entity); 

        Task<IEnumerable<ProductEntity>> GetAll();

        //Task UpdateAsync(ProductEntity entity);
    }
}
