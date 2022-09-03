using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
