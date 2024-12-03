using MembersTeams.Domain.Entities;

namespace MembersTeams.Application.Interfaces.Producers
{
    public interface ITeamProducer
    {
        Task CreateUpdateTeamProducer(Team team);
        Task DeleteTeamProducer(long teamId);
    }
}