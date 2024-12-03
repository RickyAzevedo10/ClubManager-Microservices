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
    public class PlayersController : ControllerBase
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;
        private readonly IPlayerAppService _playerAppService;

        public PlayersController(INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, IPlayerAppService playerAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _playerAppService = playerAppService;
        }

        #region Player
        /// <summary>
        /// create player
        /// </summary>
        /// <param name="playerBody"></param>
        /// <returns></returns>
        [HttpPost("Player")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PostPlayer([FromBody] CreatePlayerDTO playerBody)
        {
            PlayerResponseDTO? response = await _playerAppService.CreatePlayer(playerBody);
            return DomainResult<PlayerResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete player
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Player")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> DeletePlayer([FromQuery] long id)
        {
            PlayerResponseDTO? response = await _playerAppService.DeletePlayer(id);
            return DomainResult<PlayerResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get player with search
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        [HttpGet("PlayerSearch")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário")]
        public async Task<IActionResult> SearchPlayers([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? position)
        {
            List<PlayerResponseDTO>? response = await _playerAppService.SearchPlayersAsync(firstName, lastName, position);
            return DomainResult<List<PlayerResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("Players")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário")]
        public async Task<IActionResult> GetPlayer([FromQuery] long playerId)
        {
            PlayerResponseDTO? response = await _playerAppService.GetPlayer(playerId);
            return DomainResult<PlayerResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update player
        /// </summary>
        /// <param name="playerToUpdate"></param>
        /// <returns></returns>
        [HttpPut("Players")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PutPlayers([FromBody] UpdatePlayerDTO playerToUpdate)
        {
            PlayerResponseDTO? response = await _playerAppService.UpdatePlayer(playerToUpdate);
            return DomainResult<PlayerResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        #endregion

        #region PlayerTransfer
        /// <summary>
        /// create PlayerTransfer
        /// </summary>
        /// <param name="playerTransferBody"></param>
        /// <returns></returns>
        [HttpPost("PlayerTransfer")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PostPlayerTransfer([FromBody] CreatePlayerTransferDTO playerTransferBody)
        {
            PlayerTransferResponseDTO? response = await _playerAppService.CreatePlayerTransfer(playerTransferBody);
            return DomainResult<PlayerTransferResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete PlayerTransfer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("PlayerTransfer")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> DeletePlayerTransfer([FromQuery] long id)
        {
            PlayerTransferResponseDTO? response = await _playerAppService.DeletePlayerTransfer(id);
            return DomainResult<PlayerTransferResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all PlayerTransfer
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("PlayerTransfer")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário")]
        public async Task<IActionResult> GetPlayerTransfer([FromQuery] long playerId)
        {
            List<PlayerTransferResponseDTO>? response = await _playerAppService.GetPlayerTransfer(playerId);
            return DomainResult<List<PlayerTransferResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update PlayerTransfer
        /// </summary>
        /// <param name="playerTransferToUpdate"></param>
        /// <returns></returns>
        [HttpPut("PlayerTransfer")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PutPlayerTransfer([FromBody] UpdatePlayerTransferDTO playerTransferToUpdate)
        {
            PlayerTransferResponseDTO? response = await _playerAppService.UpdatePlayerTransfer(playerTransferToUpdate);
            return DomainResult<PlayerTransferResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
        #endregion

        #region PlayerContract
        /// <summary>
        /// create PlayerContract
        /// </summary>
        /// <param name="playerContractBody"></param>
        /// <returns></returns>
        [HttpPost("PlayerContract")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PostPlayerContract([FromBody] CreatePlayerContractDTO playerContractBody)
        {
            PlayerContractResponseDTO? response = await _playerAppService.CreatePlayerContract(playerContractBody);
            return DomainResult<PlayerContractResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete PlayerContract
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("PlayerContract")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> DeletePlayerContract([FromQuery] long id)
        {
            PlayerContractResponseDTO? response = await _playerAppService.DeletePlayerContract(id);
            return DomainResult<PlayerContractResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all PlayerContract
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("PlayerContract")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário")]
        public async Task<IActionResult> GetPlayerContract([FromQuery] long playerId)
        {
            List<PlayerContractResponseDTO>? response = await _playerAppService.GetPlayerContract(playerId);
            return DomainResult<List<PlayerContractResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update PlayerContract
        /// </summary>
        /// <param name="playerContractToUpdate"></param>
        /// <returns></returns>
        [HttpPut("PlayerContract")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PutPlayerContract([FromBody] UpdatePlayerContractDTO playerContractToUpdate)
        {
            PlayerContractResponseDTO? response = await _playerAppService.UpdatePlayerContract(playerContractToUpdate);
            return DomainResult<PlayerContractResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
        #endregion

        #region PlayerPerformanceHistory
        /// <summary>
        /// create PlayerPerformanceHistory
        /// </summary>
        /// <param name="playerPerformanceHistory"></param>
        /// <returns></returns>
        [HttpPost("PlayerPerformanceHistory")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PostPlayerPerformanceHistory([FromBody] CreatePlayerPerformanceHistoryDTO playerPerformanceHistory)
        {
            PlayerPerformanceHistorySimpleResponseDTO? response = await _playerAppService.CreatePlayerPerformanceHistory(playerPerformanceHistory);
            return DomainResult<PlayerPerformanceHistorySimpleResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete PlayerPerformanceHistory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("PlayerPerformanceHistory")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> DeletePlayerPerformanceHistory([FromQuery] long id)
        {
            PlayerPerformanceHistorySimpleResponseDTO? response = await _playerAppService.DeletePlayerPerformanceHistory(id);
            return DomainResult<PlayerPerformanceHistorySimpleResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get PlayerPerformanceHistory
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        [HttpGet("PlayerPerformanceHistory")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário")]
        public async Task<IActionResult> GetPlayerPerformanceHistory([FromQuery] long playerId, [FromQuery] string season)
        {
            PlayerPerformanceHistoryResponseDTO? response = await _playerAppService.GetPlayerPerformanceHistory(playerId, season);
            return DomainResult<PlayerPerformanceHistoryResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update PlayerPerformanceHistory
        /// </summary>
        /// <param name="playerPerformanceHistoryToUpdate"></param>
        /// <returns></returns>
        [HttpPut("PlayerPerformanceHistory")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PutPlayerPerformanceHistory([FromBody] UpdatePlayerPerformanceHistoryDTO playerPerformanceHistoryToUpdate)
        {
            PlayerPerformanceHistorySimpleResponseDTO? response = await _playerAppService.UpdatePlayerPerformanceHistory(playerPerformanceHistoryToUpdate);
            return DomainResult<PlayerPerformanceHistorySimpleResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
        #endregion
    }
}
