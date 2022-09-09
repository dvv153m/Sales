using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Configuration
{
    public class OrderApiOptions
    {
        public const string SectionName = "AppSettings";

        public string ProductApiUrl { get; set; }

        public string PromocodeApiUrl { get; set; }

        public string SqlConnectionString { get; set; }

        public string MasterConnectionString { get; set; }

        public decimal MinimalOrderPrice { get; set; }
    }
}
