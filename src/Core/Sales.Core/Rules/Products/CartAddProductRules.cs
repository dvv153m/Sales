
namespace Sales.Core.Rules.Products
{
    /// <summary>
    /// Правила добавления товаров в корзину
    /// </summary>
    public abstract class CartAddProductRules
    {
        protected CartAddProductRules _nextRuleHandler;

        public void SetNextRule(CartAddProductRules ruleHandler)
        {
            _nextRuleHandler = ruleHandler;
        }
        
        public void NextRule(RuleContext ruleContext)
        {            
            if (_nextRuleHandler != null)
                _nextRuleHandler.Handle(ruleContext);
        }

        /// <summary>
        /// Обработчика правил
        /// </summary>
        /// <param name="orderDto">Заказ с текущим содержимым</param>
        /// <param name="product">Добавляемый товар в корзину(в заказ)</param>        
        public abstract void Handle(RuleContext ruleContext);
    }
}
