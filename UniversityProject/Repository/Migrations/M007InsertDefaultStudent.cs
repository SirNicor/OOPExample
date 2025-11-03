namespace Repository.Migrations;
using FluentMigrator;

[Migration(7, "Insert Default Student")]
public class M007InsertDefaultStudent : AutoReversingMigration
{
    public override void Up()
    {
        for(int i = 0; i<10; ++i)
        {
            Insert.IntoTable("Student")
                .Row(new
                {
                    PassportId = i+1, MillitaryId = 1, CriminalRecord = false, CourseId = 1, SkipHours = 0,
                    CountOfExamsPassed = 0, CreditScores = 0
                });
        }
    }

}