using Dapper;
using Sales.Contracts.Entity.Order;
using Sales.Core.Interfaces.Repositories;
using Sales.Infrastructure.Data.Context;
using System.Data;

namespace Sales.Infrastructure.Order.Data.Dapper.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _dbContext;

        public OrderRepository(DapperContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /*public Task<OrderEntity> AddAsync(OrderEntity entity)
        {
            var insertOrderQuery = @"INSERT INTO Order (PromocodeId, Date, Status, Price, UpdateDate) 
                                          VALUES (@PromocodeId, @Date, @Status, @Price, @CreatedDate, @UpdateDate)
                                          SELECT CAST(SCOPE_IDENTITY() as int)";

            var insertProductDetailsQuery = @"INSERT INTO OrderDetail (OrderId, ProductId, Quantity, Price, CreatedDate) 
                                          VALUES (@OrderId, @ProductId, @Quantity, @Price, @CreatedDate)";

            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var productParameters = new DynamicParameters();
                        productParameters.Add("Title", entity.PromocodeId, DbType.Int64);
                        productParameters.Add("CopyNumber", entity.Date, DbType.DateTime);
                        productParameters.Add("Price", entity.Status, DbType.Int32);
                        productParameters.Add("ImagePath", entity.ImagePath, DbType.String);
                        productParameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime);
                        productParameters.Add("UpdateDate", entity.UpdateDate, DbType.DateTime);

                        var productId = await connection.QuerySingleAsync<int>(insertProductQuery, productParameters, transaction);
                        entity.Id = productId;

                        foreach (var productDetail in entity.ProductDetails)
                        {
                            productDetail.CreatedDate = DateTime.UtcNow;
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
        }*/

        public OrderEntity GetOrderByPromocodeId(long promocodeId)
        {
            throw new NotImplementedException();
        }
    }
}
