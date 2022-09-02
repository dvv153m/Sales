using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Entity.Order
{
    public class OrderEntity
    {
        /// <summary>
        /// Id заказа (Номер заказа)
        /// </summary>
        public int Id { get; set; }

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
    }    
}
