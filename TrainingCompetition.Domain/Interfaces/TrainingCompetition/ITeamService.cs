using Contracts;
using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Domain.Interfaces.Identity
{
    public interface ITeamService
    {
        Task<Team?> DeleteTeam(Team? team);
        Task<Team?> CreateTeam(CreateUpdateTeam playerTeamBody);
        Task<Team?> UpdateTeam(CreateUpdateTeam teamToUpdate, Team team);
    }
}