using Sales.Contracts.Entity.Order;
using Sales.Core.Interfaces.Repositories;
using System.Collections.Concurrent;

namespace Sales.Infrastructure.Order.Data.Dapper.Repositories
{
    public class OrderCacheDecoratorRepository : IOrderRepository
    {
        private readonly IOrderRepository _repository;
        private static readonly ConcurrentDictionary<string, List<OrderEntity>> _ordersCacheByPromocode = new ConcurrentDictionary<string, List<OrderEntity>>();
        private static readonly ConcurrentDictionary<long, OrderEntity> _ordersCacheById = new ConcurrentDictionary<long, OrderEntity>();

        public OrderCacheDecoratorRepository(IOrderRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<OrderEntity> AddAsync(OrderEntity entity)
        {
            OrderEntity orderEntity = await _repository.AddAsync(entity);            
            _ordersCacheById.TryAdd(orderEntity.Id, entity);

            if (_ordersCacheByPromocode.TryGetValue(orderEntity.Promocode, out List<OrderEntity> orders))
            {
                orders.Add(orderEntity);
            }
            else
            {
                _ordersCacheByPromocode.TryAdd(orderEntity.Promocode, new List<OrderEntity>() { orderEntity });                
            }

            return orderEntity;
        }

        public async Task AddProductToOrder(OrderDetailsEntity entity)
        {
            await _repository.AddProductToOrder(entity);
            if (_ordersCacheById.TryGetValue(entity.OrderId, out var order))
            {                
                order.OrderDetails.Add(entity);                
            }
            
        }

        public async Task DeleteProductFromOrderAsync(long orderId, long productId)
        {
            await _repository.DeleteProductFromOrderAsync(orderId, productId);
            if (_ordersCacheById.TryGetValue(orderId, out OrderEntity order))
            {                
                order.OrderDetails.RemoveAll(x => x.OrderId == orderId && x.ProductId == productId);                
            }                        
        }

        public async Task<IEnumerable<OrderEntity>> GetOrdersByPromocodeAsync(string promocode)
        {
            if (_ordersCacheByPromocode.TryGetValue(promocode, out List<OrderEntity> orders))
            {
                return orders;
            }
            return await _repository.GetOrdersByPromocodeAsync(promocode);            
        }

        public async Task UpdateAsync(OrderEntity entity)
        {
            await _repository.UpdateAsync(entity);
            _ordersCacheById.AddOrUpdate(entity.Id, entity, (oldValue, newValue) => entity);

            if (_ordersCacheByPromocode.TryGetValue(entity.Promocode, out List<OrderEntity> orders))
            {                
                orders.RemoveAll(x => x.Id == entity.Id); 
                orders.Add(entity);                
            }
        }

        public async Task UpdateOrderDetailAsync(OrderDetailsEntity entity)
        {
            await _repository.UpdateOrderDetailAsync(entity);   
        }
    }
}
