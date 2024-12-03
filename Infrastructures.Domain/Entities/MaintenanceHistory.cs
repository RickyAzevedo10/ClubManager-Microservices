namespace Infrastructures.Domain.Entities
{
    public class MaintenanceHistory : BaseEntity
    {
        public long FacilityId { get; set; }
        public string MaintenanceType { get; set; }
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public long RequestUserId { get; set; }
        // Relacionamentos com outras tabelas
        public Facility Facility { get; set; }
        
        public User RequestUser { get; set; }

        public void SetFacilityId(long facilityId)
        {
            FacilityId = facilityId;
        }

        public void SetMaintenanceType(string maintenanceType)
        {
            MaintenanceType = maintenanceType;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetMaintenanceDate(DateTime maintenanceDate)
        {
            MaintenanceDate = maintenanceDate;
        }

        public void SetRequestUserId(long requestUserId)
        {
            RequestUserId = requestUserId;
        }
    }
}
