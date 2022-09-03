using Sales.Contracts.Entity.Product;
using Sales.Contracts.Request.Product;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;

namespace Sales.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task AddAsync(CreateProductRequest request)
        {
            var productEntity = new ProductEntity
            {
                Title = request.Title,
                CopyNumber = request.CopyNumber,
                Price = request.Price
            };

            await _productRepository.AddAsync(productEntity);
        }

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
             var entities = await _productRepository.GetAll();
            return entities;
        }
    }
}
