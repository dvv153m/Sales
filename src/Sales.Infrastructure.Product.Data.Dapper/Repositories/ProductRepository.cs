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
        public IEnumerable<ProductDetailEntity> GetAll()
        {
            /*var query = "SELECT * FROM ProductDetail p JOIN Attribute a ON p.AttributeId = a.Id";

            using (var connection = _context.CreateConnection())
            { 
                var products = connection.Query<ProductDetailEntity, AttributeEntity, ProductDetailEntity>
            }*/
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                var res = connection.Query<ProductDetailEntity>("SELECT * FROM ProductDetail");//todo async
                return res;
            }
        }
    }
}
