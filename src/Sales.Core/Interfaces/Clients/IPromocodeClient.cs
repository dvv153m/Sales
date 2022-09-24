using Sales.Core.Domain;

namespace Sales.Core.Interfaces.Clients
{
    public interface IPromocodeClient
    {
        Task<Promocode> GetByPromocodeAsync(string promocode);

        Task<Promocode> GeneratePromocodeAsync();
    }
}
