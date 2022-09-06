using Sales.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Rules.Products
{
    /// <summary>
    /// Обработчик правил добавления товаров в корзину
    /// </summary>
    public abstract class CartRuleHandler
    {
        protected CartRuleHandler _nextRuleHandler;

        public void SetNextHandler(CartRuleHandler ruleHandler)
        {
            _nextRuleHandler = ruleHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public void NextHandle(Cart cart, ProductDto request, ref string errorInfo)
        {            
            if (_nextRuleHandler != null)
                _nextRuleHandler.Handle(cart, request, ref errorInfo);
        }

        /// <summary>
        /// Обработчика правил
        /// </summary>
        /// <param name="cart">Корзина</param>
        /// <param name="product">Добавляемый товар в корзину</param>
        /// <param name="errorInfo">Описывает причину невозможности добавления товара</param>
        public abstract void Handle(Cart cart, ProductDto product, ref string errorInfo);
    }
}
