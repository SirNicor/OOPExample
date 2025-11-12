namespace Repository.Migrations;
using FluentMigrator;
using FluentMigrator.Runner;

[Migration(1, "Added address")]
public class M0001AddAddress : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Address")
            .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("Country").AsString()
            .WithColumn("City").AsString()
            .WithColumn("Street").AsString()
            .WithColumn("HouseNumber").AsInt64();
    }
}