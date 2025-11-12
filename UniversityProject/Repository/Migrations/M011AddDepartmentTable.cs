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
            .WithColumn("NameDepartment").AsString(100).NotNullable()
            .WithColumn("HeadDepartment").AsInt64().NotNullable().ForeignKey("Administrator", "Id");    
        Create.Table("AdministrationOfDepartmentOffice")
            .WithColumn("DepartmentId").AsInt64().ForeignKey("Department", "Id")
            .WithColumn("AdministratorId").AsInt64().ForeignKey("Administrator", "Id");
        Create.PrimaryKey("PK_AdministrationOfDepartmentOffice").OnTable("AdministrationOfDepartmentOffice").Columns("DepartmentId", "AdministratorId");
        Insert.IntoTable("Department")
            .Row(new {FacultyId = 1, NameDepartment = "IIST", HeadDepartment = 8});
        Insert.IntoTable("AdministrationOfDepartmentOffice")
            .Row(new {DepartmentId = 1, AdministratorId = 9});
    }
}