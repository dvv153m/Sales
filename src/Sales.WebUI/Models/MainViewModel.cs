using System.ComponentModel.DataAnnotations;

namespace Sales.WebUI.Models
{
    public class MainViewModel
    {
        [Required]
        public string Promocode { get; set; }

        List<ProductViewModel> Products { get; set; }
    }
}
