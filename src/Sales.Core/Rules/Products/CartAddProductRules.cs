using Sales.Contracts.Models;


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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public void NextRule(Cart cart, ProductDto request)
        {            
            if (_nextRuleHandler != null)
                _nextRuleHandler.Handle(cart, request);
        }

        /// <summary>
        /// Обработчика правил
        /// </summary>
        /// <param name="cart">Корзина</param>
        /// <param name="product">Добавляемый товар в корзину</param>        
        public abstract void Handle(Cart cart, ProductDto product);
    }
}
