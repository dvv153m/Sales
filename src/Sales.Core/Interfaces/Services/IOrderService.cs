﻿using Sales.Contracts.Models;
using Sales.Contracts.Request.Order;


namespace Sales.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto> AddAsync(CreateOrderRequest request);

        Task AddProductToOrderAsync(AddProductToOrderRequest request);

        Task<OrderDto?> GetById(long id);

        Task<OrderDto?> GetByPromocodeId(long id);
    }
}
