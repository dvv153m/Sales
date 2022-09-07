using Sales.Contracts.Models;
using Sales.Core.Exceptions;

namespace Sales.Core.Rules.Products
{
    /// <summary>
    /// Правило описывающее, что в корзине только уникальные товары
    /// </summary>
    public class ProductUniquenessRule : CartAddProductRules
    {
        public override void Handle(Cart cart, ProductDto product)
        {            
            bool isUniqueProduct = !cart.Products.Select(p => p.Id).Contains(product.Id);

            if (isUniqueProduct)
            {
                base.NextRule(cart, product);
            }
            else
            {
                throw new ProductException($"{product.Title} уже есть в корзине");                
            }
        }
    }
}
