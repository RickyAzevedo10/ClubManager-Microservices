namespace TrainingCompetition.Domain.DTOs
{
    public class CreateTrainingAttendanceDTO
    {
        public long TrainingSessionId { get; set; }
        public long PlayerId { get; set; }
        public bool IsPresent { get; set; }
        public string? AbsenceReason { get; set; }
    }
}
