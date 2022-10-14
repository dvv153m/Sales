using Dapper;
using Sales.Core.Entity.Order;
using Sales.Core.Interfaces.Repositories;
using Sales.Infrastructure.Data.Context;
using System.Data;
using static Dapper.SqlMapper;

namespace Sales.Infrastructure.Order.Data.Dapper.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _dbContext;
        private readonly string _databaseName;

        public OrderRepository(DapperContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            using (var connection = _dbContext.CreateConnection())
            {
                _databaseName = connection.Database;
            }
        }

        public async Task AddProductToOrder(OrderItemEntity entity)
        {
            var insertProductItemsQuery = @"INSERT INTO OrderItem (OrderId, ProductId, Quantity, Price, CreatedDate) 
                                          VALUES (@OrderId, @ProductId, @Quantity, @Price, @CreatedDate)";

            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                connection.Open();

                entity.CreatedDate = DateTime.UtcNow;
                var orderItemParameters = new DynamicParameters();
                orderItemParameters.Add("OrderId", entity.OrderId, DbType.Int64);
                orderItemParameters.Add("ProductId", entity.ProductId, DbType.Int64);
                orderItemParameters.Add("Quantity", entity.Quantity, DbType.Int32);
                orderItemParameters.Add("Price", entity.Price, DbType.Decimal);
                orderItemParameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime);

                await connection.ExecuteAsync(insertProductItemsQuery, orderItemParameters);
            }
        }

        public async Task<OrderEntity> AddAsync(OrderEntity entity)
        {
            var insertOrderQuery = @$"INSERT INTO [{_databaseName}].[dbo].[Order] (Promocode, Date, Status, Price, UpdateDate, CreatedDate) 
                                          VALUES (@Promocode, @Date, @Status, @Price, @UpdateDate, @CreatedDate)
                                          SELECT CAST(SCOPE_IDENTITY() as int)";

            var insertOrderItemQuery = @$"INSERT INTO [{_databaseName}].[dbo].[OrderItem] (OrderId, ProductId, Quantity, Price, CreatedDate) 
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

                        foreach (var orderItem in entity.OrderItems)
                        {
                            orderItem.CreatedDate = DateTime.UtcNow;
                            var productDetailParameters = new DynamicParameters();
                            productDetailParameters.Add("OrderId", orderId, DbType.Int64);
                            productDetailParameters.Add("ProductId", orderItem.ProductId, DbType.Int64);
                            productDetailParameters.Add("Quantity", orderItem.Quantity, DbType.Int32);
                            productDetailParameters.Add("Price", orderItem.Price, DbType.Decimal);
                            productDetailParameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime);
                            
                            await connection.ExecuteAsync(insertOrderItemQuery, productDetailParameters, transaction);
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

        /// <summary>
        /// Получить текущий заказ по промокоду
        /// </summary>
        /// <param name="promocode"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderEntity>> GetOrdersByPromocodeAsync(string promocode, CancellationToken cancellationToken = default)
        {            
            var selectOrderQuery = @$"SELECT * FROM [{_databaseName}].[dbo].[Order] mainOrder
                                          LEFT JOIN [{_databaseName}].[dbo].[OrderItem] orderItem
                                          ON mainOrder.Id = orderItem.OrderId
                                          WHERE Promocode= '{promocode}'";

            using (var connection = _dbContext.CreateConnection())
            {
                var orderDict = new Dictionary<long, OrderEntity>();
                var orderEntities = new List<OrderEntity>();
                
                var products = await connection.QueryAsync<OrderEntity, OrderItemEntity, OrderEntity>(
                    new CommandDefinition(selectOrderQuery, cancellationToken: cancellationToken), (order, orderItem) =>
                    {
                        if (!orderDict.TryGetValue((long)order.Id, out var currentOrder))
                        {
                            currentOrder = order;
                            currentOrder.OrderItems = new List<OrderItemEntity>();
                            orderDict.Add(order.Id, order);
                        }

                        if (orderItem != null)
                        {                            
                            currentOrder.OrderItems.Add(orderItem);
                        }

                        return currentOrder;
                    });

                return orderDict.Values;
            }
        }

        public async Task UpdateAsync(OrderEntity entity)
        {

            var updateQuery = @$"UPDATE [{_databaseName}].[dbo].[Order] SET Promocode=@Promocode, Status=@Status, Price=@Price, UpdateDate=@UpdateDate
                                WHERE Id=@Id";

            entity.UpdateDate = DateTime.UtcNow;

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int64);
            parameters.Add("Promocode", entity.Promocode, DbType.String);
            parameters.Add("Status", entity.Status, DbType.Int32);
            parameters.Add("Price", entity.Price, DbType.Decimal);
            parameters.Add("UpdateDate", entity.UpdateDate, DbType.DateTime);

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(updateQuery, parameters);
            }
        }

        public async Task UpdateOrderItemAsync(OrderItemEntity entity)
        {

            var updateQuery = @$"UPDATE [{_databaseName}].[dbo].[OrderItem]
                                     SET Quantity=@Quantity, Price=@Price
                                     WHERE Id=@Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int64);
            parameters.Add("Quantity", entity.Quantity, DbType.Int32);
            parameters.Add("Price", entity.Price, DbType.Decimal);

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(updateQuery, parameters);
            }
        }

        public async Task DeleteProductFromOrderAsync(long orderId, long productId)
        {
            var deleteQuery = @$"DELETE FROM [{_databaseName}].[dbo].[OrderItem]
                                     WHERE OrderId=@OrderId AND ProductId=@ProductId";

            var parameters = new DynamicParameters();
            parameters.Add("OrderId", orderId, DbType.Int64);
            parameters.Add("ProductId", productId, DbType.Int64);

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(deleteQuery, parameters);
            }
        }
    }
}
