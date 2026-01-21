using System.Data;
using FluentMigrator;
using FluentMigrator.Builders;

namespace Repository.Migrations;
using Migrations;

[Migration(14, "Create new table Direction")]  
public class M014AddDirectionTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Direction")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey()
            .WithColumn("DepartmentId").AsInt64().NotNullable().ForeignKey("University", "Id")
            .WithColumn("DegreesStudyId").AsInt64().NotNullable().ForeignKey("DegreesStudy", "Id")
            .WithColumn("NameDirection").AsString(100).NotNullable()
            .WithColumn("ChatId").AsInt64().Nullable();
        Create.Table("StudentOfDirection")
            .WithColumn("DirectionId").AsInt64().ForeignKey("Direction", "Id").OnDelete(Rule.Cascade)
            .WithColumn("StudentId").AsInt64().ForeignKey("Student", "Id").OnDelete(Rule.Cascade);
        Create.PrimaryKey("PK_StudentOfDirection").OnTable("StudentOfDirection").Columns("DirectionId", "StudentId");
        Create.Table("DisciplineOfDirection")
            .WithColumn("DirectionId").AsInt64().ForeignKey("Direction", "Id").OnDelete(Rule.Cascade)
            .WithColumn("DisciplineId").AsInt64().ForeignKey("Discipline", "Id").OnDelete(Rule.Cascade);
        Create.PrimaryKey("PK_DisciplineOfDirection").OnTable("DisciplineOfDirection").Columns("DirectionId", "DisciplineId");
        Insert.IntoTable("Direction")
            .Row(new {DepartmentId = 1, DegreesStudyId = 1, NameDirection = "Direction1", ChatId = -5024216786 });
        for(int i = 1; i<11; i++)
        {
            Insert.IntoTable("StudentOfDirection")
                .Row(new { DirectionId = 1, StudentId = i });
            Insert.IntoTable("DisciplineOfDirection")
                .Row(new { DirectionId = 1, DisciplineId = i });
        }
    }
}