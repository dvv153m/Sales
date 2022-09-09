using Sales.Contracts.Entity.Order;


namespace Sales.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        //Task<OrderEntity> AddAsync(OrderEntity entity);

        OrderEntity GetOrderByPromocodeId(long promocodeId);
    }    
}
