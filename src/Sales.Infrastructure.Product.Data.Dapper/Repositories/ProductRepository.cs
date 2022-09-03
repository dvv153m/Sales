using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using Sales.Contracts.Entity.Product;
using Sales.Core.Interfaces.Repositories;
using System.Data;

namespace Sales.Infrastructure.Product.Data.Dapper.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductApiOptions _config;

        public ProductRepository(IOptions<ProductApiOptions> config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Value;
        }
        public IEnumerable<ProductDetailEntity> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_config.SqlConnectionString))
            {
                return db.Query<ProductDetailEntity>("SELECT * FROM ProductDetail");//todo async
            }
        }
    }
}
