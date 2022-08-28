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

        public string SqlConnection { get; set; }

        public string MasterConnection { get; set; }
    }
}
