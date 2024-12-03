using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Identity
{
    public interface ITeamService
    {
        Task<Team?> DeleteTeam(long id);
        Task<Team?> CreateTeam(CreateTeamDTO playerTeamBody);
        Task<List<TeamPlayer>?> CreateTeamPlayer(List<long> teamBody, Team team);
        Task<List<TeamCoach>?> CreateTeamCoach(List<CreateTeamCoachDTO> teamCoachBody, Team team);
        Task<Team?> UpdateTeam(UpdateTeamDTO teamToUpdate, Team team);
        Task<List<TeamPlayer>?> UpdateTeamPlayers(UpdateTeamDTO teamToUpdate, Team team);
        Task<List<TeamCoach>?> UpdateTeamCoaches(UpdateTeamDTO teamToUpdate, Team team);
    }
}