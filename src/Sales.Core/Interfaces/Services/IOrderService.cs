using Sales.Contracts.Models;
using Sales.Contracts.Request.Order;


namespace Sales.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto> AddAsync(CreateOrderRequest request);

        Task<OrderDto?> GetById(long id);
    }
}
