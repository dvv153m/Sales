
namespace Sales.Core.Dto
{
    public class OrderDto
    {
        public long Id;

        public string Promocode { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatusDto Status { get; set; }

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        public decimal Price { get; set; }

        public List<OrderItemsDto> OrderItems { get; set; }
    }    
}
