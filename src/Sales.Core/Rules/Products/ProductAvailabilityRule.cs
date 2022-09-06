using Sales.Contracts.Models;


namespace Sales.Core.Rules.Products
{
    /// <summary>
    /// Правило описывающее, что добавляемый товар есть в наличии
    /// </summary>
    public class ProductAvailabilityRule : CartRuleHandler
    {        
        public override void Handle(Cart cart, ProductDto product, ref string errorInfo)
        {            
            if (product.CopyNumber > 0)
            {
                base.NextHandle(cart, product, ref errorInfo);
            }
            else
            {
                errorInfo = $"{product.Title} нет в наличии";
            }
        }
    }
}
