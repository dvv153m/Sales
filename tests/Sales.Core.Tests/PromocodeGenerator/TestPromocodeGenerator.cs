
namespace Sales.Core.Tests.PromocodeGenerator
{
    public class TestPromocodeGenerator
    {
        [Fact]
        public void Build_OnSuccess_ReturnPromocode()
        {
            //Arrange
            int promocodeLen = 8;

            //Act
            string promocode = Helper.PromocodeGenerator.Build(promocodeLen);

            //Arrange
            Assert.True(promocode.Length == promocodeLen);
        }
    }
}