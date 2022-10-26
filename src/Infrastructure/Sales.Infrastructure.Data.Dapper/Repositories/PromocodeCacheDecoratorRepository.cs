using Microsoft.Extensions.Caching.Memory;
using Sales.Core.Entity;
using Sales.Core.Interfaces.Repositories;
using System.Collections.Concurrent;

namespace Sales.Infrastructure.Promocode.Data.Dapper.Repositories
{
    public class PromocodeCacheDecoratorRepository : IPromocodeRepository
    {
        private readonly IPromocodeRepository _repository;

        private static readonly ConcurrentDictionary<string, PromocodeEntity> _promocodesCache = new ConcurrentDictionary<string, PromocodeEntity>();

        private readonly IMemoryCache _memoryCache;

        private readonly TimeSpan _absoluteExpirationRelativeToNow;

        public PromocodeCacheDecoratorRepository(IPromocodeRepository promocodeRepository,
                                                 IMemoryCache memoryCache,
                                                 TimeSpan absoluteExpirationRelativeToNow)
        {
            _repository = promocodeRepository ?? throw new ArgumentNullException(nameof(promocodeRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

            _absoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
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

        public Task<PromocodeEntity> GetByPromocodeAsync(string promocode, CancellationToken cancellationToken = default)
        {
            string key = $"promo-{promocode}";
            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(_absoluteExpirationRelativeToNow);
                    return  _repository.GetByPromocodeAsync(promocode, cancellationToken);
                });
        }
    }
}
