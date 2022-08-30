using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Configuration
{
    public class WebUIConfig
    {
        public const string SectionName = "AppSettings";

        public int PromoocodeLenght { get; set; }
    }
}
