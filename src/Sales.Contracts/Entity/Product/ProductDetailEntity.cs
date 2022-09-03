using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Entity.Product
{
    public class ProductDetailEntity
    {
        public int Id { get; set; }

        //public int ProductId { get; set; }

        //public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }

        public string Value { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
