using Sales.Contracts.Entity.Product;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetAll();
    }
}
