using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sales.Infrastructure.Data.Dapper.Context
{
    public class DapperContext
    {
        private readonly IOptions<PromocodeApiConfig> _config;

        public DapperContext(IOptions<PromocodeApiConfig> config)
        {
            if(config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_config.Value.SqlConnection);

        public IDbConnection CreateMasterConnection()
            => new SqlConnection(_config.Value.MasterConnection);
    }
}
