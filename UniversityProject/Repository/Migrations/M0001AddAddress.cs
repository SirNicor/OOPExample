namespace Repository.Migrations;
using FluentMigrator;
using FluentMigrator.Runner;

[Migration(1, "Added address")]
public class M0001AddAddress : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Address")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("_country").AsString()
            .WithColumn("_city").AsString()
            .WithColumn("_street").AsString()
            .WithColumn("_houseNumber").AsInt64();
    }
}