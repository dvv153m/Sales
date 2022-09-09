using Sales.Contracts.Models;
using Sales.Contracts.Request.Order;
using Sales.Core.Exceptions;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Rules.Orders;
using Sales.Core.Rules.Products;

namespace Sales.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;        
        private readonly IProductClient _productClient;
        private readonly IPromocodeClient _promocodeClient;
        private readonly OrderAddRules _orderRules;
        private readonly CartAddProductRules _cartAddProductRules;

        public OrderService(IOrderRepository orderRepository,                            
                            IProductClient productClient,
                            IPromocodeClient promocodeClient,
                            OrderAddRules orderRules,
                            CartAddProductRules cartAddProductRules)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));            
            _productClient = productClient ?? throw new ArgumentNullException(nameof(productClient));
            _promocodeClient = promocodeClient ?? throw new ArgumentNullException(nameof(promocodeClient));
            _orderRules = orderRules ?? throw new ArgumentNullException(nameof(orderRules));
            _cartAddProductRules = cartAddProductRules ?? throw new ArgumentNullException(nameof(cartAddProductRules));
        }        

        public async Task AddProductToOrderAsync(AddProductToOrderRequest request)
        {            
            var promocode = await _promocodeClient.GetByPromocodeAsync(request.Promocode);
            if (promocode == null)
            {
                throw new OrderException("данного промокода не существует");
            }
                       
            var product = await _productClient.GetProductByIdAsync(productId: request.ProductId);
            if(product == null)
            {
                throw new OrderException("данного товара не существует");
            }

            //получить текущий заказ
            //var order = GetCurrentOrder()
            
            //прогоняем по правилам добавления в корзину
            //_cartAddProductRules.Handle(order, product)
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
