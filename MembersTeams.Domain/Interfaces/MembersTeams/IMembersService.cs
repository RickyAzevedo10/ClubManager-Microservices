using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Identity
{
    public interface IMembersService
    {
        Task<ClubMember?> Create(CreateClubMemberDTO createClubMember);
        Task<ClubMember?> Delete(long id);
        Task<MinorClubMember?> DeleteMinorClubMember(long id);
        Task<MinorClubMember?> CreateMinorClubMember(CreateMinorClubMemberDTO createMinorClubMember);
        Task<ClubMember?> Update(UpdateClubMemberDTO clubMemberToUpdate, ClubMember clubMember);
        Task<MinorClubMember?> UpdateMinorClubMember(UpdateMinorClubMemberDTO minorClubMemberToUpdate, MinorClubMember minorClubMember);
        Task<UserClubMember?> Create(long userId, long clubMemberId);
        Task<UserClubMember?> UpdateUserClubMember(long userId, long clubMemberId);
    }
}