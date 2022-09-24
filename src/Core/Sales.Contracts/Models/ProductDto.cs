using Sales.Contracts.Request.Product;

namespace Sales.Contracts.Models
{
    public class ProductDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Кол-во экземпляров, участвующих в распродаже
        /// </summary>
        public int CopyNumber { get; set; }

        /// <summary>
        /// Цена за единицу товара
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Путь к фотографии
        /// </summary>
        public string ImagePath { get; set; }

        public List<ProductDetailDto> ProductDetails { get; set; }
    }

    public class ProductDetailDto
    {
        public string Attribute { get; set; }

        public long AttributeId { get; set; }

        public string Value { get; set; }
    }
}
