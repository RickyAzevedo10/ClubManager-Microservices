namespace MembersTeams.Domain.DTOs
{
    public class TeamResponseDTO
    {
        public long TeamCategoryId { get; set; }
        public string Name { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public long ClubId { get; set; }
        public List<TeamCoachResponseDTO> TeamCoaches { get; set; }
        public List<TeamPlayerResponseDTO> TeamPlayers { get; set; }
    }

    public class TeamCoachResponseDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public bool IsHeadCoach { get; set; }
    }

    public class TeamPlayerResponseDTO
    {
        public long Id { get; set; }
        public long TeamId { get; set; }
        public long PlayerId { get; set; }
    }

}
