namespace Infrastructures.Domain.DTOs
{
    public class CreateMaintenanceRequestDTO
    {
        public long FacilityId { get; set; }
        public string MaintenanceType { get; set; }
        public string ProblemDescription { get; set; }
        public string Priority { get; set; }
        public DateTime RequestDate { get; set; }
        public long RequestedUserId { get; set; }
        public string Status { get; set; }
    }
}
