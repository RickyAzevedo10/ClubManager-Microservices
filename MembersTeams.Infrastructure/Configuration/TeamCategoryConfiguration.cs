using MembersTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MembersTeams.Infrastructure.Configuration
{
    internal class TeamCategoryConfiguration : IEntityTypeConfiguration<TeamCategory>
    {
        public void Configure(EntityTypeBuilder<TeamCategory> entity)
        {
            entity.HasKey(e => e.Id);
            SeedData(entity);
        }
        public void SeedData(EntityTypeBuilder<TeamCategory> entity)
        {
            // Verificar se já existem dados
            if (!entity.Metadata.GetSeedData().Any())
            {
                entity.HasData(
                    new TeamCategory { Id = 1, Name = "Pré-Petizes", Description = "Pré-Petizes" },
                    new TeamCategory { Id = 2, Name = "Petizes", Description = "Petizes" },
                    new TeamCategory { Id = 3, Name = "Traquinas", Description = "Traquinas" },
                    new TeamCategory { Id = 4, Name = "Benjamins", Description = "Benjamins" },
                    new TeamCategory { Id = 5, Name = "Infantis", Description = "Infantis" },
                    new TeamCategory { Id = 6, Name = "Iniciados", Description = "Iniciados" },
                    new TeamCategory { Id = 7, Name = "Juvenis", Description = "Juvenis" },
                    new TeamCategory { Id = 8, Name = "Juniores", Description = "Juniores" },
                    new TeamCategory { Id = 9, Name = "Seniores", Description = "Seniores" }
                );
            }
        }
    }
}
