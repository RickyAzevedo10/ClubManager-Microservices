using Microsoft.EntityFrameworkCore;
using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Repositories.Identity;
using TrainingCompetition.Infra.Contexts;
using TrainingCompetition.Infra.Persistence;

namespace TrainingCompetition.Infrastructure.Repositories
{
    public class TrainingAttendanceRepository : BaseRepository<TrainingAttendance>, ITrainingAttendanceRepository
    {
        public TrainingAttendanceRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<TrainingAttendance>?> GetTrainingAttendanceByPlayerId(long playerId)
        {
            return await GetEntity().Where(x => x.PlayerId == playerId)
                .ToListAsync();
        }
    }
}
