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
            .WithColumn("NameFaculty").AsString(100).NotNullable()
            .WithColumn("Dean").AsInt64().NotNullable().ForeignKey("Administrator", "Id")
            .WithColumn("DeputyDean").AsInt64().NotNullable().ForeignKey("Administrator", "Id");
        Create.Table("AdministrationOfDeanOffice")
            .WithColumn("IdFaculty").AsInt64().ForeignKey("Faculty", "Id")
            .WithColumn("IdAdministrator").AsInt64().ForeignKey("Administrator", "Id");
        Create.PrimaryKey("PK_AdministrationOfDeanOffice").OnTable("AdministrationOfDeanOffice").Columns("IdFaculty", "IdAdministrator");
        Insert.IntoTable("Faculty")
            .Row(new {IdUniversity = 1, NameFaculty = "FITU", Dean = 4, DeputyDean = 5});
        Insert.IntoTable("AdministrationOfDeanOffice")
            .Row(new {IdFaculty = 1, IdAdministrator = 6})
            .Row(new {IdFaculty = 1, IdAdministrator = 7});
    }
}