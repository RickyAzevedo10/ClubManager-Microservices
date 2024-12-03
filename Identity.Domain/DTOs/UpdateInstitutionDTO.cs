namespace Identity.Domain.DTOs
{
    public class UpdateInstitutionDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Abbreviation { get; set; }
        public string? Logo { get; set; }
        public DateTime FoundationDate { get; set; }
        public string? Address { get; set; }
        public string? Colors { get; set; }
        public string? StadiumName { get; set; }
        public int StadiumCapacity { get; set; }
        public DateTime StadiumInauguration { get; set; }
        public bool HaveTrainingCenter { get; set; }
        public string? TrainingCenterAddress { get; set; }
        public string? OfficialWebsiteUrl { get; set; }
        public string? SocialMediaLinks { get; set; }
    }
}
