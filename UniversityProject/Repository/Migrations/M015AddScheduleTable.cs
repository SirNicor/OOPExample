namespace Repository.Migrations;
using System.Data;
using FluentMigrator;


[Migration(15, "Create new table Schedule")]  
public class M015AddUserStateTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("DataWeekForSchedule")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey()
            .WithColumn("DaysOfWeek").AsString();
        Create.Table("DataСoupleForSchedule")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey()
            .WithColumn("StartCouple").AsString().NotNullable()
            .WithColumn("EndCouple").AsString().NotNullable();
        Insert.IntoTable("DataWeekForSchedule")
            .Row(new { DaysOfWeek = "Monday" })
            .Row(new { DaysOfWeek = "Tuesday" })
            .Row(new { DaysOfWeek = "Wednesday" })
            .Row(new { DaysOfWeek = "Thursday" })
            .Row(new { DaysOfWeek = "Friday" })
            .Row(new { DaysOfWeek = "Saturday" });
        Insert.IntoTable("DataСoupleForSchedule")
            .Row(new { StartCouple = "09:00:00", EndCouple = "10:30:00" })
            .Row(new { StartCouple = "10:45:00", EndCouple = "12:15:00" })
            .Row(new { StartCouple = "13:15:00", EndCouple = "14:45:00" })
            .Row(new { StartCouple = "15:00:00", EndCouple = "16:30:00" })
            .Row(new { StartCouple = "16:30:00", EndCouple = "16:45:00" });

        Create.Table("Schedule")
            .WithColumn("Id").AsInt64().Identity()
            .WithColumn("DirectionId").AsInt64().ForeignKey("Direction", "ID")
            .WithColumn("DisciplineId").AsInt64().ForeignKey("Discipline", "ID")
            .WithColumn("TeacherId").AsInt64().ForeignKey("Teacher", "ID")
            .WithColumn("DataWeekForScheduleId").AsInt32().ForeignKey("DataWeekForSchedule", "ID")
            .WithColumn("DataСoupleForScheduleId").AsInt32().ForeignKey("DataСoupleForSchedule", "Id");
        Create.PrimaryKey("PK_Schedule").OnTable("Schedule").Columns("DirectionId", "DisciplineId", "TeacherId");

        for (int i = 1; i < 11; i++)
        {
            Insert.IntoTable("Schedule")
                .Row(new{DirectionId = 1, DisciplineId = i, TeacherId = i%4 == 0? (i+1)%4 : i%4,  
                    DataWeekForScheduleId = i%6 == 0? (i+1)%6 : i%6, 
                    DataСoupleForScheduleId = i%5 == 0? (i+1)%5 : i%5});
        }
    }
}