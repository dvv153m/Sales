using Sales.Core.Domain;
using Sales.Core.Interfaces.Services;

namespace Sales.Infrastructure.Services
{
    public class PromocodeClient : IPromocodeClient
    {
        private readonly IHttpProxy _httpProxy;
        private readonly string _promocodeApiUrl;

        public PromocodeClient(IHttpProxy httpProxy, string productApiUrl)
        {
            _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
            _promocodeApiUrl = productApiUrl;
        }

        public async Task<Promocode> GetByPromocode(string promocode)
        {
            return await _httpProxy.GetAsync<Promocode>($"{_promocodeApiUrl}/{promocode}");
        }
    }
}
