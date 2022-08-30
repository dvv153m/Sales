
namespace Sales.Core.Tests.PromocodeGenerator
{
    public class TestPromocodeGenerator
    {
        [Fact]
        public void Build_OnSuccess_ReturnPromocode()
        {
            //Arrange
            int promocodeLen = 7;

            //Act
            string promocode = Helper.PromocodeGenerator.Build(promocodeLen);

            //Arrange
            Assert.True(promocode.Length == promocodeLen);
        }

        [Fact]
        public void Build_ThrowArgumentException()
        {
            //Arrange
            int promocodeLen = 3;

            //Act            
            //Arrange
            Assert.Throws<ArgumentException>(
                () => 
                {
                    return Helper.PromocodeGenerator.Build(promocodeLen);                    
                });
        }
    }
}