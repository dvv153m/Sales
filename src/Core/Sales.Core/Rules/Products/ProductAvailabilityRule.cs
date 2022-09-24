using Sales.Core.Exceptions;

namespace Sales.Core.Rules.Products
{
    /// <summary>
    /// Правило описывающее, что добавляемый товар есть в наличии
    /// </summary>
    public class ProductAvailabilityRule : CartAddProductRules
    {        
        public override void Handle(RuleContext ruleContext)
        {            
            if (ruleContext.Product.CopyNumber > 0)
            {
                if (ruleContext.Product.CopyNumber > ruleContext.Quantity)
                {
                    base.NextRule(ruleContext);
                }
                else
                {
                    throw new ProductException($"{ruleContext.Product.Title} нет в наличии необходимого количества");
                }
            }
            else
            {
                throw new ProductException($"{ruleContext.Product.Title} нет в наличии");
            }
        }
    }
}
