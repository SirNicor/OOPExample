using FluentMigrator;

namespace Repository.Migrations;

[Migration(5, "Insert Default Address")]
public class M005InsertDefaultAddress : AutoReversingMigration
{
    public override void Up()
    {
        Insert.IntoTable("Address")
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 1
            })
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 2
            })
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 3
            })
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 4
            })
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 5
            })
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 6
            })
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 7
            })
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 8
            })
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 9
            })
            .Row(new
            {
                _country = "Russia", _city = "Moscow", _street = "Geroyev Panfilovtsev Street", _houseNumber = 10
            });
    }
}