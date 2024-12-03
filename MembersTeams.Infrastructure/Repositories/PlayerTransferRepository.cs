using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Infrastructure.Repositories
{
    public class PlayerTransferRepository : BaseRepository<PlayerTransfer>, IPlayerTransferRepository
    {
        public PlayerTransferRepository(DataContext context) : base(context)
        {
        }

        public async Task<PlayerTransfer?> GetByIdAsync(long id)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<PlayerTransfer>?> GetAllPlayerTransferAsync(long playerId)
        {
            return await GetEntity().Where(x => x.PlayerId == playerId).ToListAsync();
        }

    }
}
