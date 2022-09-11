using Sales.Contracts.Models;
using Sales.Core.Exceptions;

namespace Sales.Core.Rules.Products
{
    /// <summary>
    /// Правило описывающее, что в корзине только уникальные товары
    /// </summary>
    public class ProductUniquenessRule : CartAddProductRules
    {
        public override void Handle(RuleContext ruleContext)
        {
            if (ruleContext.Order?.OrderDetails != null)
            {                
                foreach (OrderDetailsDto p in ruleContext.Order.OrderDetails)
                { 
                    if(p.ProductId == ruleContext.Product.Id)
                    {
                        throw new ProductException($"{ruleContext.Product.Title} уже есть в корзине");
                    }
                }
            }
            
            base.NextRule(ruleContext);            
        }
    }
}
