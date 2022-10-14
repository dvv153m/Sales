
namespace Sales.Core.Dto
{
    public class OrderItemsDto
    {
        public long ProductId { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена за единицу товара
        /// </summary>
        public decimal Price { get; set; }
    }
}
