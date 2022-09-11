﻿using Sales.Contracts.Entity.Order;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderEntity> AddAsync(OrderEntity entity);

        Task AddProductToOrder(OrderDetailsEntity entity);

        Task<OrderEntity> GetOrderByPromocodeAsync(string promocode);

        Task UpdateAsync(OrderEntity entity);

        Task UpdateOrderDetailAsync(OrderDetailsEntity entity);
    }    
}
