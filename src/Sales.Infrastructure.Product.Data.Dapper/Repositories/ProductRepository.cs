﻿using Dapper;
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

        public IEnumerable<ProductEntity> GetAll()
        {
            var query = @"SELECT * FROM Product p 
                          JOIN ProductDetail d 
                          ON p.Id = d.ProductId
                          JOIN Attribute a 
                          ON d.AttributeId = a.Id";

            using (var connection = _dbContext.CreateConnection())
            {
                var productEntities = new List<ProductEntity>();
                var products = connection.Query<ProductEntity, ProductDetailEntity, AttributeEntity, ProductEntity>(
                    query, (product, productDetails, attribute) =>
                    {
                        productDetails.Attribute = attribute;
                        productEntities.Add(product);
                        return product;
                    });

                return productEntities;
            }
        }

        /*SELECT * FROM [Product].[dbo].[Product] p
  JOIN [Product].[dbo].[ProductDetail] d 
  ON p.Id = d.ProductId
  JOIN [Product].[dbo].[Attribute] a
  ON d.AttributeId = a.Id*/
        /*public IEnumerable<ProductDetailEntity> GetAll()
        {
            var query = "SELECT * FROM ProductDetail p JOIN Attribute a ON p.AttributeId = a.Id";

            using (var connection = _dbContext.CreateConnection())
            {
                var productEntities = new List<ProductDetailEntity>();
                var products = connection.Query<ProductDetailEntity, AttributeEntity, ProductDetailEntity>(
                    query, (productDetails, attribute) =>
                    {
                        productDetails.Attribute = attribute;
                        productEntities.Add(productDetails);
                        return productDetails;
                    });

                return productEntities;
            }            
        }*/

        /*using (IDbConnection connection = _dbContext.CreateConnection())
            {
                var res = connection.Query<ProductDetailEntity>("SELECT * FROM ProductDetail");//todo async
                return res;
            }*/
    }
}