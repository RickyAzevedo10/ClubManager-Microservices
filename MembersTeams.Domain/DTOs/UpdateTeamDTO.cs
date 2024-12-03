namespace MembersTeams.Domain.DTOs
{
    public class UpdateTeamDTO
    {
        public int? Id { get; set; }
        public int TeamCategoryId { get; set; }
        public string Name { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public int ClubId { get; set; }
        public List<UpdateTeamCoachDTO> TeamCoachDTO { get; set; }
        public List<UpdatePlayersDTO> TeamPlayers { get; set; }
    }

    public class UpdateTeamCoachDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public bool IsHeadCoach { get; set; }
    }

    public class UpdatePlayersDTO
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
    }
}
