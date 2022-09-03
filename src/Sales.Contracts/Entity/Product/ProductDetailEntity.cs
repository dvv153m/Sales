using Sales.Contracts.Entity.Base;

namespace Sales.Contracts.Entity.Product
{
    public class ProductDetailEntity : EntityBase
    {        
        public long ProductId { get; set; }
        
        public long AttributeId { get; set; }

        public AttributeEntity Attribute { get; set; }

        public string Value { get; set; }        
    }
}
