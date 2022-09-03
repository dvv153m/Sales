using Dapper;
using Sales.Contracts.Entity.Product;
using Sales.Core.Interfaces.Repositories;
using Sales.Infrastructure.Data.Context;

namespace Sales.Infrastructure.Product.Data.Dapper.Repositories
{
    public class ProductRepository : IProductRepository
    {        
        private readonly DapperContext _dapperContext;

        public ProductRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext ?? throw new ArgumentNullException(nameof(dapperContext));            
        }
        public IEnumerable<ProductDetailEntity> GetAll()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var res = connection.Query<ProductDetailEntity>("SELECT * FROM ProductDetail");//todo async
                return res;
            }            
        }
    }
}
