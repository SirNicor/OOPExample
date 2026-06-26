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
            .WithColumn("AddressString").AsString(255).Nullable()
            .WithColumn("Country").AsString().Nullable()
            .WithColumn("City").AsString().Nullable()
            .WithColumn("Street").AsString().Nullable()
            .WithColumn("HouseNumber").AsString().Nullable();
    }
}