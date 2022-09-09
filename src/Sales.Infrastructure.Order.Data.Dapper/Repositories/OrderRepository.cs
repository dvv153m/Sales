using Sales.Contracts.Entity.Order;
using Sales.Core.Interfaces.Repositories;


namespace Sales.Infrastructure.Order.Data.Dapper.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Task<OrderEntity> AddAsync(OrderEntity entity)
        {
            throw new NotImplementedException();
        }

        public OrderEntity GetOrderByPromocodeId(long promocodeId)
        {
            throw new NotImplementedException();
        }
    }
}
