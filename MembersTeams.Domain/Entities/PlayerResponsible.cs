using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Entities
{
    public class PlayerResponsible : BaseEntity
    {
        public long PlayerId { get; set; }
        public Player Player { get; set; }
        public long MemberId { get; set; }
        public ClubMember Member { get; set; }
        public string Relationship { get; set; }
        public bool IsPrimaryContact { get; set; }

        public void SetRelationship(string relationship)
        {
            Relationship = relationship;
        }

        public void SetIsPrimaryContact(bool isPrimaryContact)
        {
            IsPrimaryContact = isPrimaryContact;
        }

        public void SetMemberId(long memberId)
        {
            MemberId = memberId;
        }

        public void SetPlayerId(long playerId)
        {
            PlayerId = playerId;
        }
    }
}
