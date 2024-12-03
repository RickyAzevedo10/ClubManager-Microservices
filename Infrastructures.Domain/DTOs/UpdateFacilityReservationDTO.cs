namespace Infrastructures.Domain.DTOs
{
    public class UpdateFacilityReservationDTO
    {
        public long Id { get; set; }
        public long FacilityId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string EventType { get; set; }
        public string EventDescription { get; set; }
        public long ReservedUserId { get; set; }
    }
}
