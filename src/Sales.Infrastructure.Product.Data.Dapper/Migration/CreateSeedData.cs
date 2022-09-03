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

            InsertIntoProductDetailTable();            
        }        

        private void InsertIntoAttributeTable()
        {
            Insert.IntoTable("Attribute")
                  .WithIdentityInsert()
                  .Row(new
                  {
                      Id = 1,
                      Name = "Автор",
                      CreatedDate = DateTime.Now
                  });

            Insert.IntoTable("Attribute")
                  .WithIdentityInsert()
                  .Row(new
                  {
                      Id = 2,
                      Name = "Год издания",
                      CreatedDate = DateTime.Now
                  });

            Insert.IntoTable("Attribute")
                  .WithIdentityInsert()
                  .Row(new
                  {
                      Id = 3,
                      Name = "ISBN код",
                      CreatedDate = DateTime.Now
                  });
        }

        private void InsertIntoProductDetailTable()
        {
            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      AttributeId = 1,
                      Value = "Пушкин",
                      CreatedDate = DateTime.Now
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      AttributeId = 2,
                      Value = "2000",
                      CreatedDate = DateTime.Now
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      AttributeId = 3,
                      Value = "978-5-699-12014-7",
                      CreatedDate = DateTime.Now
                  });


            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      AttributeId = 1,
                      Value = "Бунин",
                      CreatedDate = DateTime.Now
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      AttributeId = 2,
                      Value = "1997",
                      CreatedDate = DateTime.Now
                  });

            Insert.IntoTable("ProductDetail")
                  .Row(new
                  {
                      AttributeId = 2,
                      Value = "571-3-256-18092-2",
                      CreatedDate = DateTime.Now
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
                CreatedDate = DateTime.Now
            });

            Delete.FromTable("Attribute").Row(new
            {
                Id = 2,
                Name = "Год издания",
                CreatedDate = DateTime.Now
            });

            Delete.FromTable("Attribute").Row(new
            {
                Id = 3,
                Name = "ISBN код",
                CreatedDate = DateTime.Now
            });
        }

        private void DeleteRowsFromProductDetailTable()
        {
            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 1,
                Value = "Пушкин",
                CreatedDate = DateTime.Now
            });

            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 2,
                Value = "2000",
                CreatedDate = DateTime.Now
            });

            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 3,
                Value = "978-5-699-12014-7",
                CreatedDate = DateTime.Now
            });


            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 1,
                Value = "Бунин",
                CreatedDate = DateTime.Now
            });

            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 2,
                Value = "1997",
                CreatedDate = DateTime.Now
            });

            Delete.FromTable("ProductDetail").Row(new
            {
                AttributeId = 3,
                Value = "571-3-256-18092-2",
                CreatedDate = DateTime.Now
            });
        }
    }
}
