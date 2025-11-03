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
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 1
            })
            .Row(new
            {
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 2
            })
            .Row(new
            {
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 3
            })
            .Row(new
            {
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 4
            })
            .Row(new
            {
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 5
            })
            .Row(new
            {
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 6
            })
            .Row(new
            {
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 7
            })
            .Row(new
            {
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 8
            })
            .Row(new
            {
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 9
            })
            .Row(new
            {
                Country = "Russia", City = "Moscow", Street = "Geroyev Panfilovtsev Street", HouseNumber = 10
            });
    }
}