using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Services
{
    public interface IPromocodeService
    {
        bool AddPromocode();

        bool LoginByPromocode(string promocode);        
    }
}
