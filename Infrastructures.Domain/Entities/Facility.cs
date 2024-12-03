namespace Infrastructures.Domain.Entities
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public long FacilityCategoryId { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public FacilityCategory FacilityCategory { get; set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetFacilityCategoryId(long facilityCategoryId)
        {
            FacilityCategoryId = facilityCategoryId;
        }

        public void SetLocation(string location)
        {
            Location = location;
        }

        public void SetCapacity(int capacity)
        {
            Capacity = capacity;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}
