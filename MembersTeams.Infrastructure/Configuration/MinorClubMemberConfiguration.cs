using MembersTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MembersTeams.Infrastructure.Configuration
{
    internal class MinorClubMemberConfiguration : IEntityTypeConfiguration<MinorClubMember>
    {
        public void Configure(EntityTypeBuilder<MinorClubMember> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(u => u.Email).IsUnique();
        }

    }
}
