namespace yevgeller2.DTOs
{
    public class ActivityDurationRecordDTO
    {
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public int DurationInSeconds { get; set; }
        public string RecordType { get; set; }
    }
}