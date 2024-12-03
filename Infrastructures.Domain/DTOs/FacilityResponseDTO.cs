namespace Infrastructures.Domain.DTOs
{
    public class FacilityResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int FacilityCategoryId { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public FacilityCategoryResponseDTO FacilityCategory { get; set; }
    }
}
