using MembersTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MembersTeams.Infrastructure.Configuration
{
    internal class PlayerCategoryConfiguration : IEntityTypeConfiguration<PlayerCategory>
    {
        public void Configure(EntityTypeBuilder<PlayerCategory> entity)
        {
            entity.HasKey(e => e.Id);
            SeedData(entity);
        }

        public void SeedData(EntityTypeBuilder<PlayerCategory> entity)
        {
            // Verificar se já existem dados
            if (!entity.Metadata.GetSeedData().Any())
            {
                entity.HasData(
                    new PlayerCategory { Id = 1, Name = "Sub-5", Description = "Pré-Petizes" },
                    new PlayerCategory { Id = 2, Name = "Sub-6", Description = "Petizes 1º ano" },
                    new PlayerCategory { Id = 3, Name = "Sub-7", Description = "Petizes 2º ano" },
                    new PlayerCategory { Id = 4, Name = "Sub-8", Description = "Traquinas 1º ano" },
                    new PlayerCategory { Id = 5, Name = "Sub-9", Description = "Traquinas 2º ano" },
                    new PlayerCategory { Id = 6, Name = "Sub-10", Description = "Benjamins 1º ano" },
                    new PlayerCategory { Id = 7, Name = "Sub-11", Description = "Benjamins 2º ano" },
                    new PlayerCategory { Id = 8, Name = "Sub-12", Description = "Infantis 1º ano" },
                    new PlayerCategory { Id = 9, Name = "Sub-13", Description = "Infantis 2º ano" },
                    new PlayerCategory { Id = 10, Name = "Sub-14", Description = "Iniciados 1º ano" },
                    new PlayerCategory { Id = 11, Name = "Sub-15", Description = "Iniciados 2º ano" },
                    new PlayerCategory { Id = 12, Name = "Sub-16", Description = "Juvenis 1º ano" },
                    new PlayerCategory { Id = 13, Name = "Sub-17", Description = "Juvenis 2º ano" },
                    new PlayerCategory { Id = 14, Name = "Sub-18", Description = "Juniores 1º ano" },
                    new PlayerCategory { Id = 15, Name = "Sub-19", Description = "Juniores 2º ano" },
                    new PlayerCategory { Id = 16, Name = "Seniores", Description = "Seniores" }
                );
            }
        }
    }
}
