using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sales.Contracts.Configuration;
using Sales.Core.Interfaces.Services;

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
        
        public async Task<TOut> GetAsync<TOut>(string paramsUri)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync($"{_config.ProductApiUrl}/{paramsUri}");
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(responseBody);                
            }            
        }
    }
}
