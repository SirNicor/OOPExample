using FluentMigrator.Expressions;

namespace Repository.Migrations;
using System.Data;
using FluentMigrator;

[Migration(17, "Create new table AuthorizationTable")]
public class M017AddAuthorizationTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Role")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey()
            .WithColumn("Name").AsString(100).NotNullable();
        Insert.IntoTable("Role").Row(new {Name = "Teacher"})
            .Row(new {Name = "StudentAdministrator"})
            .Row(new {Name = "AdministrationAdministrator"});
        Create.Table("AuthorizationTable")
            .WithColumn("Id").AsInt64().NotNullable().Identity().PrimaryKey()
            .WithColumn("Login").AsString(255).NotNullable().Unique()
            .WithColumn("Email").AsString(255).Nullable()
            .WithColumn("HashPassword").AsString(255).NotNullable()
            .WithColumn("Salt").AsString(255).NotNullable()
            .WithColumn("Phone").AsString(255).Nullable()
            .WithColumn("IdAdmin").AsInt64().ForeignKey("Administrator", "Id").Nullable()
            .WithColumn("IdTeacher").AsInt64().ForeignKey("Teacher", "Id").Nullable()
            .WithColumn("BlackList").AsBoolean().NotNullable().WithDefaultValue(false);
        Create.Index("IX_Login_Email_Phone").OnTable("AuthorizationTable")
            .OnColumn("Login").Ascending()
            .OnColumn("Email").Ascending()
            .OnColumn("Phone").Ascending();
        Create.Table("RoleForAuthorization")
            .WithColumn("IdRole").AsInt32().ForeignKey("Role", "Id").NotNullable()
            .WithColumn("IdUser").AsInt64().ForeignKey("AuthorizationTable", "Id").NotNullable();
        Create.Table("RefreshJWTToken")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey()
            .WithColumn("Token").AsString(255).NotNullable()
            .WithColumn("RevokedAt").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("IdAuthorizationTable").AsInt64().ForeignKey("AuthorizationTable", "Id").NotNullable();
    }
}