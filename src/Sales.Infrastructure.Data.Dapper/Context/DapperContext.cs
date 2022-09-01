using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sales.Infrastructure.Data.Dapper.Context
{
    public class DapperContext
    {
        private readonly PromocodeApiOptions _config;

        public DapperContext(IOptions<PromocodeApiOptions> config)
        {
            if(config == null || config.Value == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Value;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_config.SqlConnectionString);

        public IDbConnection CreateMasterConnection()
            => new SqlConnection(_config.MasterConnectionString);
    }
}
