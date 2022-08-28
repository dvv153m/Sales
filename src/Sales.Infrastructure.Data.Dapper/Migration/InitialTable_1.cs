using FluentMigrator;


namespace Sales.Infrastructure.Data.Dapper.Migration
{
    [Migration(1)]
    public class InitialTable_1 : FluentMigrator.Migration
    {
        public override void Down()
        {
            Delete.Table("Promocodes");
        }

        public override void Up()
        {
            Create.Table("Promocodes")
           .WithColumn("Id").AsInt64().NotNullable().PrimaryKey()
           .WithColumn("Value").AsString(50).NotNullable()
           .WithColumn("CreatedDate").AsDateTime().NotNullable();           
        }
    }
}
