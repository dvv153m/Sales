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

        /*public Task UpdateAsync(ProductEntity entity)
        {
            var updateQuery = @"UPDATE Product SET Title=@Title, CopyNumber=@CopyNumber, Price=@Price 
                                ImagePath=@ImagePath, UpdateDate = @UpdateDate
                                WHERE Id=@Id";

            entity.UpdateDate = DateTime.UtcNow;

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int32);
            parameters.Add("Title", entity.Title, DbType.String);
            parameters.Add("CopyNumber", entity.CopyNumber, DbType.Int32);
            parameters.Add("Price", entity.Price, DbType.Decimal);
            parameters.Add("ImagePath", entity.ImagePath, DbType.String);
            parameters.Add("UpdateDate", entity.UpdateDate, DbType.String);
        }*/

        public async Task AddAsync(ProductEntity entity)
        {            
            var insertProductQuery = @"INSERT INTO Product (Title, CopyNumber, Price, ImagePath, CreatedDate, UpdateDate) 
                                          VALUES (@Title, @CopyNumber, @Price, @ImagePath, @CreatedDate, @UpdateDate)
                                          SELECT CAST(SCOPE_IDENTITY() as int)";

            var insertProductDetailsQuery = @"INSERT INTO ProductDetail (ProductId, AttributeId, Value, CreatedDate) 
                                          VALUES (@ProductId, @AttributeId, @Value, @CreatedDate)";

            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdateDate = DateTime.UtcNow;

            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var productParameters = new DynamicParameters();
                        productParameters.Add("Title", entity.Title, DbType.String);
                        productParameters.Add("CopyNumber", entity.CopyNumber, DbType.Int32);
                        productParameters.Add("Price", entity.Price, DbType.Decimal);
                        productParameters.Add("ImagePath", entity.ImagePath, DbType.String);
                        productParameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime);
                        productParameters.Add("UpdateDate", entity.UpdateDate, DbType.DateTime);

                        var productId = await connection.QuerySingleAsync<int>(insertProductQuery, productParameters, transaction);

                        foreach (var productDetail in entity.ProductDetails)
                        {
                            var productDetailParameters = new DynamicParameters();
                            productDetailParameters.Add("ProductId", productId, DbType.Int64);
                            productDetailParameters.Add("AttributeId", productDetail.AttributeId, DbType.Int64);
                            productDetailParameters.Add("Value", productDetail.Value, DbType.String);
                            productDetailParameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime);

                            await connection.ExecuteAsync(insertProductDetailsQuery, productDetailParameters, transaction);
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            /*using (IDbConnection connection = _dbContext.CreateConnection())
            {
                var sqlQuery = "INSERT INTO Promocode (Value, CreatedDate) VALUES(@Value, @CreatedDate)";
                await connection.ExecuteAsync(sqlQuery, entity);
            }*/
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
                var productDict = new Dictionary<long, ProductEntity>();

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
