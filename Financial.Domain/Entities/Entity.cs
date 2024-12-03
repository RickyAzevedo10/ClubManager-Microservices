namespace Financial.Domain.Entities
{
    public class Entity : BaseEntity
    {
        public bool Internal { get; set; }
        public bool External { get; set; }
        public string? EntityType { get; set; }
        public string? EntityName { get; set; }
        public long? ClubMemberId { get; set; }
        public long? PlayerId { get; set; }
        public virtual Player? Player { get; set; }

        public virtual ClubMember? UserClubMember { get; set; }

        public virtual ICollection<Revenue> Revenues { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }

        public void SetInternal(bool internalValue)
        {
            Internal = internalValue;
        }

        public void SetExternal(bool externalValue)
        {
            External = externalValue;
        }

        public void SetEntityType(string entityType)
        {
            EntityType = entityType;
        }

        public void SetEntityName(string entityName)
        {
            EntityName = entityName;
        }

        public void SetClubMemberId(long? clubMemberId)
        {
            ClubMemberId = clubMemberId;
        }

        public void SetPlayerId(long? playerId)
        {
            PlayerId = playerId;
        }


    }
}
