using Sales.Contracts.Models;


namespace Sales.Core.Rules.Orders
{
    /// <summary>
    /// Правила добавления заказа
    /// </summary>
    public abstract class OrderAddRules
    {
        protected OrderAddRules _nextRuleHandler;

        public void SetNextRule(OrderAddRules ruleHandler)
        {
            _nextRuleHandler = ruleHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public void NextRule(OrderDto request)
        {
            if (_nextRuleHandler != null)
                _nextRuleHandler.Handle(request);
        }

        /// <summary>
        /// Обработчик правил
        /// </summary>
        /// <param name="cart">Корзина</param>
        /// <param name="product">Добавляемый товар в корзину</param>
        /// <param name="errorInfo">Описывает причину невозможности добавления товара</param>
        public abstract void Handle(OrderDto order);
    }
}
