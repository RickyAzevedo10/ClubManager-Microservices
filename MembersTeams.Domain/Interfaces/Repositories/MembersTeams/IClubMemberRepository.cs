using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Repositories.Identity
{
    public interface IClubMemberRepository : IBaseRepository<ClubMember>
    {
        Task<ClubMember?> GetByIdAsync(long clubMemberId);
        Task<ClubMember?> GetByEmailAsync(string email);
        Task<IEnumerable<ClubMember>> SearchClubMembersAsync(string? firstName, string? lastName);
    }
}