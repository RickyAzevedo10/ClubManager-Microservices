using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Infrastructure.Repositories
{
    public class PlayerPerformanceHistoryRepository : BaseRepository<PlayerPerformanceHistory>, IPlayerPerformanceHistoryRepository
    {
        public PlayerPerformanceHistoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<PlayerPerformanceHistory?> GetByIdAsync(long id)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<PlayerPerformanceHistory>?> GetAllPlayerPerformanceHistoryForSeasonAsync(long playerId, string season)
        {
            return await GetEntity().Where(x => x.PlayerId == playerId && x.Season == season).ToListAsync();
        }
    }
}
