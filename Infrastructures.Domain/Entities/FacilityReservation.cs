namespace Infrastructures.Domain.Entities
{
    public class FacilityReservation : BaseEntity
    {
        public long FacilityId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string EventType { get; set; }
        public string EventDescription { get; set; }
        public long ReservedUserId { get; set; }
        // Relacionamentos com outras tabelas
        public Facility Facility { get; set; }
        
        public User ReservedUser { get; set; }


        public void SetFacilityId(long facilityId)
        {
            FacilityId = facilityId;
        }

        public void SetStartTime(DateTime startTime)
        {
            StartTime = startTime;
        }

        public void SetEndTime(DateTime endTime)
        {
            EndTime = endTime;
        }

        public void SetEventType(string eventType)
        {
            EventType = eventType;
        }

        public void SetEventDescription(string eventDescription)
        {
            EventDescription = eventDescription;
        }

        public void SetReservedUserId(long reservedUserId)
        {
            ReservedUserId = reservedUserId;
        }
    }
}
