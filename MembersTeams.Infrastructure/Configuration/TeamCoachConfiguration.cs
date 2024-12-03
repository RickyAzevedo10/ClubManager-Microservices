using MembersTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MembersTeams.Infrastructure.Configuration
{
    internal class TeamCoachConfiguration : IEntityTypeConfiguration<TeamCoach>
    {
        public void Configure(EntityTypeBuilder<TeamCoach> entity)
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(tc => tc.Team)
                .WithMany(t => t.TeamCoaches)
                .HasForeignKey(tc => tc.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            //TODO
            //entity.HasOne(tc => tc.User)
            //    .WithMany(u => u.TeamCoaches)
            //    .HasForeignKey(tc => tc.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
