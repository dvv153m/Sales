using Sales.Contracts.Entity.Base;

namespace Sales.Contracts.Entity.Order
{
    public class OrderDetailsEntity : EntityBase
    {        
        /// <summary>
        /// Id заказа
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Id товара
        /// </summary>
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
