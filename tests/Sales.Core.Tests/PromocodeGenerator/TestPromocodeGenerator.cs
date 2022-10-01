
namespace Sales.Core.Tests.PromocodeGenerator
{
    public class TestPromocodeGenerator
    {
        [Fact]
        public void Build_EqualsPromocodeLength_OnSuccess()
        {
            //arrange
            const int expected = 7;
            var promocodeGenerator = new Sales.Infrastructure.Authentication.PromocodeGenerator();

            //act
            int actual = promocodeGenerator.Build(promocodeLenght: expected).Length;

            //arrange
            Assert.True(expected == actual);
        }

        [Fact]
        public void Build_PromocodeLenghtLess4_ThrowArgumentException()
        {
            //arrange
            const int promocodeLenght = 3;
            var promocodeGenerator = new Sales.Infrastructure.Authentication.PromocodeGenerator();

            //act            
            //arrange
            Assert.Throws<ArgumentException>(
                () => 
                {
                    return promocodeGenerator.Build(promocodeLenght);                    
                });
        }
    }
}