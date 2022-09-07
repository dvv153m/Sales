using Sales.Contracts.Entity.Order;
using Sales.Contracts.Models;
using Sales.Core.Exceptions;
using Sales.Core.Interfaces.Repositories;


namespace Sales.Core.Rules.Orders
{
    /// <summary>
    /// Правило описывающее, что можно сделать только один заказ по одному промокоду
    /// </summary>
    public class OneOrderForOnePromocodeRule : OrderAddRules
    {
        private readonly IOrderRepository _orderRepository;

        public OneOrderForOnePromocodeRule(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentException(nameof(orderRepository)); 
        }
        public override void Handle(OrderDto order)
        {
            OrderEntity orderEntity = _orderRepository.GetOrderByPromocodeId(order.PromocodeId);
            if (orderEntity != null)
            {
                throw new OrderException("с таким промокодом уже оформляли заказ");
            }
            base.NextRule(order);
        }
    }
}
