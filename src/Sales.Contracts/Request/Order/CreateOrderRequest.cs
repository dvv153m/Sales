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

        public List<OrderDetailsRequest> OrderDetails { get; set; }
    }

    public class OrderDetailsRequest
    {
        public int ProductId { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Quantity { get; set; }
    }
}
