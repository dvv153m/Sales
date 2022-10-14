using Sales.Core.Entity.Order;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderEntity> AddAsync(OrderEntity entity);

        Task AddProductToOrder(OrderItemEntity entity);

        Task<IEnumerable<OrderEntity>> GetOrdersByPromocodeAsync(string promocode, CancellationToken cancellationToken = default);

        Task UpdateAsync(OrderEntity entity);

        Task UpdateOrderItemAsync(OrderItemEntity entity);

        Task DeleteProductFromOrderAsync(long orderId, long productId);
    }    
}
