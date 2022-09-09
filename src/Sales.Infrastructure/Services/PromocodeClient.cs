using Sales.Core.Interfaces.Services;

namespace Sales.Infrastructure.Services
{
    public class PromocodeClient : IPromocodeClient
    {
        private readonly IHttpProxy _httpProxy;
        private readonly string _productApiUrl;

        public PromocodeClient(IHttpProxy httpProxy, string productApiUrl)
        {
            _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
            _productApiUrl = productApiUrl;
        }

        public Task<bool> IsExist(string promocode)
        {
            throw new NotImplementedException();
        }
    }
}
