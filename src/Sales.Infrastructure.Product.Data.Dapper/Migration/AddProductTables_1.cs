using FluentMigrator;

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
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();            

            Create.Table("ProductDetail")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("AttributeId").AsInt32().NotNullable()
                  .WithColumn("ProductId").AsInt32().NotNullable()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();*/
        }
    }
}