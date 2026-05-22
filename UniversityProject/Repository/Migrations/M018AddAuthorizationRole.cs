using FluentMigrator.Expressions;

namespace Repository.Migrations;
using System.Data;
using FluentMigrator;

[Migration(18, "Create new table AuthorizationRole")]
public class M018AddAuthorizationRole : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("TypeOperationRole")
            .WithColumn("Id").AsInt16().PrimaryKey().Identity()
            .WithColumn("Name").AsString(50).NotNullable().Unique();
        Insert.IntoTable("TypeOperationRole")
            .Row(new { Name = "Create" })
            .Row(new { Name = "Read" })
            .Row(new { Name = "Update" })
            .Row(new { Name = "Delete" })
            .Row(new { Name = "All" });
        Create.Table("AccessPage")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Name").AsString(100).NotNullable().Unique();
        Insert.IntoTable("AccessPage")
            .Row(new { Name = "AdministratorPage" })
            .Row(new { Name = "AdministratorRegister" })
            .Row(new { Name = "StudentPage" })
            .Row(new { Name = "StudentRegister" });
        Create.Table("RoleAccess")
            .WithColumn("IdRole").AsInt32().ForeignKey("Role", "Id").NotNullable()
            .WithColumn("IdTypeOperation").AsInt16().ForeignKey("TypeOperationRole", "Id").NotNullable()
            .WithColumn("IdAccessPage").AsInt64().ForeignKey("AccessPage", "Id").NotNullable();
        Create.Index("Ix_Role_TypeOperation_AccessPage").OnTable("RoleAccess")
            .OnColumn("IdRole").Ascending()
            .OnColumn("IdTypeOperation").Ascending()
            .OnColumn("IdAccessPage").Ascending();
        Insert.IntoTable("RoleAccess")
            .Row(new { IdRole = 1, IdTypeOperation = 2, IdAccessPage = 4 })
            .Row(new { IdRole = 1, IdTypeOperation = 2, IdAccessPage = 3 })
            .Row(new { IdRole = 2, IdTypeOperation = 5, IdAccessPage = 3 })
            .Row(new { IdRole = 3, IdTypeOperation = 2, IdAccessPage = 2 })
            .Row(new { IdRole = 3, IdTypeOperation = 5, IdAccessPage = 1 });
    }   
}