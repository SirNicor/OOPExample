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
                    _serial = "000" + i, _number = "00000" + i, _firstName = $"st{i}f", _middleName = $"st{i}m", _lastName = $"st{i}l",
                    _birthDate = "2007-01-01", _addressId = i+1, _placeReceipt = "1"
                });
        }
    }
}