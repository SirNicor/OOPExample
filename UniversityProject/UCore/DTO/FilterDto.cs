namespace UCore;

public class FilterDto
{
        public DateTime? FilterBirthDayStart { get; set; }
        public DateTime? FilterBirthDayEnd { get; set; }
        public long? FilterSkipHoursStart { get; set; }
        public long? FilterSkipHoursEnd { get; set; }
        public long? FilterCourse { get; set; }
        public long? FilterTotalScore { get; set; }
}