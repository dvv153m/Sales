using Sales.Contracts.Entity.Order;


namespace Sales.Contracts.Models
{
    public class OrderDto
    {
        public long Id;

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

        public List<OrderDetailsDto> OrderDetails { get; set; }
    }

    public class OrderDetailsDto
    {
        public int ProductId { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена с учетом кол-ва товара
        /// </summary>
        public decimal Price { get; set; }
    }
}
