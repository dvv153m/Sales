﻿using Sales.Core.Dto;
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
            if (ruleContext.Quantity > 1)
            {
                throw new ProductException($"В корзину можно положить только один экземпляр одного и того же товара");
            }

            if (ruleContext.Order?.OrderDetails != null)
            {                
                foreach (OrderDetailsDto orderDetail in ruleContext.Order.OrderDetails)
                { 
                    if(orderDetail.ProductId == ruleContext.Product.Id)
                    {
                        throw new ProductException($"{ruleContext.Product.Title} уже есть в корзине");
                    }
                }
            }
            
            base.NextRule(ruleContext);            
        }
    }
}
