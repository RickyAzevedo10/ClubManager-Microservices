namespace Infrastructures.Domain.Entities
{
    public class FacilityCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Facility> Facility { get; set; }
    }
}
