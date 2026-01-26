namespace UCore;

public class ScheduleDto
{
    public long Id { get; set; }
    public long DirectionId { get; set; }
    public long DisciplineId { get; set; }
    public long TeacherId { get; set; }
    public DataWeekForSchedule DataWeek { get; set; }
    public string  StartCouple { get; set; }
    public string EndCouple { get; set; }
}