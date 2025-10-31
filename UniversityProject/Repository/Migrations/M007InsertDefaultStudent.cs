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
                    _passportId = i+1, _millitaryId = 1, _criminalRecord = false, _courseID = 1, _skipHours = 0,
                    _countOfExamsPassed = 0, _creditScores = 0
                });
        }
    }

}