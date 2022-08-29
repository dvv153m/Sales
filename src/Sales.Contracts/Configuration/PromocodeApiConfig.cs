using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Configuration
{
    public class PromocodeApiConfig
    {
        public const string SectionName = "ConnectionStrings";

        public string SqlConnectionString { get; set; }

        public string MasterConnectionString { get; set; }
    }
}
