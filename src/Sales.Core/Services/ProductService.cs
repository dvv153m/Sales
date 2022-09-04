using Sales.Contracts.Entity.Product;
using Sales.Contracts.Models;
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

        public async Task<ProductDto> AddAsync(CreateProductRequest request)
        {
            ProductEntity productEntity = Map(request);
            productEntity = await _productRepository.AddAsync(productEntity);

            ProductDto productDto = Map(productEntity);
            return productDto;
        }

        public async Task<ProductDto> GetById(long id)
        {
            ProductEntity productEntity = await _productRepository.GetById(id);

            ProductDto productDto = Map(productEntity);
            return productDto;
        }

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
             var entities = await _productRepository.GetAll();
            return entities;
        }

        private ProductDto Map(ProductEntity request)
        {
            var productDetails = new List<ProductDetailDto>();
            foreach (var productDetail in request.ProductDetails)
            {
                productDetails.Add(new ProductDetailDto
                {
                    AttributeId = productDetail.AttributeId,
                    Value = productDetail.Value
                });
            }
            return new ProductDto
            {
                Id = request.Id,
                Title = request.Title,
                CopyNumber = request.CopyNumber,
                Price = request.Price,
                ProductDetails = productDetails,
                ImagePath = request.ImagePath
            };
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
                ProductDetails = productDetails,
                ImagePath = request.ImagePath
            };
        }
    }
}
