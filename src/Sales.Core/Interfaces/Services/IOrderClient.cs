
namespace Sales.Core.Interfaces.Services
{
    public interface IOrderClient
    {
        Task AddProductToOrder(string promocode, long productId, int quantity);

        /// <summary>
        /// Удаление товара из корзины
        /// </summary>
        /// <param name="promocode"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task DeleteFromCart(string promocode, long productId);
    }
}
