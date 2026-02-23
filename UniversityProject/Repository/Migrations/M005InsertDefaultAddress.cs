using FluentMigrator;

namespace Repository.Migrations;

[Migration(5, "Insert Default Address")]
public class M005InsertDefaultAddress : AutoReversingMigration
{
    public override void Up()
    {
        for (int i = 0; i < 40; i++)
        {
            Insert.IntoTable("Address")
                .Row(new
                {
                    Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = i
                });
        }
    }
}