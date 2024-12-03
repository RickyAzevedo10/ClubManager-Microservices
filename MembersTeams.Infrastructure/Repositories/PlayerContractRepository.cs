using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Infrastructure.Repositories
{
    public class PlayerContractRepository : BaseRepository<PlayerContract>, IPlayerContractRepository
    {
        public PlayerContractRepository(DataContext context) : base(context)
        {
        }

        public async Task<PlayerContract?> GetByIdAsync(long userId)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<PlayerContract>?> GetAllPlayerContractAsync(long playerId)
        {
            return await GetEntity().Where(x => x.PlayerId == playerId).ToListAsync();
        }

    }
}
