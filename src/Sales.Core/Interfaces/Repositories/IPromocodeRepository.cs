using Sales.Contracts.Entity;
using Sales.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Repositories
{
    public interface IPromocodeRepository
    {
        void Add(PromocodeEntity promocode);

        PromocodeEntity GetByPromocode(string promocode);

        IEnumerable<PromocodeEntity> GetAll();
    }
}
