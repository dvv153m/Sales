using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Configuration
{
    public class PromocodeApiOptions
    {
        public const string SectionName = "AppSettings";

        public string SqlConnectionString { get; set; }

        public string MasterConnectionString { get; set; }

        public int PromoocodeLenght { get; set; }
    }
}
