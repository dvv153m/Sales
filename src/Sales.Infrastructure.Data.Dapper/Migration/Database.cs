using Dapper;
using Sales.Infrastructure.Promocode.Data.Dapper.Context;

namespace Sales.Infrastructure.Promocode.Data.Dapper.Migration
{
    public class Database
    {
        private readonly DapperContext _context;

        public Database(DapperContext context)
        {
            if(context == null)
                throw new ArgumentNullException(nameof(context));

            _context = context;
        }
        public void CreateDatabaseIfNotExists()
        {
            string dbName = GetDatabaseName();

            var query = "SELECT * FROM sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);
            using (var connection = _context.CreateMasterConnection())
            {                
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }

        private string GetDatabaseName()
        {            
            using (var connection = _context.CreateConnection())
            {
                return connection.Database;
            }
        }
    }
}
