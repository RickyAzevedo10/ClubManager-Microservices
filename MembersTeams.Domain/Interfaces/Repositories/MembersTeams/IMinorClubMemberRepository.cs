using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Repositories.Identity
{
    public interface IMinorClubMemberRepository : IBaseRepository<MinorClubMember>
    {
        Task<MinorClubMember?> GetByIdAsync(long clubMemberId);
        Task<MinorClubMember?> GetByEmailAsync(string email);
        Task<IEnumerable<MinorClubMember>> SearchMinorClubMemberAsync(string? firstName, string? lastName);
    }
}