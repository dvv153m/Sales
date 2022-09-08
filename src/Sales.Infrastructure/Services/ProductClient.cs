using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using Sales.Contracts.Models;
using Sales.Core.Interfaces.Services;

namespace Sales.Infrastructure.Services
{
    public class ProductClient : IProductClient
    {
        private readonly ISalesProxy _salesProxy;
        private readonly OrderApiOptions _config;

        public ProductClient(ISalesProxy saleProxy, IOptions<OrderApiOptions> config)
        {
            _salesProxy = saleProxy ?? throw new ArgumentNullException(nameof(saleProxy));
            if (config?.Value == null) throw new ArgumentNullException(nameof(config));

            _config = config.Value;
        }

        public async Task<ProductDto> GetProductV1(long productId)
        {            
            return await _salesProxy.GetAsync<ProductDto>($"{_config.ProductApiUrl}/products/{productId}");            
        }
    }
}
