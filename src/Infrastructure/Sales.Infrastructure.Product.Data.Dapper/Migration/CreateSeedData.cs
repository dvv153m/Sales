using FluentMigrator;
using FluentMigrator.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Product.Data.Dapper.Migration
{
    [Migration(2)]
    public class CreateSeedData : FluentMigrator.Migration
    {             
        public override void Up()
        {
            InsertIntoAttributeTable();

            InsertIntoProductTable();

            InsertIntoProductDetailTable();            
        }

        private void InsertIntoProductTable()
        {
            Insert.IntoTable("Product")
                  .WithIdentityInsert()
                  .Row(new
                  {
                      Id = 1,
                      Title = "Медный всадник",
                      CopyNumber = 10,
                      Price = 500,
                      ImagePath = "",
                      CreatedDate = DateTime.UtcNow,
                      UpdateDate = DateTime.UtcNow
                  });

            Insert.IntoTable("Product")
                  .WithIdentityInsert()
                  .Row(new
                  {
                      Id = 2,
                      Title = "Станционный смотритель",
                      CopyNumber = 20,
                      Price = 600,
                      ImagePath = "",
                      CreatedDate = DateTime.UtcNow,
                      UpdateDate = DateTime.UtcNow
                  });

            Insert.IntoTable("Product")
                  .WithIdentityInsert()
                  .Row(new
                  {
                      Id = 3,
                      Title = "Анна Каренина",
                      CopyNumber = 30,
                      Price = 700,
                      ImagePath = "",
                      CreatedDate = DateTime.UtcNow,
                      UpdateDate = DateTime.UtcNow
                  });
        }

        private void InsertIntoAttributeTable()
        {
            Insert.IntoTable("Attribute")
                  .WithIdentityInsert()
                  .Row(new
                  {
                      Id = 1,
                      Name = "Автор",
                      CreatedDate = DateTime.UtcNow
                  });

            Insert.IntoTable("Attribute")
                  .WithIdentityInsert()
                  .Row(new
                  {
                      Id = 2,
                      Name = "Год издания",
                      CreatedDate = DateTime.UtcNow
                  });

            Insert.IntoTable("Attribute")
                  .WithIdentityInsert()
                  .Row(new
                  {
                      Id = 3,
                      Name = "ISBN код",
                      CreatedDate = DateTime.UtcNow
                  });
        }

        private void InsertIntoProductDetailTable()
        {
            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      ProductId = 1,
                      AttributeId = 1,
                      Value = "Пушкин",
                      CreatedDate = DateTime.UtcNow
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      ProductId = 1,
                      AttributeId = 2,
                      Value = "2000",
                      CreatedDate = DateTime.UtcNow
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      ProductId = 1,
                      AttributeId = 3,
                      Value = "978-5-699-12014-7",
                      CreatedDate = DateTime.UtcNow
                  });


            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      ProductId = 2,
                      AttributeId = 1,
                      Value = "Пушкин",
                      CreatedDate = DateTime.UtcNow
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      ProductId = 2,
                      AttributeId = 2,
                      Value = "1997",
                      CreatedDate = DateTime.UtcNow
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      ProductId = 2,
                      AttributeId = 3,
                      Value = "571-3-256-18092-2",
                      CreatedDate = DateTime.UtcNow
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      ProductId = 3,
                      AttributeId = 1,
                      Value = "Толстой",
                      CreatedDate = DateTime.UtcNow
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      ProductId = 3,
                      AttributeId = 2,
                      Value = "2004",
                      CreatedDate = DateTime.UtcNow
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      ProductId = 3,
                      AttributeId = 3,
                      Value = "111-2-486-16092-7",
                      CreatedDate = DateTime.UtcNow
                  });
        }

        public override void Down()
        {
            DeleteRowsFromAttributeTable();

            DeleteRowsFromProductDetailTable();            
        }        

        private void DeleteRowsFromAttributeTable()
        {
            Delete.FromTable("Attribute").Row(new
            {
                Id = 1,
                Name = "Автор",
                CreatedDate = DateTime.UtcNow
            });

            Delete.FromTable("Attribute").Row(new
            {
                Id = 2,
                Name = "Год издания",
                CreatedDate = DateTime.UtcNow
            });

            Delete.FromTable("Attribute").Row(new
            {
                Id = 3,
                Name = "ISBN код",
                CreatedDate = DateTime.UtcNow
            });
        }

        private void DeleteRowsFromProductDetailTable()
        {
            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 1,
                Value = "Пушкин",
                CreatedDate = DateTime.UtcNow
            });

            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 2,
                Value = "2000",
                CreatedDate = DateTime.UtcNow
            });

            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 3,
                Value = "978-5-699-12014-7",
                CreatedDate = DateTime.UtcNow
            });


            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 1,
                Value = "Бунин",
                CreatedDate = DateTime.UtcNow
            });

            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 2,
                Value = "1997",
                CreatedDate = DateTime.UtcNow
            });

            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 3,
                Value = "571-3-256-18092-2",
                CreatedDate = DateTime.UtcNow
            });
        }
    }
}
