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
            .WithColumn("UserStatus").AsString();
        Insert.IntoTable("ListUserState")
            .Row(new { UserStatus = "notInitialized" })
            .Row(new { UserStatus = "forRegistration" })
            .Row(new { UserStatus = "waitingForUniversityInput" })
            .Row(new { UserStatus = "waitingForFacultyInput" })
            .Row(new { UserStatus = "waitingForDepartmentInput" })
            .Row(new { UserStatus = "waitingForDirectionInput" })
            .Row(new { UserStatus = "waitingForLastNameInput" })
            .Row(new { UserStatus = "waitingForFirstNameInput" })
            .Row(new { UserStatus = "fullRegistration" });
        Create.Table("UserState")
           .WithColumn("Id").AsInt64().PrimaryKey().NotNullable()
           .WithColumn("ListUserStateId").AsInt16().ForeignKey("ListUserState", "Id").NotNullable()
           .WithColumn("UniversityId").AsInt64().ForeignKey("University", "Id").Nullable()
           .WithColumn("FacultyId").AsInt64().ForeignKey("Faculty", "Id").Nullable()
           .WithColumn("DepartmentId").AsInt64().ForeignKey("Department", "Id").Nullable()
           .WithColumn("DirectionId").AsInt64().ForeignKey("Direction", "Id").Nullable()
           .WithColumn("StudentId").AsInt64().ForeignKey("Student", "Id").Nullable()
           .WithColumn("FirstName").AsString().Nullable()
           .WithColumn("LastName").AsString().Nullable();
    }
}