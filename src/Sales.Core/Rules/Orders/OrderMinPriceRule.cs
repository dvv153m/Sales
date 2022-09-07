using Sales.Contracts.Models;
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

        public override void Handle(OrderDto order)
        {
            if (order.Price < _minPrice)
            {
                throw new OrderException($"минимальная сумма заказа: {_minPrice}") ;
            }
        }
    }
}
