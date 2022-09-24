using Sales.Contracts.Entity.Product;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> AddAsync(ProductEntity entity); 

        Task<IEnumerable<ProductEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<ProductEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductEntity>> GetByIdsAsync(int[] ids);

        Task UpdateAsync(ProductEntity entity);

        Task DeleteAsync(int id);
    }
}
