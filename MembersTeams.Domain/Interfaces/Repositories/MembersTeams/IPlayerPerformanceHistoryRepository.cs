using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Repositories.Identity
{
    public interface IPlayerPerformanceHistoryRepository : IBaseRepository<PlayerPerformanceHistory>
    {
        Task<PlayerPerformanceHistory?> GetByIdAsync(long id);
        Task<List<PlayerPerformanceHistory>?> GetAllPlayerPerformanceHistoryForSeasonAsync(long playerId, string season);
    }
}