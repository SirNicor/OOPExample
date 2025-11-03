namespace Repository.Migrations;
using FluentMigrator;

[Migration(3, "Added Passport Table")]
public class M003AddPassportTable : AutoReversingMigration
{
    public override void Up()
    {
            Create.Table("Passport")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Serial").AsString(4)
                .WithColumn("Number").AsString(6)
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("MiddleName").AsString().NotNullable()
                .WithColumn("BirthData").AsDate().NotNullable()
                .WithColumn("AddressId").AsInt32().NotNullable().ForeignKey("Address", "Id")
                .WithColumn("PlaceReceipt").AsString().NotNullable();
            Create.Index("IndexSerialNumber")
                .OnTable("Passport").OnColumn("Serial").Ascending()
                .OnColumn("Number").Ascending()
                .WithOptions().Unique();
    }
}