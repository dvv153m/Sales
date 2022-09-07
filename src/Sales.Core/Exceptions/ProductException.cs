using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Exceptions
{
    public class ProductException : ApplicationException
    {
        public ProductException(string message) : base(message)
        {

        }
    }
}
