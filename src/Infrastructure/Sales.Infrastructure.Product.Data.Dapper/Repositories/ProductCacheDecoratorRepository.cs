using Sales.Contracts.Entity.Product;
using Sales.Core.Interfaces.Repositories;
using System.Collections.Concurrent;

namespace Sales.Infrastructure.Product.Data.Dapper.Repositories
{
    public class ProductCacheDecoratorRepository : IProductRepository
    {
        private readonly IProductRepository _repository;

        private static readonly ConcurrentDictionary<long, ProductEntity> _productsCache = new ConcurrentDictionary<long, ProductEntity>();

        public ProductCacheDecoratorRepository(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ProductEntity> AddAsync(ProductEntity entity)
        {
            ProductEntity productEntity = await _repository.AddAsync(entity);
            _productsCache.TryAdd(entity.Id, entity);
            return productEntity;
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<ProductEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            if (_productsCache.TryGetValue(id, out ProductEntity entity))
            {
                return entity;
            }

            var productEntity = await _repository.GetByIdAsync(id, cancellationToken);
            if (productEntity != null)
            {
                _productsCache.TryAdd(productEntity.Id, productEntity);
            }

            return productEntity;
        }

        public async Task<IEnumerable<ProductEntity>> GetByIdsAsync(int[] ids)
        {
            var products = new List<ProductEntity>(ids.Length);
            var notFoundIds = new List<int>(ids.Length);
            foreach (var id in ids)
            {
                if (_productsCache.TryGetValue(id, out ProductEntity entity))
                {
                    products.Add(entity);
                }
                else
                { 
                    notFoundIds.Add(id);
                }
            }

            IEnumerable<ProductEntity> productEntities = await _repository.GetByIdsAsync(notFoundIds.ToArray());
            if (productEntities != null && productEntities.Any())
            {
                foreach (var entity in productEntities)
                {
                    _productsCache.TryAdd(entity.Id, entity);
                }
            }

            return products;
        }

        public async Task UpdateAsync(ProductEntity entity)
        {
            await _repository.UpdateAsync(entity);

            _productsCache.AddOrUpdate(entity.Id, entity, (oldValue, newValue) => entity);            
        }
    }
}
