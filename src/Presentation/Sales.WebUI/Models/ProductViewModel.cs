using Sales.Core.Entity.Product;

namespace Sales.WebUI.Models
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Кол-во экземпляров
        /// </summary>
        public int CopyNumber { get; set; }

        /// <summary>
        /// Цена за единицу товара
        /// </summary>
        public decimal Price { get; set; }

        public List<ProductDetailViewModel> ProductDetails { get; set; }
    }

    public class ProductDetailViewModel
    {
        public string Attribute { get; set; }

        public string Value { get; set; }
    }
}
