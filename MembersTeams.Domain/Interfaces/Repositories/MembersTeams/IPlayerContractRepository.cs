using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Repositories.Identity
{
    public interface IPlayerContractRepository : IBaseRepository<PlayerContract>
    {
        Task<PlayerContract?> GetByIdAsync(long clubMemberId);
        Task<List<PlayerContract>?> GetAllPlayerContractAsync(long playerId);
    }
}