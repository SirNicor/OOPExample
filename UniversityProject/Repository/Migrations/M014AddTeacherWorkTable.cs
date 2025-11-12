using FluentMigrator;

namespace Repository.Migrations;
using Migrations;

[Migration(14, "Create new table Teacher")]  
public class M014AddTeacherWorkTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Teacher")
            .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("Salary").AsInt64().NotNullable()
            .WithColumn("CriminalRecord").AsBoolean()
            .WithColumn("PassportID").AsInt64().NotNullable().ForeignKey("Passport", "Id")
            .WithColumn("MilitaryID").AsInt64().NotNullable().ForeignKey("IdMilitary", "Id");
        Create.Table("DisciplineOfTeacher")
            .WithColumn("IdTeacher").AsInt64().NotNullable().ForeignKey("Teacher", "Id")
            .WithColumn("IdDiscipline").AsInt64().NotNullable().ForeignKey("Discipline", "Id");    
        Create.PrimaryKey("PK_DisciplineOfTeacher").OnTable("DisciplineOfTeacher").Columns("IdTeacher", "IdDiscipline");
        for(int i = 0; i<5; ++i)
        {
            Insert.IntoTable("Passport")
                .Row(new
                {
                    Serial = "200" + i, Number = "00000" + i, FirstName = $"ad{i}f", MiddleName = $"ad{i}m", LastName = $"ad{i}l",
                    BirthData = "1990-01-01", AddressId = i+1, PlaceReceipt = "1"
                });
        }
        Random random = new Random();
        for (int i = 0; i < 5; i++)
        {
            Insert.IntoTable("Teacher")
                .Row(new { Salary = random.Next(50000, 100000).ToString(), PassportId = i+26, MilitaryId = 1, CriminalRecord = false});
        }

        for (int i = 1; i < 6; ++i)
        {
            Insert.IntoTable("DisciplineOfTeacher")
                .Row(new { IdTeacher = i, IdDiscipline = i })
                .Row(new { IdTeacher = i, IdDiscipline = 2 * i });
        }
    }
}