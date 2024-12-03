using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Repositories.Identity;

namespace TrainingCompetition.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        //TrainingCompetition
        IMatchRepository MatchRepository { get; }
        IMatchStatisticRepository MatchStatisticRepository { get; }
        ITrainingAttendanceRepository TrainingAttendanceRepository { get; }
        ITrainingSessionRepository TrainingSessionRepository { get; }
        IBaseRepository<Player> PlayerRepository { get; }
        IBaseRepository<Team> TeamRepository { get; }

    }
}