using Contracts;
using Financial.Domain.Entities;

namespace Financial.Domain.Interfaces.Identity
{
    public interface IMembersService
    {
        Task<ClubMember?> Create(CreateUpdateClubMember createClubMember);
        Task<ClubMember?> Delete(ClubMember clubMember);
        Task<ClubMember?> Update(CreateUpdateClubMember clubMemberToUpdate, ClubMember clubMember);
    }
}