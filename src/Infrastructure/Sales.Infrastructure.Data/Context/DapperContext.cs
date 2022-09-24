using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sales.Infrastructure.Data.Context
{
    public class DapperContext
    {
        private readonly string _sqlConnectionString;
        private readonly string _masterConnectionString;

        public DapperContext(string sqlConnectionString, string masterConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
            _masterConnectionString = masterConnectionString;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_sqlConnectionString);

        public IDbConnection CreateMasterConnection()
            => new SqlConnection(_masterConnectionString);
    }
}
