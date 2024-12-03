using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Repositories.Identity;
using TrainingCompetition.Infra.Contexts;
using TrainingCompetition.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TrainingCompetition.Infrastructure.Repositories
{
    public class MatchStatisticRepository : BaseRepository<MatchStatistic>, IMatchStatisticRepository
    {
        public MatchStatisticRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<MatchStatistic>?> GetMatchStatisticsFromMatchID(long matchId)
        {
            return await GetEntity().Where(x => x.MatchId == matchId)
                //.Include(x => x.Player) //TODO
                .ToListAsync();
        }

        public async Task<List<MatchStatistic>?> GetPlayerMatchStatistics(long playerId)
        {
            return await GetEntity().Where(x => x.PlayerId == playerId)
                //.Include(x => x.Player) //TODO
                .ToListAsync();
        }

        public async Task<List<MatchStatistic>?> GetPlayerMatchStatisticsFromMatchId(long playerId, long matchId)
        {
            return await GetEntity().Where(x => x.PlayerId == playerId && x.MatchId == matchId)
                //.Include(x => x.Player)
                .Include(x => x.Match)
                .ToListAsync();
        }
    }
}
