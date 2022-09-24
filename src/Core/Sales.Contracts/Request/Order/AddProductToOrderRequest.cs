
namespace Sales.Contracts.Request.Order
{
    public class AddProductToOrderRequest
    {
        public string Promocode { get; set; }

        public long ProductId { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Quantity { get; set; }
    }
}
