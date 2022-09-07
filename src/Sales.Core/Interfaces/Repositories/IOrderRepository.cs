using Sales.Contracts.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        OrderEntity GetOrderByPromocodeId(long promocodeId);
    }
}
