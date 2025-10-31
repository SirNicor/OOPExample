namespace Repository.Migrations;
using FluentMigrator;

[Migration(3, "Added Passport Table")]
public class M003AddPassportTable : AutoReversingMigration
{
    public override void Up()
    {
            Create.Table("Passport")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("_serial").AsString().Unique()
                .WithColumn("_number").AsString().Unique()
                .WithColumn("_firstName").AsString().NotNullable()
                .WithColumn("_lastName").AsString().NotNullable()
                .WithColumn("_middleName").AsString().NotNullable()
                .WithColumn("_birthDate").AsDate().NotNullable()
                .WithColumn("_addressId").AsInt32().NotNullable().ForeignKey("Address", "Id")
                .WithColumn("_placeReceipt").AsString().NotNullable();
    }
}