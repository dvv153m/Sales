using Sales.Contracts.Entity;


namespace Sales.Core.Interfaces.Repositories
{
    public interface IPromocodeRepository
    {
        Task AddAsync(PromocodeEntity entity);

        Task<PromocodeEntity> GetByPromocodeAsync(string promocode);

        Task<IEnumerable<PromocodeEntity>> GetAllAsync();
    }
}
