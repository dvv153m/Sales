using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Request.Order
{
    public class AddProductToOrderRequest
    {
        public string Promocode { get; set; }

        public long ProductId { get; set; }
    }
}
