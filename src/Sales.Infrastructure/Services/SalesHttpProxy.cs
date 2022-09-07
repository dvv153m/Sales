using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Services;
using System.Reflection;


namespace Sales.Infrastructure.Services
{
    public class SalesHttpProxy : ISalesProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly OrderApiOptions _config; 

        public SalesHttpProxy(IHttpClientFactory httpClientFactory, IOptions<OrderApiOptions> config)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            if (config == null || config.Value == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Value;
        }

        //public Task<TOut> Get<TOut>(string uri)
        public async Task<string> Get(string paramsUri)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync($"{_config.ProductApiUrl}/{paramsUri}");
            }

            //_config.ProductApiUrl = https://localhost:7033/api/v1
            //paramsUri = products/1

            return "";
        }
    }
}
