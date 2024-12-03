namespace TrainingCompetition.Domain.Entities
{
    public class TrainingAttendance : BaseEntity
    {
        public long? TrainingSessionId { get; set; }
        public long? PlayerId { get; set; }
        public bool IsPresent { get; set; }
        public string AbsenceReason { get; set; }

        public TrainingSession TrainingSessions { get; set; }

        public Player Player { get; set; }

        public void SetTrainingSessionId(long trainingSessionId)
        {
            TrainingSessionId = trainingSessionId;
        }

        public void SetPlayerId(long playerId)
        {
            PlayerId = playerId;
        }

        public void SetIsPresent(bool isPresent)
        {
            IsPresent = isPresent;
        }

        public void SetAbsenceReason(string absenceReason)
        {
            AbsenceReason = absenceReason;
        }

    }
}
