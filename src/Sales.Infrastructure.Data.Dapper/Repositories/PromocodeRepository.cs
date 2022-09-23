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
            entity.CreatedDate = DateTime.UtcNow;
            entity.Role = "User";
            using (IDbConnection connection = _dbContext.CreateConnection())
            {                
                var sqlQuery = "INSERT INTO Promocode (Value, Role, CreatedDate) VALUES(@Value, @Role, @CreatedDate)";                
                await connection.ExecuteAsync(sqlQuery, entity);                
            }
        }

        public async Task<PromocodeEntity> GetByPromocodeAsync(string promocode, CancellationToken cancellationToken = default)
        {
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                var query = "SELECT * FROM Promocode WHERE Value = @Value";
                var parameters = new DynamicParameters();
                parameters.Add("Value", promocode);                
                var cmd = new CommandDefinition(query, parameters: parameters, cancellationToken: cancellationToken);
                var result = await connection.QueryAsync<PromocodeEntity>(cmd);                
                return result.FirstOrDefault();                
            }
        }

        public async Task<IEnumerable<PromocodeEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                var query = "SELECT * FROM Promocode";
                var cmd = new CommandDefinition(query, cancellationToken: cancellationToken);
                return await connection.QueryAsync<PromocodeEntity>("SELECT * FROM Promocode");
            }
        }
    }
}
