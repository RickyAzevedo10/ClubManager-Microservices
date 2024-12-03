namespace TrainingCompetition.Domain.DTOs
{
    public class TrainingAttendanceResponseDTO
    {
        public long Id { get; set; }
        public long TrainingSessionId { get; set; }
        public long PlayerId { get; set; }
        public bool IsPresent { get; set; }
        public string? AbsenceReason { get; set; }
    }
}
