using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using Sales.Contracts.Entity;
using Sales.Core.Domain;
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

        public void Add(PromocodeEntity promocode)
        {
            using (IDbConnection db = new SqlConnection(_config.SqlConnectionString))
            {
                var sqlQuery = "INSERT INTO Promocode (Value, CreatedDate) VALUES(@Name, @Age)";
                db.Execute(sqlQuery, promocode);
            }
        }

        public Promocode GetByPromocode(string promocode)
        {
            using (IDbConnection db = new SqlConnection(_config.SqlConnectionString))
            {
                return db.Query<Promocode>("SELECT * FROM Users WHERE Id = @id", new { promocode }).FirstOrDefault();
            }
        }

        public IEnumerable<Promocode> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_config.SqlConnectionString))
            {
                return db.Query<Promocode>("SELECT * FROM Promocode");
            }
        }
    }
}
