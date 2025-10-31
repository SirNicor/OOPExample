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
                .WithColumn("_levelID").AsString();
            Insert.IntoTable("IdMillitary")
                .Row(new { _levelID = "DidNotServe" })
                .Row(new { _levelID = "ExcludedFromTheStock" })
                .Row(new { _levelID = "PostponementHealth" })
                .Row(new { _levelID = "PostponementUniversityB" })
                .Row(new { _levelID = "PostponementUniversityS" });
            Create.Table("DegreesStudy")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("_levelDegrees").AsString();
            Insert.IntoTable("DegreesStudy")
                .Row(new { _levelDegrees = "bachelor" })
                .Row(new { _levelDegrees = "doctoral" })
                .Row(new { _levelDegrees = "master" })
                .Row(new { _levelDegrees = "specialization" });
    }
}