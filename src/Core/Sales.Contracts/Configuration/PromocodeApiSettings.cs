using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Configuration
{
    public class PromocodeApiSettings
    {
        public const string SectionName = "AppSettings";

        public string SqlConnectionString { get; set; }

        public string MasterConnectionString { get; set; }

        public int PromocodeLenght { get; set; }

        public CacheOptions CacheOptions { get; set; }
    }

    public class CacheOptions
    {        
        public TimeSpan AbsoluteExpirationRelativeToNow { get; set; }

        public TimeSpan SlidingExpiration { get; set; }
    }
}
