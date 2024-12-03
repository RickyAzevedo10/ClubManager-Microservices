using MembersTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MembersTeams.Infrastructure.Configuration
{
    internal class PlayerContractConfiguration : IEntityTypeConfiguration<PlayerContract>
    {
        public void Configure(EntityTypeBuilder<PlayerContract> entity)
        {
            entity.Property(p => p.Salary)
            .HasPrecision(18, 2);
        }

    }
}
