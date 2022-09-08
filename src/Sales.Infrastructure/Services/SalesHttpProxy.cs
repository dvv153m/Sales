﻿using Newtonsoft.Json;
using Sales.Core.Interfaces.Services;

namespace Sales.Infrastructure.Services
{
    public class SalesHttpProxy : ISalesProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;
         

        public SalesHttpProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));            
        }
        
        public async Task<TOut> GetAsync<TOut>(string url)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(responseBody);                
            }            
        }
    }
}
