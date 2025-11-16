using System.Data;
using FluentMigrator;

namespace Repository.Migrations;

[Migration(9, "Create new table University")]
public class M009AddUniversityTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("University")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey()
            .WithColumn("NameUniversity").AsString(100).NotNullable()
            .WithColumn("Budget").AsString().NotNullable();
        Create.Table("PersonalOfUniversity")
            .WithColumn("IdUniversity").AsInt64().ForeignKey("University", "Id").OnDelete(Rule.Cascade)
            .WithColumn("IdAdministrator").AsInt64().ForeignKey("Administrator", "Id");
        Create.PrimaryKey("PK_PersonalOfUniversity").OnTable("PersonalOfUniversity").Columns("IdUniversity", "IdAdministrator");
        Insert.IntoTable("University")
            .Row(new {NameUniversity = "VUZ", Budget = 1000000});
        Insert.IntoTable("PersonalOfUniversity")
            .Row(new {IdUniversity = 1, IdAdministrator = 2})
            .Row(new {IdUniversity = 1, IdAdministrator = 3});
    }
}