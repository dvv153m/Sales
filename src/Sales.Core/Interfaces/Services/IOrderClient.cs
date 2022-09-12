
namespace Sales.Core.Interfaces.Services
{
    public interface IOrderClient
    {
        Task AddProductToOrder(string promocode, long productId, int quantity);
    }
}
