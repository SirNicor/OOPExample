using FluentMigrator.Builders;

namespace Repository.Migrations;
using FluentMigrator;

[Migration(8, "Create new table Administrator")]
public class M008AddAdministratorTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Administrator")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("Salary").AsInt64().NotNullable()
            .WithColumn("CriminalRecord").AsBoolean()
            .WithColumn("PassportID").AsInt32().NotNullable().ForeignKey("Passport", "Id")
            .WithColumn("MilitaryID").AsInt16().NotNullable().ForeignKey("IdMilitary", "Id");
        for(int i = 0; i<10; ++i)
        {
            Insert.IntoTable("Passport")
                .Row(new
                {
                    Serial = "100" + i, Number = "00000" + i, FirstName = $"ad{i}f", MiddleName = $"ad{i}m", LastName = $"ad{i}l",
                    BirthData = "1990-01-01", AddressId = i+1, PlaceReceipt = "1"
                });
        }
        for(int i = 0; i<5; ++i)
        {
            Insert.IntoTable("Passport")
                .Row(new
                {
                    Serial = "100" + i, Number = "10000" + i, FirstName = $"ad{i}f", MiddleName = $"ad{i}m", LastName = $"ad{i}l",
                    BirthData = "1990-01-01", AddressId = i+1, PlaceReceipt = "1"
                });
        }
        Random random = new Random();
        for (int i = 0; i < 15; i++)
        {
            Insert.IntoTable("Administrator")
                .Row(new { Salary = random.Next(50000, 100000).ToString(), PassportId = 11+i, MilitaryId = 1, CriminalRecord = false});
        }
    }
}