using MembersTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MembersTeams.Infrastructure.Configuration
{
    internal class ClubMemberConfiguration : IEntityTypeConfiguration<ClubMember>
    {
        public void Configure(EntityTypeBuilder<ClubMember> entity)
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(u => u.Email).IsUnique();

            entity.HasOne(c => c.UserClubMember)
                .WithOne(u => u.ClubMember)
                .HasForeignKey<UserClubMember>(u => u.ClubMemberId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
