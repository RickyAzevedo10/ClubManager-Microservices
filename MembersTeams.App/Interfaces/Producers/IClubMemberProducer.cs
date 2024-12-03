using MembersTeams.Domain.Entities;

namespace MembersTeams.Application.Interfaces.Producers
{
    public interface IClubMemberProducer
    {
        Task CreateUpdateTeamProducer(ClubMember clubMember);
        Task DeleteClubMemberProducer(long clubMemberId);
    }
}