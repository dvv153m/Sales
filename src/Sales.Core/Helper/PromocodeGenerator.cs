using Sales.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return new string(Enumerable.Repeat(_chars, promocodeLenght)
                              .Select(s => s[_random.Next(s.Length)])
                              .ToArray());
        }
    }
}
