using Moq;
using Sales.Contracts.Entity.Order;
using Sales.Contracts.Models;
using Sales.Core.Exceptions;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Rules.Orders;

namespace Sales.Core.Tests.OrderRules
{
    public class TestOrderRules
    {
        [Fact]
        public void Handle_OnePromocodeOneOrder_Success()
        {
            //Arrange
            string promocode = "qwerty";
            OrderEntity? orderEntity = null;
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(x => x.GetOrderByPromocodeAsync(promocode)).Returns(Task.FromResult(orderEntity));
            var sut = new OneOrderForOnePromocodeRule(orderRepositoryMock.Object);            
            
            //Act
            //Assert
            var exception = Record.Exception(() => sut.Handle(new OrderDto() { Promocode= promocode }));
            Assert.Null(exception);           
        }

        [Fact]
        public void Handle_OnePromocodeTwoOrder_ThrowOrderException()
        {
            //Arrange
            OrderEntity orderEntity = new OrderEntity() { Promocode = "qwerty"};
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(x => x.GetOrderByPromocodeAsync(orderEntity.Promocode)).Returns(Task.FromResult(orderEntity));
            var sut = new OneOrderForOnePromocodeRule(orderRepositoryMock.Object);            
                        
            //Assert
            var exception = Assert.Throws<OrderException>(
            () =>
            {
                //Act
                sut.Handle(new OrderDto() { Promocode = orderEntity.Promocode});
            });

            Assert.NotNull(exception);
            Assert.Equal("с таким промокодом уже оформляли заказ", exception.Message);
        }
    }
}
