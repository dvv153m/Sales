using Moq;
using Sales.Core.Entity.Order;
using Sales.Contracts.Models;
using Sales.Core.Exceptions;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Rules;
using Sales.Core.Rules.Orders;

namespace Sales.Core.Tests.OrderRules
{
    public class TestOrderRules
    {
        [Fact]
        public void Handle_OnePromocodeOneOrder_Success()
        {
            //Arrange
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            string promocode = "qwerty";
            IEnumerable<OrderEntity> orders = new List<OrderEntity>() { };
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(x => x.GetOrdersByPromocodeAsync(promocode, cancelTokenSource.Token)).Returns(Task.FromResult(orders));
            var sut = new OneOrderForOnePromocodeRule(orderRepositoryMock.Object);

            var ruleContext = new RuleContext(order: new OrderDto() { Promocode = promocode },
                                                  product: null,
                                                  quantity: 1,
                                                  orderCountByPromocode: 0);

            //Act
            //Assert            
            var exception = Record.Exception(() =>
            {                
                sut.Handle(ruleContext);
            });
                
            Assert.Null(exception);           
        }

        [Fact]
        public void Handle_OnePromocodeTwoOrder_ThrowOrderException()
        {
            //Arrange
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            OrderEntity orderEntity = new OrderEntity() { Promocode = "qwerty"};
            IEnumerable<OrderEntity> orders = new List<OrderEntity>() { };
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(x => x.GetOrdersByPromocodeAsync(orderEntity.Promocode, cancelTokenSource.Token)).Returns(Task.FromResult(orders));
            var sut = new OneOrderForOnePromocodeRule(orderRepositoryMock.Object);

            var ruleContext = new RuleContext(order: new OrderDto() { Promocode = orderEntity.Promocode },
                                                  product: null,
                                                  quantity: 1,
                                                  orderCountByPromocode: 0);

            //Assert
            var exception = Assert.Throws<OrderException>(
            () =>
            {                                                
                //Act
                sut.Handle(ruleContext);
            });

            Assert.NotNull(exception);
            Assert.Equal("с таким промокодом уже оформляли заказ", exception.Message);
        }
    }
}
