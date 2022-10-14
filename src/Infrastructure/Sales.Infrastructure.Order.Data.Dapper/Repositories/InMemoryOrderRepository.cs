using Sales.Core.Entity.Order;
using Sales.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Order.Data.Dapper.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<OrderEntity> _orders = new();

        public Task<OrderEntity> AddAsync(OrderEntity entity)
        {
            _orders.Add(entity);

            return Task.FromResult(entity);
        }

        public Task AddProductToOrder(OrderItemEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductFromOrderAsync(long orderId, long productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderEntity>> GetOrdersByPromocodeAsync(string promocode, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_orders.Where(order => order.Promocode == promocode));
        }

        public Task UpdateAsync(OrderEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderItemAsync(OrderItemEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
