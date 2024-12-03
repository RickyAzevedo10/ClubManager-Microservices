using MembersTeams.Domain.DTOs;

namespace MembersTeams.Application.Interfaces
{
    public interface ITeamAppService
    {
        Task<List<TeamResponseDTO>?> GetTeams();
        Task<List<TeamResponseDTO>?> GetAllPlayersFromTeam(long teamId);
        Task<TeamResponseDTO?> DeleteTeam(long id);
        Task<TeamResponseDTO?> CreateTeam(CreateTeamDTO teamBody);
        Task<TeamResponseDTO?> UpdateTeam(UpdateTeamDTO teamToUpdate);
    }
}
