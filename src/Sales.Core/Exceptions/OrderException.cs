using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Exceptions
{
    public class OrderException : ApplicationException
    {
        public OrderException(string message) : base(message)
        {

        }
    }
}
