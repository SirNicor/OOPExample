namespace UCore;

public class FilterDto
{
        public string[]? FilterDate {
                get
                { 
                        return _filterDate;
                }
                set
                {
                        _filterDate = value??["", ""];
                }}
        public long? FilterSkipHoursStart { get; set; }
        public long? FilterSkipHoursEnd { get; set; }
        public long? FilterCourse { get; set; }
        public long? FilterTotalScore { get; set; }

        private string[] _filterDate = ["", ""];
}