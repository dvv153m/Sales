using Sales.Contracts.Models;


namespace Sales.Core.Rules
{
    /// <summary>
    /// Контекс правил для их последующей обработки
    /// </summary>
    public class RuleContext
    {
        /// <summary>
        /// Текущий заказ (уже в бд)
        /// </summary>
        public OrderDto Order { get; private set; }
        
        /// <summary>
        /// Добавляемый товар в заказ (корзину)
        /// </summary>
        public ProductDto Product { get; private set; }

        /// <summary>
        /// Кол-во добавляемых экземпляров Product в заказ (корзину)
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order">Текущий заказ (уже в бд)</param>
        /// <param name="product">Добавляемый товар в заказ (корзину)</param>
        /// <param name="quantity">Кол-во экземпляров Product</param>
        public RuleContext(OrderDto order, ProductDto product,  int quantity)
        {
            Order = order;
            Product = product;
            Quantity = quantity;
        }
    }
}
