using Infrastructures.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.Infrastructure.Configuration
{
    internal class FacilityCategoryConfiguration : IEntityTypeConfiguration<FacilityCategory>
    {
        public void Configure(EntityTypeBuilder<FacilityCategory> entity)
        {
            entity.HasKey(e => e.Id);
            SeedData(entity);
        }

        public void SeedData(EntityTypeBuilder<FacilityCategory> entity)
        {
            // Verificar se já existem dados
            if (!entity.Metadata.GetSeedData().Any())
            {
                entity.HasData(
                    new FacilityCategory { Id = 1, Name = "Sports Field", Description = "Outdoor or indoor areas designed for sports activities, such as soccer fields, tennis courts, etc." },
                    new FacilityCategory { Id = 2, Name = "Gym", Description = "Fitness centers equipped with exercise machines, weights, and other fitness equipment." },
                    new FacilityCategory { Id = 3, Name = "Meeting Room", Description = "Rooms designated for meetings, conferences, and other business-related gatherings." },
                    new FacilityCategory { Id = 4, Name = "Restroom", Description = "Facilities providing restroom and changing areas, including showers and lockers." },
                    new FacilityCategory { Id = 5, Name = "Office", Description = "Workspaces for administrative tasks, including private offices and open office areas." },
                    new FacilityCategory { Id = 6, Name = "Auditorium", Description = "Large rooms or halls designed for lectures, presentations, and performances." }
                );
            }
        }
    }
}
