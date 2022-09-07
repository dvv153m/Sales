using Sales.Contracts.Entity.Order;


namespace Sales.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderEntity> AddAsync(OrderEntity entity);

        OrderEntity GetOrderByPromocodeId(long promocodeId);
    }

    public class OrderRepository : IOrderRepository
    {
        public async Task<OrderEntity> AddAsync(OrderEntity entity)
        {
            await Task.Delay(1000); 
            return new OrderEntity();
        }

        public OrderEntity GetOrderByPromocodeId(long promocodeId)
        {            
            return new OrderEntity();
        }
    }
}
