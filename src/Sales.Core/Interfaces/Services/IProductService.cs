using Sales.Contracts.Entity.Product;
using Sales.Contracts.Models;
using Sales.Contracts.Request.Product;

namespace Sales.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDto> AddAsync(CreateProductRequest request);

        Task<IEnumerable<ProductDto>> GetAll();

        Task<IEnumerable<ProductDto>> GetByIds(int[] ids);

        Task<ProductDto?> GetById(long id);

        Task UpdateAsync(UpdateProductRequest entity);

        Task DeleteAsync(int id);
    }
}
