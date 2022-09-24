using FluentMigrator;

namespace Sales.Infrastructure.Order.Data.Dapper.Migrations
{
    [Migration(1)]
    public class CreateOrdersTables : Migration
    {
        public override void Down()
        {
            Delete.Table("OrderDetail");
            Delete.Table("Order");            
        }

        public override void Up()
        {
            Create.Table("Order")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Promocode").AsString().NotNullable()
                  .WithColumn("Date").AsDateTime().NotNullable()
                  .WithColumn("Status").AsInt32().NotNullable()
                  .WithColumn("Price").AsDecimal().NotNullable()
                  .WithColumn("UpdateDate").AsDateTime().NotNullable()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();

            Create.Table("OrderDetail")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("OrderId").AsInt64().NotNullable()
                  .WithColumn("ProductId").AsInt64().NotNullable()
                  .WithColumn("Price").AsDecimal().NotNullable()
                  .WithColumn("Quantity").AsInt32().NotNullable()                                    
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();

            Create.ForeignKey()
            .FromTable("OrderDetail").ForeignColumn("OrderId")
            .ToTable("Order").PrimaryColumn("Id");
        }
    }
}
