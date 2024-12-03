namespace Infrastructures.Domain.DTOs
{
    public class CreateFacilityDTO
    {
        public string Name { get; set; }
        public long FacilityCategoryId { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
    }
}
