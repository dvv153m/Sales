
namespace Sales.Contracts.Request.Product
{
    public class CreateProductRequest
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
        //public string PhotoPath { get; set; }

        public List<ProductDetailModel> ProductDetails { get; set; }
    }

    public class ProductDetailModel
    {        
        public long AttributeId { get; set; }

        public string Value { get; set; }
    }
}
