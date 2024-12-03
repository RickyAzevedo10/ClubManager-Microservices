namespace MembersTeams.Domain.Entities
{
    public class TeamPlayer : BaseEntity
    {
        public long TeamId { get; set; }
        public Team Team { get; set; }
        public long PlayerId { get; set; }
        public Player Player { get; set; }

        public void SetTeamId(long teamId)
        {
            TeamId = teamId;
        }

        public void SetPlayerId(long playerId)
        {
            PlayerId = playerId;
        }
    }
}
