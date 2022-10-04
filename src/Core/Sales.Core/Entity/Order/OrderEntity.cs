using Sales.Core.Entity.Base;

namespace Sales.Core.Entity.Order
{
    public class OrderEntity : EntityBase
    {        
        public string Promocode { get; set; }

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

        public DateTime UpdateDate { get; set; }

        public List<OrderDetailsEntity> OrderDetails { get; set; }
    }    
}
