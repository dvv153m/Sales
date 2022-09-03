using FluentMigrator;

namespace Sales.Infrastructure.Product.Data.Dapper.Migration
{
    [Migration(1)]
    public class CreateProductTables : FluentMigrator.Migration
    {
        public override void Down()
        {
            Delete.Table("Attribute");
        }

        public override void Up()
        {
            Create.Table("Attribute")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Name").AsString(50).NotNullable().Unique()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();

            Create.Table("Product")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Title").AsString(150).NotNullable()
                  .WithColumn("CopyNumber").AsInt32().NotNullable()
                  .WithColumn("Price").AsInt32().NotNullable()
                  .WithColumn("PhotoPath").AsString()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();

            Create.Table("ProductDetail")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("AttributeId").AsInt64().NotNullable()
                  .WithColumn("ProductId").AsInt64().NotNullable()
                  .WithColumn("Value").AsString(150).NotNullable()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();

            Create.ForeignKey() 
            .FromTable("ProductDetail").ForeignColumn("ProductId")
            .ToTable("Product").PrimaryColumn("Id");

            Create.ForeignKey()
            .FromTable("ProductDetail").ForeignColumn("AttributeId")
            .ToTable("Attribute").PrimaryColumn("Id");
        }
    }
}