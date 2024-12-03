namespace Infrastructures.Domain.Entities
{
    public class MaintenanceRequest : BaseEntity
    {
        public long FacilityId { get; set; }
        public string MaintenanceType { get; set; }
        public string ProblemDescription { get; set; }
        public string Priority { get; set; }
        public DateTime RequestDate { get; set; }
        public long RequestedUserId { get; set; }
        public string Status { get; set; }
        // Relacionamentos com outras tabelas
        public Facility Facility { get; set; }
        
        public User RequestedUser { get; set; }

        public void SetFacilityId(long facilityId)
        {
            FacilityId = facilityId;
        }

        public void SetMaintenanceType(string maintenanceType)
        {
            MaintenanceType = maintenanceType;
        }

        public void SetProblemDescription(string problemDescription)
        {
            ProblemDescription = problemDescription;
        }

        public void SetPriority(string priority)
        {
            Priority = priority;
        }

        public void SetRequestDate(DateTime requestDate)
        {
            RequestDate = requestDate;
        }

        public void SetRequestedUserId(long requestedUserId)
        {
            RequestedUserId = requestedUserId;
        }

        public void SetStatus(string status)
        {
            Status = status;
        }
    }
}
