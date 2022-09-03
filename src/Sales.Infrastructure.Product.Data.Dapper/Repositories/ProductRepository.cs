using Dapper;
using Sales.Contracts.Entity.Product;
using Sales.Core.Interfaces.Repositories;
using Sales.Infrastructure.Data.Context;
using System.Data;

namespace Sales.Infrastructure.Product.Data.Dapper.Repositories
{
    public class ProductRepository : IProductRepository
    {        
        private readonly DapperContext _dbContext;

        public ProductRepository(DapperContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));            
        }

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            var query = @"SELECT * FROM Product p 
                          JOIN ProductDetail d 
                          ON p.Id = d.ProductId
                          JOIN Attribute a 
                          ON d.AttributeId = a.Id";

            using (var connection = _dbContext.CreateConnection())
            {
                var productDict = new Dictionary<int, ProductEntity>();

                var productEntities = new List<ProductEntity>();
                var products = await connection.QueryAsync<ProductEntity, ProductDetailEntity, AttributeEntity, ProductEntity>(
                    query, (product, productDetails, attribute) =>
                    {
                        if (!productDict.TryGetValue(product.Id, out var currentProduct))
                        {
                            currentProduct = product;
                            currentProduct.ProductDetails = new List<ProductDetailEntity>();                            
                            productDict.Add(currentProduct.Id, currentProduct);
                        }

                        productDetails.Attribute = attribute;
                        currentProduct.ProductDetails.Add(productDetails);  
                        
                        return currentProduct;
                    });

                return productDict.Values.ToList();                
            }
        }        
    }
}
