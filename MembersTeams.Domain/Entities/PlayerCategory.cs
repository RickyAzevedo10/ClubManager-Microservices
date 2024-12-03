using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Entities
{
    public class PlayerCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Player> Player { get; set; }
    }
}
