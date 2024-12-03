namespace TrainingCompetition.Domain.DTOs
{
    public class CreateMatchDTO
    {
        public DateTime Date { get; set; }
        public string Opponent { get; set; }
        public string Location { get; set; }
        public string CompetitionType { get; set; }
        public long TeamId { get; set; }
        public bool IsCanceled { get; set; }
    }
}
