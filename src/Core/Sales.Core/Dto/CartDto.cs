using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Dto
{
    /// <summary>
    /// Корзина
    /// </summary>
    public class CartDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
