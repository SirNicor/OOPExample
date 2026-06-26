using System.Data;
using FluentMigrator;

namespace Repository.Migrations;
using Migrations;

[Migration(11, "Create new table Department")]  
public class M011AddDepartmentTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Department")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey()
            .WithColumn("FacultyId").AsInt64().NotNullable().ForeignKey("University", "Id")
            .WithColumn("NameDepartment").AsString(100).NotNullable();
        Create.Table("AdministrationOfDepartment")
            .WithColumn("DepartmentId").AsInt64().ForeignKey("Department", "Id").OnDelete(Rule.Cascade)
            .WithColumn("AdministratorId").AsInt64().ForeignKey("Administrator", "Id");
        Create.PrimaryKey("PK_AdministrationOfDepartmentOffice").OnTable("AdministrationOfDepartment").Columns("DepartmentId", "AdministratorId");
        Insert.IntoTable("Department")
            .Row(new {FacultyId = 1, NameDepartment = "IIST"});
        Insert.IntoTable("AdministrationOfDepartment")
            .Row(new {DepartmentId = 1, AdministratorId = 9});
    }
}