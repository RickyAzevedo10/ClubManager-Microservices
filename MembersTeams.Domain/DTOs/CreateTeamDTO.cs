namespace MembersTeams.Domain.DTOs
{
    public class CreateTeamDTO
    {
        public long TeamCategoryId { get; set; }
        public string Name { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public long ClubId { get; set; }
        public List<CreateTeamCoachDTO> TeamCoachDTO { get; set; }
        public List<long> PlayerId { get; set; }
    }

    public class CreateTeamCoachDTO
    {
        public long UserId { get; set; }
        public bool IsHeadCoach { get; set; }
    }

}
