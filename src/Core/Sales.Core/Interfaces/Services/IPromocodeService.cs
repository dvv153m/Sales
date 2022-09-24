using Sales.Core.Domain;

namespace Sales.Core.Interfaces.Services
{
    public interface IPromocodeService
    {         
        Task<Promocode> GetByPromocodeAsync(string promocode);

        Task<Promocode> AddPromocodeAsync();
    }
}
