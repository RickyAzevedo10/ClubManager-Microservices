namespace Infrastructures.Domain.DTOs
{
    public class UpdateFacilityDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long FacilityCategoryId { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
    }
}
