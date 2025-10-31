namespace Repository.Migrations;
using FluentMigrator;

[Migration(4, "Added Student Table")]
public class M004AddStudentTable : AutoReversingMigration
{
    public override void Up()
    {
            Create.Table("Student")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("_passportID").AsInt32().Unique().NotNullable().ForeignKey("Passport", "Id")
                .WithColumn("_millitaryID").AsInt32().NotNullable().ForeignKey("IdMillitary", "Id")
                .WithColumn("_criminalRecord").AsBoolean().NotNullable()
                .WithColumn("_courseID").AsInt16().NotNullable()
                .WithColumn("_skipHours").AsInt32().Nullable()
                .WithColumn("_countOfExamsPassed").AsInt32().Nullable()
                .WithColumn("_creditScores").AsInt32().Nullable();
    }
}