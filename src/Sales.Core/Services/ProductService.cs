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
            ProductEntity productEntity = Map(request);
            await _productRepository.AddAsync(productEntity);
        }        

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
             var entities = await _productRepository.GetAll();
            return entities;
        }

        private ProductEntity Map(CreateProductRequest request)
        {
            var productDetails = new List<ProductDetailEntity>();
            foreach (var productDetail in request.ProductDetails)
            {
                productDetails.Add(new ProductDetailEntity
                {
                    AttributeId = productDetail.AttributeId,
                    Value = productDetail.Value
                });
            }
            return new ProductEntity
            {
                Title = request.Title,
                CopyNumber = request.CopyNumber,
                Price = request.Price,
                ProductDetails = productDetails
            };
        }
    }
}
