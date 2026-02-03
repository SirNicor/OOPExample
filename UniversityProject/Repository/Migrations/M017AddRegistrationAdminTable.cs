namespace Repository.Migrations;
using System.Data;
using FluentMigrator;

[Migration(17, "Create new table RegistrationAdminTable")]
public class M017AddRegistrationAdminTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("RegistrationAdminTable")
            .WithColumn("Id").AsInt64().NotNullable().Identity()
            .WithColumn("Login").AsString(255).NotNullable().Unique().PrimaryKey()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("Password").AsString(255).NotNullable()
            .WithColumn("Phone").AsString(255).NotNullable();
    }
}