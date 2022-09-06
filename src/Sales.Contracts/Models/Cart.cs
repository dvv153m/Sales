using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Models
{
    /// <summary>
    /// Корзина
    /// </summary>
    public class Cart
    {
        public List<ProductDto> Products { get; set; }
    }
}
