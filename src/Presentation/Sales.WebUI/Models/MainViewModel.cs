using System.ComponentModel.DataAnnotations;

namespace Sales.WebUI.Models
{
    public class MainViewModel
    {
        [Required]
        public string Promocode { get; set; }

        /// <summary>
        /// Список всех товаров
        /// </summary>
        public IEnumerable<ProductViewModel> Products { get; set; }

        /// <summary>
        /// Корзина
        /// </summary>
        public IEnumerable<ProductViewModel> Cart { get; set; }
    }
}
