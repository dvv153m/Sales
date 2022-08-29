using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts.Entity
{
    public class PromocodeEntity
    {        
        public long Id { get; set; }

        public string Value { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
