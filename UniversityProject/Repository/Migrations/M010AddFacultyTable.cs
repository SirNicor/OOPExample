using System.Data;

namespace Repository.Migrations;
using FluentMigrator;

[Migration(10, "Create new table Faculty")]  
public class M010AddFacultyTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Faculty")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey()
            .WithColumn("IdUniversity").AsInt64().NotNullable().ForeignKey("University", "Id")
            .WithColumn("NameFaculty").AsString(100).NotNullable();
        Create.Table("AdministrationOfFaculty")
            .WithColumn("IdFaculty").AsInt64().ForeignKey("Faculty", "Id").OnDelete(rule: Rule.Cascade)
            .WithColumn("IdAdministrator").AsInt64().ForeignKey("Administrator", "Id");
        Create.PrimaryKey("PK_AdministrationOfDeanOffice").OnTable("AdministrationOfFaculty").Columns("IdFaculty", "IdAdministrator");
        Insert.IntoTable("Faculty")
            .Row(new {IdUniversity = 1, NameFaculty = "FITU"});
        Insert.IntoTable("AdministrationOfFaculty")
            .Row(new {IdFaculty = 1, IdAdministrator = 6})
            .Row(new {IdFaculty = 1, IdAdministrator = 7});
    }
}