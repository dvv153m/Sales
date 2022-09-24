
namespace Sales.Contracts.Entity
{
    public class PromocodeEntity
    {        
        public long Id { get; set; }

        public string Value { get; set; }

        public string Role { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
