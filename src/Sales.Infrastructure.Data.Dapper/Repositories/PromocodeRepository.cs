using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using Sales.Contracts.Entity;
using Sales.Core.Domain;
using Sales.Core.Interfaces.Repositories;
using System.Data;
using System.Xml.Linq;

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
            promocode.CreatedDate = DateTime.Now;
            using (IDbConnection db = new SqlConnection(_config.SqlConnectionString))
            {                
                var sqlQuery = "INSERT INTO Promocode (Value, CreatedDate) VALUES(@Value, @CreatedDate)";
                db.Execute(sqlQuery, promocode);
                //await db.ExecuteAsync(sqlQuery, promocode);
                /*var parameters = new DynamicParameters();
                parameters.Add("Value", promocode.Value);
                parameters.Add("CreatedDate", promocode.CreatedDate);
                var sqlQuery = "INSERT INTO Promocode (Value, CreatedDate) VALUES(@Value, @CreatedDate)";
                db.Execute(sqlQuery, parameters);*/

            }
        }

        public PromocodeEntity GetByPromocode(string promocode)
        {
            using (IDbConnection db = new SqlConnection(_config.SqlConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Value", promocode);
                //var result = await db.QueryAsync<PromocodeEntity>("SELECT * FROM Promocode WHERE Value = @Value", parameters);
                //return result.FirstOrDefault();
                return db.Query<PromocodeEntity>("SELECT * FROM Promocode WHERE Value = @Value", parameters).FirstOrDefault();
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
