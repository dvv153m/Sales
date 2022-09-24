using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Configuration
{
    public class ProductApiOptions
    {
        public const string SectionName = "AppSettings";

        public string SqlConnectionString { get; set; }

        public string MasterConnectionString { get; set; }
    }
}
