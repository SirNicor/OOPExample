using System.Data;

namespace Repository.Migrations;
using FluentMigrator;

[Migration(4, "Added Student Table")]
public class M004AddStudentTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Student")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("PassportID").AsInt64().Unique().NotNullable().ForeignKey("Passport", "Id")
            .OnDelete(Rule.Cascade)
            .WithColumn("MilitaryID").AsInt64().ForeignKey("IdMilitary", "Id")
            .WithColumn("CriminalRecord").AsBoolean().NotNullable()
            .WithColumn("CourseID").AsInt64().NotNullable()
            .WithColumn("SkipHours").AsInt64().Nullable()
            .WithColumn("CountOfExamsPassed").AsInt64().Nullable()
            .WithColumn("CreditScores").AsInt64().Nullable()
            .WithColumn("ChatId").AsString().Nullable();
    }
}