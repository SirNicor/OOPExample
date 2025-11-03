namespace Repository.Migrations;
using FluentMigrator;

[Migration(6, "Insert Default Passport")]
public class M006InsertDefaultPassport : AutoReversingMigration
{
    public override void Up()
    {
        for(int i = 0; i<10; ++i)
        {
            Insert.IntoTable("Passport")
                .Row(new
                {
                    Serial = "000" + i, Number = "00000" + i, FirstName = $"st{i}f", MiddleName = $"st{i}m", LastName = $"st{i}l",
                    BirthData = "2007-01-01", AddressId = i+1, PlaceReceipt = "1"
                });
        }
    }
}