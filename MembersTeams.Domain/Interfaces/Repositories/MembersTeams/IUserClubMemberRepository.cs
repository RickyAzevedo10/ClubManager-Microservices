using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Repositories.Identity
{
    public interface IUserClubMemberRepository : IBaseRepository<UserClubMember>
    {
        Task<UserClubMember?> GetByUserIdAsync(long userId);
    }
}