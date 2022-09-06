using Sales.Contracts.Models;

namespace Sales.Core.Rules.Products
{
    /// <summary>
    /// Правило описывающее, что в корзине только уникальные товары
    /// </summary>
    public class UniqueProductHandler : CartRuleHandler
    {
        public override void Handle(Cart cart, ProductDto product, ref string errorInfo)
        {            
            bool isUniqueProduct = !cart.Products.Select(p => p.Id).Contains(product.Id);

            if (isUniqueProduct)
            {
                base.NextHandle(cart, product, ref errorInfo);
            }
            else
            {
                errorInfo = $"{product.Title} уже есть в корзине";
            }
        }
    }
}
