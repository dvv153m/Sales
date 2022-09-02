﻿using FluentMigrator;


namespace Sales.Infrastructure.Promocode.Data.Dapper.Migration
{
    [Migration(1)]
    public class AddPromocodeTable_1 : FluentMigrator.Migration
    {
        public override void Down()
        {
            Delete.Table("Promocode");
        }

        public override void Up()
        {
            Create.Table("Promocode")
                  .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Value").AsString(50).NotNullable().Unique()
                  .WithColumn("CreatedDate").AsDateTime().NotNullable();
        }
    }
}
