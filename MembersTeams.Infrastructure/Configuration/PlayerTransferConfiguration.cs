using MembersTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MembersTeams.Infrastructure.Configuration
{
    internal class PlayerTransferConfiguration : IEntityTypeConfiguration<PlayerTransfer>
    {
        public void Configure(EntityTypeBuilder<PlayerTransfer> entity)
        {
            entity.Property(p => p.TransferFee)
                .HasPrecision(18, 2);
        }
    }
}
