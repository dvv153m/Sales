using Sales.Contracts.Models;

namespace Sales.Core.Interfaces.Services
{
    public interface IProductClient
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetProductByIdAsync(long productId);
    }
}
