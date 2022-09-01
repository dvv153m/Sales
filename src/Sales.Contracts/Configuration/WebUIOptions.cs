using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Configuration
{
    public class WebUIOptions
    {
        public const string SectionName = "AppSettings";
        
        public string PromoocodeApiUrl { get; set; }

        public string ProductApiUrl { get; set; }
    }
}
