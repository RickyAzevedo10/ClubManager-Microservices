namespace Contracts
{
    public class CreateUpdateInstitution
    {
        public string Name { get; set; }
        public long ExternalInstitutionId { get; set; }
    }

    public class DeleteInstitution
    {
        public long ExternalInstitutionId { get; set; }
    }
}
