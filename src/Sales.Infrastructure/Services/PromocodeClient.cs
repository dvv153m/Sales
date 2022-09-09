using Sales.Core.Domain;
using Sales.Core.Interfaces.Services;

namespace Sales.Infrastructure.Services
{
    public class PromocodeClient : IPromocodeClient
    {
        private readonly IHttpProxy _httpProxy;
        private readonly string _promocodeApiUrl;

        public PromocodeClient(IHttpProxy httpProxy, string promocodeApiUrl)
        {
            _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
            _promocodeApiUrl = promocodeApiUrl;
        }

        public async Task<Promocode> GetByPromocodeAsync(string promocode)
        {
            return await _httpProxy.GetAsync<Promocode>($"{_promocodeApiUrl}/{promocode}");
        }

        public async Task<Promocode> GeneratePromocodeAsync()
        {
            return await _httpProxy.PostAsync<Promocode>($"{_promocodeApiUrl}");
        }
    }
}
