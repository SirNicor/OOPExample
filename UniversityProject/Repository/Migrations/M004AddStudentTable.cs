using System.Data;

namespace Repository.Migrations;
using FluentMigrator;

[Migration(4, "Added Student Table")]
public class M004AddStudentTable : AutoReversingMigration
{
    public override void Up()
    {
            Create.Table("Student")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("PassportID").AsInt32().Unique().NotNullable().ForeignKey("Passport", "Id").OnDelete(Rule.Cascade)
                .WithColumn("MillitaryID").AsInt32().ForeignKey("IdMillitary", "Id")
                .WithColumn("CriminalRecord").AsBoolean().NotNullable()
                .WithColumn("CourseID").AsInt16().NotNullable()
                .WithColumn("SkipHours").AsInt32().Nullable()
                .WithColumn("CountOfExamsPassed").AsInt32().Nullable()
                .WithColumn("CreditScores").AsInt32().Nullable();
    }
}