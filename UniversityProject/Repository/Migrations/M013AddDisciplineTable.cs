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
        Create.Table("DirectionsOfDiscipline")
            .WithColumn("IdDiscipline").AsInt64().ForeignKey("Discipline", "Id")
            .WithColumn("IdDirection").AsInt64().ForeignKey("Direction", "Id");
        Create.PrimaryKey("PK_DirectionsOfDiscipline").OnTable("DirectionsOfDiscipline").Columns("IdDiscipline", "IdDirection");
        for (int i = 0; i < 10; i++)
        {
            Insert.IntoTable("Discipline")
                .Row(new {NameDiscipline = $"Discipline{i}"});
        }

        for (int i = 0; i < 10; ++i)
        {
            Insert.IntoTable("DirectionsOfDiscipline")
                .Row(new { IdDiscipline = i+1, IdDirection = 1 });
        }
    }
}