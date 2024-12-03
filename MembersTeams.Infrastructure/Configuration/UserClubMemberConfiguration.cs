using MembersTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MembersTeams.Infrastructure.Configuration
{
    internal class UserClubMemberConfiguration : IEntityTypeConfiguration<UserClubMember>
    {
        public void Configure(EntityTypeBuilder<UserClubMember> entity)
        {
            entity.HasOne(ucm => ucm.ClubMember)
                .WithOne(cm => cm.UserClubMember)
                .HasForeignKey<UserClubMember>(ucm => ucm.ClubMemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
