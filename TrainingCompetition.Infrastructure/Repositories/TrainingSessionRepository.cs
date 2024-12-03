using Microsoft.EntityFrameworkCore;
using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Repositories.Identity;
using TrainingCompetition.Infra.Contexts;
using TrainingCompetition.Infra.Persistence;

namespace TrainingCompetition.Infrastructure.Repositories
{
    public class TrainingSessionRepository : BaseRepository<TrainingSession>, ITrainingSessionRepository
    {
        public TrainingSessionRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<TrainingSession>> GetTrainingSessionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await GetEntity()
                .Where(ts => ts.Date >= startDate && ts.Date <= endDate)
                .ToListAsync();
        }
    }
}
