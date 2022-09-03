using Sales.Contracts.Entity.Base;

namespace Sales.Contracts.Entity.Product
{
    public class ProductDetailEntity : EntityBase
    {        
        public int ProductId { get; set; }
        
        public int AttributeId { get; set; }
        public AttributeEntity Attribute { get; set; }

        public string Value { get; set; }        
    }
}
