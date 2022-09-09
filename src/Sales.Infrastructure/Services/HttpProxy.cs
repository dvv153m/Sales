using Newtonsoft.Json;
using Sales.Core.Interfaces.Services;
using System.Text;

namespace Sales.Infrastructure.Services
{
    public class HttpProxy : IHttpProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;
         
        public HttpProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));            
        }

        public async Task<TOut> GetAsync<TOut>(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();      
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(responseBody);
            }
            return default(TOut);
        }

        public async Task<TOut> PostAsync<TIn, TOut>(TIn input, string url)
        {
            var jsonInput = JsonConvert.SerializeObject(input);
            var stringContent = new StringContent(jsonInput, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsync(url, stringContent);
            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(responseBody);
            }
            return default(TOut);
        }

        public async Task<TOut> PostAsync<TOut>(string url)
        {            
            var stringContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsync(url, stringContent);
            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(responseBody);
            }
            return default(TOut);
        }
    }
}
