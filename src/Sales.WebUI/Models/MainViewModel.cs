using System.ComponentModel.DataAnnotations;

namespace Sales.WebUI.Models
{
    public class MainViewModel
    {
        [Required]
        public string Promocode { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
