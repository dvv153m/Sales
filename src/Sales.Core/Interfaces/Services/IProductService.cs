using Sales.Contracts.Entity.Product;

namespace Sales.Core.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<ProductEntity> GetAll();
    }
}
