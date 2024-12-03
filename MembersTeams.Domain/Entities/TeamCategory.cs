namespace MembersTeams.Domain.Entities
{
    public class TeamCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Team> Team { get; set; }
    }
}
