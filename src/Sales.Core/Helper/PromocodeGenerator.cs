
namespace Sales.Core.Helper
{
    public static class PromocodeGenerator
    {
        private static Random _random = new Random();

        private static string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string Build(int promocodeLenght)
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
