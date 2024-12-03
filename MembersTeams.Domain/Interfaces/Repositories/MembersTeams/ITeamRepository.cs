using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Repositories.Identity
{
    public interface ITeamRepository : IBaseRepository<Team>
    {
        Task<Team?> GetByIdAsync(long id);
        Task<List<Team>?> GetAllPlayerFromTeamAsync(long teamId);
    }
}