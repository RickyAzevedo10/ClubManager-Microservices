namespace MembersTeams.Domain.DTOs
{
    public class UpdatePlayerPerformanceHistoryDTO
    {
        public long? Id { get; set; }
        public long PlayerId { get; set; }
        public string Season { get; set; }
        public string ClubOpponent { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int MinutesPlayed { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
    }
}
