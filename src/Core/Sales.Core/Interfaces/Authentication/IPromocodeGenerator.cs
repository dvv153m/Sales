using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Authentication
{
    public interface IPromocodeGenerator
    {
        string Build(int promocodeLenght);
    }
}
