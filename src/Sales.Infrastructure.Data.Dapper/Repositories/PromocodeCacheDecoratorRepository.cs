using Sales.Contracts.Entity;
using Sales.Core.Interfaces.Repositories;
using System.Collections.Concurrent;

namespace Sales.Infrastructure.Promocode.Data.Dapper.Repositories
{
    public class PromocodeCacheDecoratorRepository : IPromocodeRepository
    {
        private readonly IPromocodeRepository _repository;

        private static readonly ConcurrentDictionary<string, PromocodeEntity> PromocodeCache = new ConcurrentDictionary<string, PromocodeEntity>();

        public PromocodeCacheDecoratorRepository(IPromocodeRepository promocodeRepository)
        {
            _repository = promocodeRepository ?? throw new ArgumentNullException(nameof(promocodeRepository));
        }

        public async Task AddAsync(PromocodeEntity entity)
        {
             await _repository.AddAsync(entity);
            PromocodeCache.TryAdd(entity.Value, entity);
        }

        public async Task<IEnumerable<PromocodeEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PromocodeEntity> GetByPromocodeAsync(string promocode)
        {
            if (PromocodeCache.TryGetValue(promocode, out PromocodeEntity entity))
            {
                return entity;
            }

            var promocodeEntity = await _repository.GetByPromocodeAsync(promocode);
            if (promocodeEntity != null)
            {
                PromocodeCache.TryAdd(promocode, promocodeEntity);
            }

            return promocodeEntity;
        }
    }
}
