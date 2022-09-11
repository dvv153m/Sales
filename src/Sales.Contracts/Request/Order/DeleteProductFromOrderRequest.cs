
namespace Sales.Contracts.Request.Order
{
    public class DeleteProductFromOrderRequest
    {
        public string Promocode { get; set; }

        public long ProductId { get; set; }
    }
}
