using Sales.Core.Domain;

namespace Sales.Core.Interfaces.Services
{
    public interface IPromocodeService
    {
        Task<string> AddPromocodeAsync();

        Task<Promocode> GetByPromocodeAsync(string promocode);        
    }
}
