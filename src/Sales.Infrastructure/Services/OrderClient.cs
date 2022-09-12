using Sales.Contracts.Request.Order;
using Sales.Core.Interfaces.Services;

namespace Sales.Infrastructure.Services
{
    public class OrderClient : IOrderClient
    {
        private readonly IHttpProxy _httpProxy;
        private readonly string _orderApiUrl;

        public OrderClient(IHttpProxy httpProxy, string orderApiUrl)
        {
            _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
            _orderApiUrl = orderApiUrl;
        }

        public async Task AddProductToOrder(string promocode, long productId, int quantity)
        {
            AddProductToOrderRequest request = new AddProductToOrderRequest
            {
                Promocode = promocode,
                ProductId = productId,
                Quantity = quantity
            };

            await _httpProxy.PostAsync(request, $"{_orderApiUrl}/product");
        }

        public async Task DeleteFromCart(string promocode, long productId)
        {
            DeleteProductFromOrderRequest request = new DeleteProductFromOrderRequest
            {
                Promocode = promocode,
                ProductId = productId
            };

            await _httpProxy.DeleteAsync(request, $"{_orderApiUrl}/product");
        }
    }
}
