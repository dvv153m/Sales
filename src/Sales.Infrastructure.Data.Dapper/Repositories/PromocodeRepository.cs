using Dapper;
using Sales.Contracts.Entity;
using Sales.Core.Interfaces.Repositories;
using Sales.Infrastructure.Data.Context;
using System.Data;

namespace Sales.Infrastructure.Promocode.Data.Dapper.Repositories
{
    public class PromocodeRepository : IPromocodeRepository
    {
        private readonly DapperContext _dbContext;

        public PromocodeRepository(DapperContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(PromocodeEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            using (IDbConnection connection = _dbContext.CreateConnection())
            {                
                var sqlQuery = "INSERT INTO Promocode (Value, CreatedDate) VALUES(@Value, @CreatedDate)";                
                await connection.ExecuteAsync(sqlQuery, entity);                
            }
        }

        public async Task<PromocodeEntity> GetByPromocodeAsync(string promocode)
        {
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Value", promocode);
                var result = await connection.QueryAsync<PromocodeEntity>("SELECT * FROM Promocode WHERE Value = @Value", parameters);
                return result.FirstOrDefault();                
            }
        }

        public IEnumerable<PromocodeEntity> GetAll()
        {
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                return connection.Query<PromocodeEntity>("SELECT * FROM Promocode");
            }
        }
    }
}
