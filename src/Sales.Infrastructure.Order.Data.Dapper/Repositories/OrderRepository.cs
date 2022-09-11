﻿using Dapper;
using Sales.Contracts.Entity.Order;
using Sales.Core.Interfaces.Repositories;
using Sales.Infrastructure.Data.Context;
using System.Data;

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

        public async Task AddProductToOrder(OrderDetailsEntity entity)
        {
            var insertProductDetailsQuery = @"INSERT INTO OrderDetail (OrderId, ProductId, Quantity, Price, CreatedDate) 
                                          VALUES (@OrderId, @ProductId, @Quantity, @Price, @CreatedDate)";

            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                connection.Open();

                entity.CreatedDate = DateTime.UtcNow;
                var productDetailParameters = new DynamicParameters();
                productDetailParameters.Add("OrderId", entity.OrderId, DbType.Int64);
                productDetailParameters.Add("ProductId", entity.ProductId, DbType.Int64);
                productDetailParameters.Add("Quantity", entity.Quantity, DbType.Int32);
                productDetailParameters.Add("Price", entity.Price, DbType.Decimal);
                productDetailParameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime);

                await connection.ExecuteAsync(insertProductDetailsQuery, productDetailParameters);
            }
        }

        public async Task<OrderEntity> AddAsync(OrderEntity entity)
        {
            var insertOrderQuery = @$"INSERT INTO [{_databaseName}].[dbo].[Order] (Promocode, Date, Status, Price, UpdateDate, CreatedDate) 
                                          VALUES (@Promocode, @Date, @Status, @Price, @UpdateDate, @CreatedDate)
                                          SELECT CAST(SCOPE_IDENTITY() as int)";

            var insertOrderDetailsQuery = @$"INSERT INTO [{_databaseName}].[dbo].[OrderDetail] (OrderId, ProductId, Quantity, Price, CreatedDate) 
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

                        foreach (var orderDetail in entity.OrderDetails)
                        {
                            orderDetail.CreatedDate = DateTime.UtcNow;
                            var productDetailParameters = new DynamicParameters();
                            productDetailParameters.Add("OrderId", orderId, DbType.Int64);
                            productDetailParameters.Add("ProductId", orderDetail.ProductId, DbType.Int64);
                            productDetailParameters.Add("Quantity", orderDetail.Quantity, DbType.Int32);
                            productDetailParameters.Add("Price", orderDetail.Price, DbType.Decimal);
                            productDetailParameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime);

                            await connection.ExecuteAsync(insertOrderDetailsQuery, productDetailParameters, transaction);
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

        public async Task<OrderEntity> GetOrderByPromocodeAsync(string promocode)
        {
            try
            {
                var selectOrderQuery = @$"SELECT * FROM [{_databaseName}].[dbo].[Order] amd
                                     JOIN [{_databaseName}].[dbo].[OrderDetail] tel
                                     ON amd.Id = tel.OrderId
                                     WHERE Promocode= '{promocode}'";

                using (var connection = _dbContext.CreateConnection())
                {                    
                    var orderDict = new Dictionary<long, OrderEntity>();
                    var orderEntities = new List<OrderEntity>();

                    var products = await connection.QueryAsync<OrderEntity, OrderDetailsEntity, OrderEntity>(
                        selectOrderQuery, (order, orderDetails) =>
                        {
                            if (!orderDict.TryGetValue((long)order.Id, out var currentOrder))
                            {
                                currentOrder = order;
                                currentOrder.OrderDetails = new List<OrderDetailsEntity>();
                                orderDict.Add(order.Id, order);
                            }

                            order.OrderDetails.Add(orderDetails);

                            return order;
                        });

                    return orderDict.Values.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new OrderEntity());
            }

            /*var selectOrderQuery = @"SELECT * FROM Order WHERE Promocode= @Promocode";
            var selectOrderDetailQuery = @"SELECT * FROM OrderDetail WHERE OrderId= @orderId";

            using (var connection = _dbContext.CreateConnection())
            {
                var order = await connection.QuerySingleOrDefaultAsync<OrderEntity>(selectOrderQuery, new { promocode});                

                await connection.QueryAsync<List<OrderDetailsEntity>>(selectOrderDetailQuery, new { promocode });
            }*/
        }

        public async Task UpdateAsync(OrderEntity entity)
        {
            var updateQuery = @"UPDATE [{_databaseName}].[dbo].[Order] SET Promocode=@Promocode, Status=@Status, Price=@Price, @UpdateDate,                                
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

        public async Task UpdateOrderDetailAsync(OrderDetailsEntity entity)
        {
            var updateQuery = @"UPDATE [{_databaseName}].[dbo].[Order] SET Quantity=@Quantity, Price=@Price
                                WHERE Id=@Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id, DbType.Int64);
            parameters.Add("Quantity", entity.Quantity, DbType.String);
            parameters.Add("Price", entity.Price, DbType.Decimal);            

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(updateQuery, parameters);
            }
        }
    }
}
