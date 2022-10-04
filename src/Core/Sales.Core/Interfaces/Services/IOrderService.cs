using Sales.Contracts.Request.Order;
using Sales.Core.Dto;

namespace Sales.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto> AddAsync(CreateOrderRequest request);

        Task DeleteProductFromOrderAsync(DeleteProductFromOrderRequest request);


        Task AddProductToOrderAsync(AddProductToOrderRequest request);

        Task<OrderDto?> GetById(long id);

        Task<OrderDto?> GetByPromocodeId(long id);
    }
}
