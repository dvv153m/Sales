using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using Sales.Contracts.Entity;
using Sales.Core.Interfaces.Repositories;
using System.Data;

namespace Sales.Infrastructure.Data.Dapper.Repositories
{
    public class PromocodeRepository : IPromocodeRepository
    {
        private readonly PromocodeApiConfig _config;

        public PromocodeRepository(IOptions<PromocodeApiConfig> config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Value;
        }

        public async Task AddAsync(PromocodeEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            using (IDbConnection db = new SqlConnection(_config.SqlConnectionString))
            {                
                var sqlQuery = "INSERT INTO Promocode (Value, CreatedDate) VALUES(@Value, @CreatedDate)";                
                await db.ExecuteAsync(sqlQuery, entity);                
            }
        }

        public async Task<PromocodeEntity> GetByPromocodeAsync(string promocode)
        {
            using (IDbConnection db = new SqlConnection(_config.SqlConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Value", promocode);
                var result = await db.QueryAsync<PromocodeEntity>("SELECT * FROM Promocode WHERE Value = @Value", parameters);
                return result.FirstOrDefault();                
            }
        }

        public IEnumerable<PromocodeEntity> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_config.SqlConnectionString))
            {
                return db.Query<PromocodeEntity>("SELECT * FROM Promocode");
            }
        }
    }
}
