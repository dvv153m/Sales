using Sales.Core.Domain;

namespace Sales.Core.Interfaces.Services
{
    public interface IPromocodeClient
    {
        Task<Promocode> GetByPromocode(string promocode);
    }
}
