using Sales.Core.Interfaces.Services;


namespace Sales.Infrastructure.Services
{
    public class PromocodeGenerator : IPromocodeGenerator
    {
        private Random _random = new Random();

        private string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private int _promocodeLenght;

        public PromocodeGenerator(int promocodeLenght)
        {
            if (promocodeLenght < 2)
                throw new ArgumentException("Min promocode lenght is 2 simbol");
            _promocodeLenght = promocodeLenght;
        }

        public string Build()
        {
            return new string(Enumerable.Repeat(_chars, _promocodeLenght)
                              .Select(s => s[_random.Next(s.Length)])
                              .ToArray());
        }
    }
}
