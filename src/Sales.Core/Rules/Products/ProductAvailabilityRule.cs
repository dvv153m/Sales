using Sales.Contracts.Models;
using Sales.Core.Exceptions;

namespace Sales.Core.Rules.Products
{
    /// <summary>
    /// Правило описывающее, что добавляемый товар есть в наличии
    /// </summary>
    public class ProductAvailabilityRule : CartAddProductRules
    {        
        public override void Handle(Cart cart, ProductDto product)
        {            
            if (product.CopyNumber > 0)
            {
                base.NextRule(cart, product);
            }
            else
            {
                throw new ProductException($"{product.Title} нет в наличии");
            }
        }
    }
}
