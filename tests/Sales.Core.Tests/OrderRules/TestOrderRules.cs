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
            long promocodeId = 1;
            OrderEntity orderEntity = null;
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(x => x.GetOrderByPromocodeId(promocodeId)).Returns(orderEntity);
            var sut = new OneOrderForOnePromocodeRule(orderRepositoryMock.Object);            
            
            //Act
            //Assert
            var exception = Record.Exception(() => sut.Handle(new OrderDto() { PromocodeId= promocodeId }));
            Assert.Null(exception);           
        }

        [Fact]
        public void Handle_OnePromocodeTwoOrder_ThrowOrderException()
        {
            //Arrange
            OrderEntity orderEntity = new OrderEntity() { PromocodeId = 1};
            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(x => x.GetOrderByPromocodeId(orderEntity.PromocodeId)).Returns(orderEntity);
            var sut = new OneOrderForOnePromocodeRule(orderRepositoryMock.Object);            
                        
            //Assert
            var exception = Assert.Throws<OrderException>(
            () =>
            {
                //Act
                sut.Handle(new OrderDto() { PromocodeId = orderEntity.PromocodeId});
            });

            Assert.NotNull(exception);
            Assert.Equal("с таким промокодом уже оформляли заказ", exception.Message);
        }
    }
}
