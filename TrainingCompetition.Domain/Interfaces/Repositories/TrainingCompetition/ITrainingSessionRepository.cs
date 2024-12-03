using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Domain.Interfaces.Repositories.Identity
{
    public interface ITrainingSessionRepository : IBaseRepository<TrainingSession>
    {
        Task<List<TrainingSession>> GetTrainingSessionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}