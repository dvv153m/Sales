using Sales.Core.Exceptions;

namespace Sales.Core.Rules.Orders
{
    public class OrderMinPriceRule : OrderAddRules
    {
        private readonly decimal _minPrice;

        public OrderMinPriceRule(decimal minPrice)
        {
            _minPrice = minPrice;
        }

        public override void Handle(RuleContext ruleContext)
        {            
            if (ruleContext?.Order?.Price < _minPrice)
            {
                throw new OrderException($"минимальная сумма заказа: {_minPrice}") ;
            }
            base.NextRule(ruleContext);
        }
    }
}
