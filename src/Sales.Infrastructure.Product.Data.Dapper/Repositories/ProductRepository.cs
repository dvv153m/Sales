using Dapper;
using Sales.Contracts.Entity;
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

        public async Task AddAsync(ProductEntity entity)
        {
            //transaction
            //add in product table
            //add product details table
            try
            {
                var query = @"INSERT INTO Product (Title, CopyNumber, Price, PhotoPath, CreatedDate) 
                                          VALUES (@Title, @CopyNumber, @Price, @PhotoPath, @CreatedDate)
                                          SELECT CAST(SCOPE_IDENTITY() as int)";

                entity.CreatedDate = DateTime.Now;
                entity.PhotoPath = "123";

                using (IDbConnection connection = _dbContext.CreateConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("Title", entity.Title, DbType.String);
                        parameters.Add("CopyNumber", entity.CopyNumber, DbType.Int32);
                        parameters.Add("Price", entity.Price, DbType.Decimal);
                        parameters.Add("PhotoPath", entity.PhotoPath, DbType.String);
                        parameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime);

                        var productId = await connection.QuerySingleAsync<int>(query, parameters, transaction);

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            { 
            
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
