using Microsoft.EntityFrameworkCore;
using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Repositories.Identity;
using TrainingCompetition.Infra.Contexts;
using TrainingCompetition.Infra.Persistence;

namespace TrainingCompetition.Infrastructure.Repositories
{
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        public MatchRepository(DataContext context) : base(context)
        {
        }

        public async Task<Match?> GetByIdAsync(long id)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Match>?> GetMachesByTeamId(long teamId)
        {
            return await GetEntity().Where(x => x.TeamId == teamId)
                //.Include(x => x.Team) //TODO
                .ToListAsync();
        }
    }
}
