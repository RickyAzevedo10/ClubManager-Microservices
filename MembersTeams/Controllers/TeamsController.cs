using MembersTeams.Application.Interfaces;
using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Interfaces;
using MembersTeams.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembersTeams.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;
        private readonly ITeamAppService _teamAppService;
        
        public TeamsController(INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, ITeamAppService teamAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _teamAppService = teamAppService;
        }

        /// <summary>
        /// get all teams
        /// </summary>
        /// <returns></returns>
        [HttpGet("Teams")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário,Treinador")]
        public async Task<IActionResult> GetTeams()
        {
            List<TeamResponseDTO>? response = await _teamAppService.GetTeams();
            return DomainResult<List<TeamResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all players from a team
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet("Team")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Treinador, Secretário")]
        public async Task<IActionResult> GetAllPlayersFromTeam([FromQuery] long teamId)
        {
            List<TeamResponseDTO>? response = await _teamAppService.GetAllPlayersFromTeam(teamId);
            return DomainResult<List<TeamResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update team
        /// </summary>
        /// <param name="teamToUpdate"></param>
        /// <returns></returns>
        [HttpPut("Team")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Treinador")]
        public async Task<IActionResult> PutTeam([FromBody] UpdateTeamDTO teamToUpdate)
        {
            TeamResponseDTO? response = await _teamAppService.UpdateTeam(teamToUpdate);
            return DomainResult<TeamResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Team")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Treinador")]
        public async Task<IActionResult> DeleteTeam([FromQuery] long id)
        {
            TeamResponseDTO? response = await _teamAppService.DeleteTeam(id);
            return DomainResult<TeamResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// create team
        /// </summary>
        /// <param name="teamBody"></param>
        /// <returns></returns>
        [HttpPost("Team")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Treinador")]
        public async Task<IActionResult> PostTeam([FromBody] CreateTeamDTO teamBody)
        {
            TeamResponseDTO? response = await _teamAppService.CreateTeam(teamBody);
            return DomainResult<TeamResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
    }
}
