
namespace Sales.Core.Interfaces.Services
{
    public interface IPromocodeClient
    {
        Task<bool> IsExist(string promocode);
    }
}
