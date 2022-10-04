using AutoFixture;
using Sales.Core.Dto;

namespace Sales.Order.Api.Tests
{
    public class OrderControllerTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Create_Order_Success()
        {
            var expected = _fixture.Build<OrderDto>().Create();
        }
    }
}