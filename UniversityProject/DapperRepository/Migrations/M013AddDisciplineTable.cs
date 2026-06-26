using System.Data;
using FluentMigrator;

namespace Repository.Migrations;
using Migrations;

[Migration(13, "Create new table Discipline")]  
public class M013AddDisciplineTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Discipline")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey()
            .WithColumn("NameDiscipline").AsString(100).NotNullable();
        Create.Table("TeacherOfDiscipline")
            .WithColumn("DisciplineId").AsInt64().ForeignKey("Discipline", "Id").OnDelete(Rule.Cascade)
            .WithColumn("TeacherId").AsInt64().ForeignKey("Teacher", "Id");
        Create.PrimaryKey("PK_TeacherOfDiscipline").OnTable("TeacherOfDiscipline").Columns("DisciplineId", "TeacherId");
        for (int i = 0; i < 10; i++)
        {
            Insert.IntoTable("Discipline")
                .Row(new {NameDiscipline = $"Discipline{i}"});
        }

        for (int i = 0; i < 10; ++i)
        {
            Insert.IntoTable("TeacherOfDiscipline")
                .Row(new { DisciplineId = i+1, TeacherId = 1 })
                .Row(new { DisciplineId = i+1, TeacherId = 2 })
                .Row(new { DisciplineId = i+1, TeacherId = 3 })
                .Row(new { DisciplineId = i+1, TeacherId = 4 })
                .Row(new { DisciplineId = i+1, TeacherId = 5 });
            
        }
    }
}