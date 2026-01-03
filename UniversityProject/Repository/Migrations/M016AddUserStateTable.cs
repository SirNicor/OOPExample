namespace Repository.Migrations;
using System.Data;
using FluentMigrator;


[Migration(16, "Create new table UserState")]
public class M016AddUserStateTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("ListUserState")
            .WithColumn("Id").AsInt16().Unique().Identity()
            .WithColumn("Status").AsString();
        Insert.IntoTable("ListUserState")
            .Row(new { Status = "notInitialized" })
            .Row(new { Status = "forRegistration" })
            .Row(new { Status = "waitingForUniversityInput" })
            .Row(new { Status = "waitingForFacultyInput" })
            .Row(new { Status = "waitingForDepartmentInput" })
            .Row(new { Status = "waitingForDirectionInput" })
            .Row(new { Status = "waitingForLastNameInput" })
            .Row(new { Status = "waitingForFirstNameInput" })
            .Row(new { Status = "fullRegistration" });
        Create.Table("UserState")
           .WithColumn("Id").AsInt64().Identity()
           .WithColumn("ListUserStateId").AsInt16().ForeignKey("ListUserState", "Id")
           .WithColumn("UniversityId").AsInt64().ForeignKey("University", "Id")
           .WithColumn("FacultyId").AsInt64().ForeignKey("Faculty", "Id")
           .WithColumn("DepartmentId").AsInt64().ForeignKey("Department", "Id")
           .WithColumn("DirectionId").AsInt64().ForeignKey("Direction", "Id")
           .WithColumn("StudentId").AsInt64().ForeignKey("Student", "Id")
           .WithColumn("FirstName").AsString()
           .WithColumn("LastName").AsString();
    }
}