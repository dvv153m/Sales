using Sales.Core.Domain;

namespace Sales.Core.Interfaces.Services
{
    public interface IPromocodeClient
    {
        Task<Promocode> GetByPromocodeAsync(string promocode);

        Task<Promocode> GeneratePromocodeAsync();
    }
}
