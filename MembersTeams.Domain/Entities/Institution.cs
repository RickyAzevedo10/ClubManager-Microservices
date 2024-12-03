namespace MembersTeams.Domain.Entities
{
    public class Institution : BaseEntity
    {
        public string Name { get; set; }
        public long ExternalInstitutionId { get; set; }

        public Institution() { }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetExternalInstitutionId(long externalInstitutionId)
        {
            ExternalInstitutionId = externalInstitutionId;
        }
    }
}
