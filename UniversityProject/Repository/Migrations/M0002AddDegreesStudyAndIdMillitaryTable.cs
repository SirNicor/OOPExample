using System.Xml.Schema;

namespace Repository.Migrations;
using FluentMigrator;
using FluentMigrator.Runner;

[Migration(2, "Added Degress Study Table and Millitary Table")]
public class M0002AddDegreesStudyAndIdMillitaryTable : AutoReversingMigration
{
    public override void Up()
    {
            Create.Table("IdMillitary")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("LevelID").AsString();
            Insert.IntoTable("IdMillitary")
                .Row(new { LevelID = "DidNotServe" })
                .Row(new { LevelID = "ExcludedFromTheStock" })
                .Row(new { LevelID = "PostponementHealth" })
                .Row(new { LevelID = "PostponementUniversityB" })
                .Row(new { LevelID = "PostponementUniversityS" });
            Create.Table("DegreesStudy")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("LevelDegrees").AsString();
            Insert.IntoTable("DegreesStudy")
                .Row(new { LevelDegrees = "bachelor" })
                .Row(new { LevelDegrees = "doctoral" })
                .Row(new { LevelDegrees = "master" })
                .Row(new { LevelDegrees = "specialization" });
    }
}