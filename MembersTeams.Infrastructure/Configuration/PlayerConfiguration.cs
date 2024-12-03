using MembersTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MembersTeams.Infrastructure.Configuration
{
    internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> entity)
        {
            //TODO
            //entity
             //.HasOne(p => p.Entity)
             //.WithOne(e => e.Player)
             //.HasForeignKey<Entity>(e => e.PlayerId)
             //.OnDelete(DeleteBehavior.Cascade);
        }

    }
}
