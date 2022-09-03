using FluentMigrator;
using FluentMigrator.SqlServer;
using System;

namespace Sales.Infrastructure.Product.Data.Dapper.Migration
{
    [Migration(1)]
    public class AddProductTables_1 : FluentMigrator.Migration
    {
        public override void Down()
        {
            Delete.Table("Attribute");
        }

        public override void Up()
        {
            Create.Table("Attribute")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Name").AsString(50).NotNullable().Unique()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();

            /*Create.Table("Product")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Title").AsString(150).NotNullable()
                  .WithColumn("CopyNumber").AsInt32().NotNullable()
                  .WithColumn("Price").AsInt32().NotNullable()
                  .WithColumn("PhotoPath").AsInt32()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();*/            

            Create.Table("ProductDetail")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("AttributeId").AsInt32().NotNullable()
                  //.WithColumn("ProductId").AsInt64().NotNullable()
                  .WithColumn("Value").AsString(150).NotNullable()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();

            /*Create.ForeignKey() 
            .FromTable("ProductDetail").ForeignColumn("ProductId")
            .ToTable("Product").PrimaryColumn("Id");*/

            Create.ForeignKey()
            .FromTable("ProductDetail").ForeignColumn("AttributeId")
            .ToTable("Attribute").PrimaryColumn("Id");

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
                      AttributeId = 2,
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
    }
}