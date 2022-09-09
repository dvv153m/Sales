using Sales.Contracts.Models;
using Sales.Core.Interfaces.Services;

namespace Sales.Infrastructure.Services
{
    public class ProductClient : IProductClient
    {
        private readonly IHttpProxy _httpProxy;
        private readonly string _productApiUrl;

        public ProductClient(IHttpProxy httpProxy, string productApiUrl)
        {
            _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
            _productApiUrl = productApiUrl;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _httpProxy.GetAsync<IEnumerable<ProductDto>>($"{_productApiUrl}");
        }

        public async Task<ProductDto> GetProductByIdAsync(long productId)
        {            
            return await _httpProxy.GetAsync<ProductDto>($"{_productApiUrl}/{productId}");            
        }
    }
}
