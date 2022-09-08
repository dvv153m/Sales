using Sales.Contracts.Models;
using Sales.Core.Interfaces.Services;

namespace Sales.Infrastructure.Services
{
    public class ProductClient : IProductClient
    {
        private readonly IHttpProxy _salesProxy;
        private readonly string _productApiUrl;

        public ProductClient(IHttpProxy saleProxy, string productApiUrl)
        {
            _salesProxy = saleProxy ?? throw new ArgumentNullException(nameof(saleProxy));
            _productApiUrl = productApiUrl;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _salesProxy.GetAsync<IEnumerable<ProductDto>>($"{_productApiUrl}/products");
        }

        public async Task<ProductDto> GetProductByIdAsync(long productId)
        {            
            return await _salesProxy.GetAsync<ProductDto>($"{_productApiUrl}/products/{productId}");            
        }
    }
}
