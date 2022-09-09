using Sales.Contracts.Models;
using Sales.Contracts.Request.Order;
using Sales.Core.Exceptions;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Rules.Orders;

namespace Sales.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderService;
        private readonly OrderAddRules _orderRules;
        private readonly IProductClient _productClient;
        private readonly IPromocodeClient _promocodeClient;

        public OrderService(IOrderRepository orderRepository,
                            OrderAddRules orderRules,
                            IProductClient productClient,
                            IPromocodeClient promocodeClient)
        {
            //orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderRules = orderRules ?? throw new ArgumentNullException(nameof(orderRules));
            _productClient = productClient ?? throw new ArgumentNullException(nameof(productClient));
            _promocodeClient = promocodeClient ?? throw new ArgumentNullException(nameof(promocodeClient));
        }        

        public async Task AddProductToOrderAsync(AddProductToOrderRequest request)
        {
            //узнать есть ли такой промокод если нет OrderException
            var promocode = await _promocodeClient.GetByPromocodeAsync(request.Promocode);
            if (promocode == null)
            {
                throw new OrderException("такого промокода не существует");
            }

            //получить продукт            
            var res = await _productClient.GetProductByIdAsync(productId: request.ProductId);
        }

        public async Task<OrderDto> AddAsync(CreateOrderRequest request)
        {
            var res = await _productClient.GetProductByIdAsync(productId: 1);

            //var product =  _salesProxy.Get<Product>(url);

            //_orderRules.Handle(new OrderDto());
            await Task.Delay(200);

            return new OrderDto();
        }

        public Task<OrderDto?> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto?> GetByPromocodeId(long id)
        {
            throw new NotImplementedException();
        }

        /*private OrderEntity Map(CreateOrderRequest createOrderRequest)
        { 
            
        }*/
    }
}
