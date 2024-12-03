using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Domain.Interfaces.Repositories.Identity
{
    public interface IMatchRepository : IBaseRepository<Match>
    {
        Task<Match?> GetByIdAsync(long id);
        Task<List<Match>?> GetMachesByTeamId(long teamId);
    }
}