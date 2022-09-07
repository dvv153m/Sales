using Sales.Contracts.Entity.Order;


namespace Sales.Contracts.Models
{
    public class OrderDto
    {
        public long PromocodeId { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        public decimal Price { get; set; }

        public List<OrderDetailsEntity> OrderDetails { get; set; }
    }
}
