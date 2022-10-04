using Sales.Core.Entity;
using Sales.Core.Interfaces.Repositories;
using System.Collections.Concurrent;

namespace Sales.Infrastructure.Promocode.Data.Dapper.Repositories
{
    public class PromocodeCacheDecoratorRepository : IPromocodeRepository
    {
        private readonly IPromocodeRepository _repository;

        private static readonly ConcurrentDictionary<string, PromocodeEntity> _promocodesCache = new ConcurrentDictionary<string, PromocodeEntity>();

        public PromocodeCacheDecoratorRepository(IPromocodeRepository promocodeRepository)
        {
            _repository = promocodeRepository ?? throw new ArgumentNullException(nameof(promocodeRepository));
        }

        public async Task AddAsync(PromocodeEntity entity)
        {
             await _repository.AddAsync(entity);
            _promocodesCache.TryAdd(entity.Value, entity);
        }

        public async Task<IEnumerable<PromocodeEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<PromocodeEntity> GetByPromocodeAsync(string promocode, CancellationToken cancellationToken = default)
        {
            if (_promocodesCache.TryGetValue(promocode, out PromocodeEntity entity))
            {
                return entity;
            }

            var promocodeEntity = await _repository.GetByPromocodeAsync(promocode, cancellationToken);
            if (promocodeEntity != null)
            {
                _promocodesCache.TryAdd(promocode, promocodeEntity);
            }

            return promocodeEntity;
        }
    }
}
