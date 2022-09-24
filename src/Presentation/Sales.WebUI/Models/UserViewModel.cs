using System.ComponentModel.DataAnnotations;

namespace Sales.WebUI.Models
{
    public class UserViewModel
    {
        [Required]
        public string Promocode { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
