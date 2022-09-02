using FluentMigrator;

namespace Sales.Infrastructure.Product.Data.Dapper.Migration
{
    [Migration(1)]
    public class AddProductTables_1 : FluentMigrator.Migration
    {
        public override void Down()
        {
            Delete.Table("Product");
        }

        public override void Up()
        {
            Create.Table("Product")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("CopyNumber").AsString(50).NotNullable().Unique()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();
        }
    }
}