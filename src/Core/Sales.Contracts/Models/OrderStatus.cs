using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Models
{
    public enum OrderStatus
    {
        /// <summary>
        /// Статус не задан
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Пользователь собирает заказ
        /// </summary>
        UserCollect = 1,

        /// <summary>
        /// Пользователь оформил заказ
        /// </summary>
        UserCompleted = 2
    }
}
