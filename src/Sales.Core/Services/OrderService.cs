﻿using Sales.Contracts.Entity.Order;
using Sales.Contracts.Models;
using Sales.Contracts.Request.Order;
using Sales.Core.Domain;
using Sales.Core.Exceptions;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;
using Sales.Core.Rules;
using Sales.Core.Rules.Orders;
using Sales.Core.Rules.Products;
using System.Data;

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

        /// <summary>
        /// Добавление товара в корзину
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task AddProductToOrderAsync(AddProductToOrderRequest request)
        {            
            await ValidatePromocode(request.Promocode);
            ProductDto productDto = await GetProduct(request.ProductId);

            IEnumerable<OrderEntity> orders = await _orderRepository.GetOrdersByPromocodeAsync(request.Promocode);
            var completedOrder = orders.Where(x => x.Status == OrderStatus.UserCompleted);
            var order = orders.FirstOrDefault(x => x.Status == OrderStatus.UserCollect);

            OrderDto orderDto = Map(order);

            var ruleContext = new RuleContext(orderDto, productDto, request.Quantity, completedOrder.Count());

            _cartAddProductRules.Handle(ruleContext);

            if (order == null)
            {
                OrderEntity orderEntity = new OrderEntity
                {
                    Promocode = request.Promocode,
                    Date = DateTime.UtcNow,
                    Status = OrderStatus.UserCollect,
                    Price = productDto.Price,
                    UpdateDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    OrderDetails = new List<OrderDetailsEntity>()
                    {
                        new OrderDetailsEntity
                        {
                            ProductId = productDto.Id,
                            Quantity = request.Quantity,
                            Price = productDto.Price
                        }
                    }
                };

                await _orderRepository.AddAsync(orderEntity);
            }
            else
            {                
                //todo это в транзакцию упаковать

                order.UpdateDate = DateTime.UtcNow;
                
                var orderDetail = order.OrderDetails.Where(p => p.ProductId == request.ProductId).FirstOrDefault();
                if (orderDetail != null)
                {                    
                    orderDetail.Quantity = request.Quantity;
                    orderDetail.Price = productDto.Price;
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
                        Price= productDto.Price
                    };
                    await _orderRepository.AddProductToOrder(newOrderDetail);
                    
                    order.Price = GetOrderPrice(order.OrderDetails);
                    order.Price += newOrderDetail.Price * newOrderDetail.Quantity;
                }
                await _orderRepository.UpdateAsync(order);
            }            
        }                

        /// <summary>
        /// Оформит заказ
        /// </summary>
        public async Task SetOrder(string promocode)
        {
            await ValidatePromocode(promocode);

            IEnumerable<OrderEntity> orders = await _orderRepository.GetOrdersByPromocodeAsync(promocode);
            var order = orders.FirstOrDefault(x => x.Status == OrderStatus.UserCollect);
            if (order != null)
            {
                order.Status = OrderStatus.UserCompleted;
                await _orderRepository.UpdateAsync(order);
            }
            else
            {
                throw new OrderException($"не найден заказ по промокоду:{promocode}");
            }            
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

        public async Task DeleteProductFromOrderAsync(DeleteProductFromOrderRequest request)
        {            
            await ValidatePromocode(request.Promocode);
            await ValidateProduct(request.ProductId);            

            var orders = await _orderRepository.GetOrdersByPromocodeAsync(request.Promocode);
            var order = orders.FirstOrDefault(x => x.Status == OrderStatus.UserCollect);
            if (order != null)
            {
                await _orderRepository.DeleteProductFromOrderAsync(order.Id, request.ProductId);
            }
            else
            {
                throw new OrderException("заказ не найден");
            }
        }

        /// <summary>
        /// Проверка на существование промокода
        /// </summary>
        /// <param name="promocode">введеный пользователем промокод</param>
        /// <returns>OrderException если промокод отсутствует</returns>
        /// <exception cref="OrderException"></exception>
        private async Task ValidatePromocode(string promocode)
        {
            Promocode promocodeObj = await _promocodeClient.GetByPromocodeAsync(promocode);
            if (promocodeObj == null || promocodeObj.Value == null)
            {
                throw new OrderException("данного промокода не существует");
            }
        }

        /// <summary>
        /// Проверка на существование товара
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private async Task ValidateProduct(long productId)
        {
            await GetProduct(productId);
        }

        private async Task<ProductDto> GetProduct(long productId)
        {
            ProductDto product = await _productClient.GetProductByIdAsync(productId);
            if (product == null)
            {
                throw new OrderException("данного товара не существует");
            }
            return product;
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

        private OrderDto? Map(OrderEntity? orderEntity)
        {
            if (orderEntity != null)
            {
                var orderDetails = new List<OrderDetailsDto>();
                if (orderEntity.OrderDetails != null)
                {
                    foreach (var orderDetail in orderEntity.OrderDetails)
                    {
                        orderDetails.Add(new OrderDetailsDto
                        {
                            ProductId = orderDetail.ProductId,
                            Quantity = orderDetail.Quantity,
                            Price = orderDetail.Price,
                        });
                    }
                }
                return new OrderDto
                {
                    Id = orderEntity.Id,
                    Promocode = orderEntity.Promocode,
                    Date = orderEntity.Date,
                    Status = orderEntity.Status,
                    Price = orderEntity.Price,
                    OrderDetails = orderDetails
                };
            }
            else
            {
                return null;
            }
        }

        /*private OrderEntity Map(CreateOrderRequest createOrderRequest)
        { 
            
        }*/
    }
}
