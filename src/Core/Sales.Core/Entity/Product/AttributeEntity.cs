using Sales.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Entity.Product
{
    /// <summary>
    /// Характеристика товара (например: автор, год издания и т.д.)
    /// </summary>
    public class AttributeEntity : EntityBase
    {        
        public string Name { get; set; }     
    }
}
