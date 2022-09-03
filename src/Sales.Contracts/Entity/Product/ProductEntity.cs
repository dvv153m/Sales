using Sales.Contracts.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string CopyNumber { get; set; }

        /// <summary>
        /// Цена за единицу товара
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Путь к фотографии
        /// </summary>
        public string PhotoPath { get; set; }    

        
        //public List<ProductDetailEntity> ProductDetails { get; set; } // возможно перенести это в доменный объект
    }
}
