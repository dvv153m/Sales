using Sales.Contracts.Models;
using Sales.Contracts.Request.Order;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Rules.Orders;

namespace Sales.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderService;
        private readonly OrderAddRules _orderRules;
        private readonly ISalesProxy _salesProxy;

        public OrderService(IOrderRepository orderRepository,
                            OrderAddRules orderRules,
                            ISalesProxy salesProxy)
        {
            //orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderRules = orderRules ?? throw new ArgumentNullException(nameof(orderRules));
            _salesProxy = salesProxy ?? throw new ArgumentNullException(nameof(salesProxy));
        }        

        public async Task AddProductToOrderAsync(AddProductToOrderRequest request)
        {
            //узнать есть ли такой промокод если нет OrderException

            //получить продукт
            var res = await _salesProxy.GetAsync<ProductDto>($"products/1");
        }

        public async Task<OrderDto> AddAsync(CreateOrderRequest request)
        {
            var res = await _salesProxy.GetAsync<ProductDto>($"products/1");

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
