using Sales.Contracts.Entity.Base;

namespace Sales.Contracts.Entity.Order
{
    public class OrderDetailsEntity : EntityBase
    {        
        /// <summary>
        /// Id заказа
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// Id товара
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Кол-во товара, заказываемого пользователем
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена за единицу товара
        /// </summary>
        public decimal Price { get; set; }
    }
}
