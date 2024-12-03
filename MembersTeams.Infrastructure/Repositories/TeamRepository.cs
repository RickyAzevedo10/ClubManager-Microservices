using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Infrastructure.Repositories
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(DataContext context) : base(context)
        {
        }

        public async Task<Team?> GetByIdAsync(long id)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Team>?> GetAllPlayerFromTeamAsync(long teamId)
        {
            return await GetEntity().Where(x => x.Id == teamId)
                .Include(x => x.TeamPlayers)
                .ThenInclude(x => x.Player)
                .ToListAsync();
        }

    }
}
