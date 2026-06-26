namespace Repository.Migrations;
using FluentMigrator;

[Migration(7, "Insert Default Student")]
public class M007InsertDefaultStudent : AutoReversingMigration
{
    public override void Up()
    {
        Random random = new Random();
        for(int i = 0; i<40; ++i)
        {
            Insert.IntoTable("Student")
                .Row(new
                {
                    PassportId = i+1, MilitaryId = 1, CriminalRecord = false, CourseId = random.Next(1, 6), SkipHours = random.Next(0, 100),
                    CountOfExamsPassed = 0, CreditScores = 0
                });
        }
    }

}