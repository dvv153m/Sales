using Sales.Contracts.Entity.Product;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> AddAsync(ProductEntity entity); 

        Task<IEnumerable<ProductEntity>> GetAllAsync();

        Task<ProductEntity> GetByIdAsync(long id);

        Task<IEnumerable<ProductEntity>> GetByIdsAsync(int[] ids);

        Task UpdateAsync(ProductEntity entity);

        Task DeleteAsync(int id);
    }
}
