namespace TrainingCompetition.Domain.DTOs
{
    public class UpdateTrainingAttendanceDTO
    {
        public int Id { get; set; }
        public int TrainingSessionId { get; set; }
        public int PlayerId { get; set; }
        public bool IsPresent { get; set; }
        public string? AbsenceReason { get; set; }
    }
}
