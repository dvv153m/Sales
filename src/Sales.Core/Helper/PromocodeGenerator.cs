
using Sales.Core.Interfaces.Services;

namespace Sales.Core.Helper
{    
    public class PromocodeGenerator : IPromocodeGenerator
    {
        private Random _random = new Random();

        private string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string Build(int promocodeLenght)
        {
            if (promocodeLenght < 4)
                throw new ArgumentException("Min promocode lenght is 4 simbol");

            return string.Create(promocodeLenght, _chars, (span, chars) =>
            {
                for (int i = 0; i < promocodeLenght; i++)
                {
                    span[i] = chars[_random.Next(chars.Length)];
                }
            });            
        }
    }
}
