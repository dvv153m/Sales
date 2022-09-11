﻿using Sales.Contracts.Entity.Order;
using Sales.Contracts.Models;
using Sales.Contracts.Request.Order;
using Sales.Core.Domain;
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

        public async Task DeleteProductFromOrderAsync(DeleteProductFromOrderRequest request)
        {
            await ExistPromocodeAndProduct(request.Promocode, request.ProductId);

            var order = await _orderRepository.GetOrderByPromocodeAsync(request.Promocode);
            if (order != null)
            {
                await _orderRepository.DeleteProductFromOrderAsync(order.Id, request.ProductId);
            }
            else
            {
                throw new OrderException("заказ не найден");
            }
        }


        public async Task<ProductDto> ExistPromocodeAndProduct(string promocode, long productId)
        {
            Promocode promocodeObj = await _promocodeClient.GetByPromocodeAsync(promocode);
            if (promocodeObj == null || promocodeObj.Value == null)
            {
                throw new OrderException("данного промокода не существует");
            }

            ProductDto product = await _productClient.GetProductByIdAsync(productId);
            if (product == null)
            {
                throw new OrderException("данного товара не существует");
            }  
            return product;
        }

        public async Task AddProductToOrderAsync(AddProductToOrderRequest request)
        {
            ProductDto product = await ExistPromocodeAndProduct(request.Promocode, request.ProductId);

            //если кол-во заказываемого товара больше чем на складе (добавить правило)

            //прогоняем по правилам добавления в корзину
            //todo сделать если заказ уже есть и его статус оформлен то в корзину уже нельзя добавлять

            //_cartAddProductRules.Handle(order, product)

            //сделать так чтобы можно было легко сделать 2 заказа по 1 промокоду

            
            var order = await _orderRepository.GetOrderByPromocodeAsync(request.Promocode);
            if (order == null)
            {
                OrderEntity orderEntity = new OrderEntity
                {
                    Promocode = request.Promocode,
                    Date = DateTime.UtcNow,
                    Status = OrderStatus.UserCollect,
                    Price = product.Price,
                    UpdateDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    OrderDetails = new List<OrderDetailsEntity>()
                    {
                        new OrderDetailsEntity
                        {
                            ProductId = product.Id,
                            Quantity = request.Quantity,
                            Price = product.Price
                        }
                    }
                };

                await _orderRepository.AddAsync(orderEntity);
            }
            else
            {                
                //это в транзакцию упаковать

                order.UpdateDate = DateTime.UtcNow;
                
                var orderDetail = order.OrderDetails.Where(p => p.ProductId == request.ProductId).FirstOrDefault();
                if (orderDetail != null)
                {                    
                    orderDetail.Quantity = request.Quantity;
                    orderDetail.Price = product.Price;
                    order.Price = GetOrderPrice(order.OrderDetails);

                    await _orderRepository.UpdateOrderDetailAsync(orderDetail);
                }
                else
                {
                    var newOrderDetail = new OrderDetailsEntity()
                    {
                        OrderId = order.Id,
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                        Price= product.Price
                    };
                    await _orderRepository.AddProductToOrder(newOrderDetail);
                    
                    order.Price = GetOrderPrice(order.OrderDetails);
                    order.Price += newOrderDetail.Price * newOrderDetail.Quantity;
                }
                await _orderRepository.UpdateAsync(order);
            }            
        }

        private decimal GetOrderPrice(IEnumerable<OrderDetailsEntity> orderDetailEntities)
        {
            decimal price = 0;
            foreach (var orderDetailEntity in orderDetailEntities)
            { 
                price += orderDetailEntity.Price * orderDetailEntity.Quantity;
            }
            return price;
        }

        /// <summary>
        /// Оформит заказ
        /// </summary>
        public void SetOrder()
        { 
            //статус заказа сменить
        }
        
        public async Task<OrderDto> AddAsync(CreateOrderRequest request)
        {
            var res = await _productClient.GetProductByIdAsync(productId: 1);

            //var product =  _salesProxy.Get<Product>(url);

            //_orderRules.Handle(new OrderDto());
            await Task.Delay(200);

            return new OrderDto();
        }

        public Task<OrderDto?> GetById(string promocode)
        {
            //забираем из бд заказ с деталями стягиваем продукты чтобы цены были актуальные

            //если статус заказа в процессе сборки то обновляем цены
            //иначе цены в order details не обновляем

            throw new NotImplementedException();
        }

        public Task<OrderDto?> GetByPromocodeId(long id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto?> GetById(long id)
        {
            throw new NotImplementedException();
        }

        /*private OrderEntity Map(CreateOrderRequest createOrderRequest)
        { 
            
        }*/
    }
}
