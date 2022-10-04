using Sales.Core.Entity.Product;
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

        public async Task<ProductDto?> GetById(long id)
        {
            ProductEntity productEntity = await _productRepository.GetByIdAsync(id);
            if (productEntity != null)
            {
                ProductDto productDto = Map(productEntity);
                return productDto;
            }
            return null;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
             var entities = await _productRepository.GetAllAsync();
            var productDto = Map(entities);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetByIds(int[] ids)
        {
            var entities = await _productRepository.GetByIdsAsync(ids);
            var productDtos = Map(entities);
            return productDtos;
        }

        public async Task UpdateAsync(UpdateProductRequest entity)
        {
            var productEntity = Map(entity);
            await _productRepository.UpdateAsync(productEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        private ProductEntity Map(UpdateProductRequest request)
        {
            return new ProductEntity
            {
                Id= request.Id,
                Title = request.Title,
                CopyNumber = request.CopyNumber,
                Price = request.Price,                
                ImagePath = request.ImagePath
            };
        }

        private ProductDto Map(ProductEntity request)
        {
            var productDetails = new List<ProductDetailDto>();
            foreach (var productDetail in request.ProductDetails)
            {
                productDetails.Add(new ProductDetailDto
                {
                    AttributeId = productDetail.AttributeId,
                    Attribute = productDetail.Attribute.Name,
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

        private IEnumerable<ProductDto> Map(IEnumerable<ProductEntity> productEntities)
        {
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (var productEntity in productEntities)
            {                 
                productDtos.Add(Map(productEntity));
            }
            return productDtos;
        }
    }
}
