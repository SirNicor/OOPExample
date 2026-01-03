namespace UCore;

public class Schedule
{
    public int Id { get; set; }
    public Direction Direction { get; set; }
    public Discipline Discipline { get; set; }
    public Teacher Teacher { get; set; }
    public DataWeekForSchedule DataWeek { get; set; }
    public string  StartCouple { get; set; }
    public string EndCouple { get; set; }
}

public enum DataWeekForSchedule
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}