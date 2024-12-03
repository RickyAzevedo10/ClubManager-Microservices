using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Domain.Interfaces.Repositories.Identity
{
    public interface ITrainingAttendanceRepository : IBaseRepository<TrainingAttendance>
    {
        Task<List<TrainingAttendance>?> GetTrainingAttendanceByPlayerId(long playerId);
    }
}