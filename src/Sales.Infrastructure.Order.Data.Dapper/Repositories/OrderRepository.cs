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

        public async Task<OrderEntity> AddAsync(OrderEntity entity)
        {
            var insertOrderQuery = @"INSERT INTO Order (Promocode, Date, Status, Price, UpdateDate, CreatedDate) 
                                          VALUES (@Promocode, @Date, @Status, @Price, @UpdateDate, @CreatedDate)
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
                        var orderParameters = new DynamicParameters();
                        orderParameters.Add("Promocode", entity.Promocode, DbType.String);
                        orderParameters.Add("Date", entity.Date, DbType.DateTime);
                        orderParameters.Add("Status", entity.Status, DbType.Int32);
                        orderParameters.Add("Price", entity.Price, DbType.Decimal);
                        orderParameters.Add("UpdateDate", entity.UpdateDate, DbType.DateTime);
                        orderParameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime);                        

                        var orderId = await connection.QuerySingleAsync<int>(insertOrderQuery, orderParameters, transaction);
                        entity.Id = orderId;

                        foreach (var productDetail in entity.OrderDetails)
                        {
                            productDetail.CreatedDate = DateTime.UtcNow;
                            var productDetailParameters = new DynamicParameters();
                            productDetailParameters.Add("OrderId", orderId, DbType.Int64);
                            productDetailParameters.Add("ProductId", productDetail.ProductId, DbType.Int64);
                            productDetailParameters.Add("Quantity", productDetail.Quantity, DbType.Int32);
                            productDetailParameters.Add("Price", productDetail.Quantity, DbType.Decimal);
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
            return entity;
        }

        public OrderEntity GetOrderByPromocodeId(long promocodeId)
        {
            throw new NotImplementedException();
        }
    }
}
