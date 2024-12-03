namespace MembersTeams.Domain.Entities
{
    public class TeamCoach : BaseEntity
    {
        public long TeamId { get; set; }
        public Team Team { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public bool IsHeadCoach { get; set; }

        public void SetTeamId(long teamId)
        {
            TeamId = teamId;
        }

        public void SetUserId(long userId)
        {
            UserId = userId;
        }

        public void SetIsHeadCoach(bool isHeadCoach)
        {
            IsHeadCoach = isHeadCoach;
        }
    }
}
