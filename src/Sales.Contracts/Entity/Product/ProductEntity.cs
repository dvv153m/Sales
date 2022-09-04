using Sales.Contracts.Entity.Base;

namespace Sales.Contracts.Entity.Product
{
    public class ProductEntity : EntityBase
    {        
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

        /// <summary>
        /// Путь к фотографии
        /// </summary>
        public string ImagePath { get; set; }

        public DateTime UpdateDate { get; set; }

        public List<ProductDetailEntity> ProductDetails { get; set; }
    }
}
