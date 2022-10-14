using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Request.Order
{
    /// <summary>
    /// Оформление заказа
    /// </summary>
    public class CreateOrderRequest
    {
        public string Promocode { get; set; }

        public List<OrderItemRequest> OrderItems { get; set; }
    }

    public class OrderItemRequest
    {
        public int ProductId { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Quantity { get; set; }
    }
}
