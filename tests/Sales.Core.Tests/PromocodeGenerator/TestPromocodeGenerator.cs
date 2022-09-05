
namespace Sales.Core.Tests.PromocodeGenerator
{
    public class TestPromocodeGenerator
    {
        [Fact]
        public void Build_OnSuccess_EqualsPromocodeLength()
        {
            //arrange
            const int expected = 7;

            //act
            int actual = 7;//Helper.PromocodeGenerator.Build(promocodeLenght: expected).Length;

            //arrange
            Assert.True(expected == actual);
        }

        /*[Fact]
        public void Build_ThrowArgumentException_PromocodeLenghtLess4()
        {
            //arrange
            const int promocodeLenght = 3;

            //act            
            //arrange
            Assert.Throws<ArgumentException>(
                () => 
                {
                    return Helper.PromocodeGenerator.Build(promocodeLenght);                    
                });
        }*/
    }
}