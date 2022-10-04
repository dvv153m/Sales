using Sales.Contracts.Request.Product;
using Sales.Core.Dto;

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
