using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Infrastructure.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(DataContext context) : base(context)
        {
        }

        public async Task<Player?> GetByIdAsync(long id)
        {
            return await GetEntity().Include(x => x.PlayerResponsibles).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Player>> SearchPlayersAsync(string? firstName, string? lastName, string? position)
        {
            var query = GetEntity();

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(p => p.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(p => p.LastName.Contains(lastName));
            }

            if (!string.IsNullOrEmpty(position))
            {
                query = query.Where(p => p.Position.Contains(position));
            }

            return await query.ToListAsync();
        }

    }
}
