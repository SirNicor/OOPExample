using FluentMigrator;

namespace Repository.Migrations;

[Migration(9, "Create new table University")]
public class M009AddUniversityTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("University")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey()
            .WithColumn("NameUniversity").AsString(100).NotNullable()
            .WithColumn("Rector").AsInt32().NotNullable().ForeignKey("Administrator", "Id")
            .WithColumn("Budget").AsString().NotNullable();
        Create.Table("PersonalOfUniversity")
            .WithColumn("IdUniversity").AsInt32().ForeignKey("University", "Id")
            .WithColumn("IdAdministrator").AsInt32().ForeignKey("Administrator", "Id");
        Insert.IntoTable("University")
            .Row(new {NameUniversity = "VUZ", Rector = 1, Budget = 1000000});
        Insert.IntoTable("PersonalOfUniversity")
            .Row(new {IdUniversity = 1, IdAdministrator = 2})
            .Row(new {IdUniversity = 1, IdAdministrator = 3});
    }
}