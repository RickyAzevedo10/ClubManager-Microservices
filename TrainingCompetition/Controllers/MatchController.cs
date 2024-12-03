using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingCompetition.Application.Interfaces;
using TrainingCompetition.Domain.DTOs;
using TrainingCompetition.Domain.Interfaces;
using TrainingCompetition.Extensions;

namespace TrainingCompetition.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;
        private readonly IMatchAppService _matchAppService;

        public MatchController(INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, IMatchAppService matchAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _matchAppService = matchAppService;
        }

        #region Match

        /// <summary>
        /// Create Match
        /// </summary>
        /// <param name="matchBody"></param>
        /// <returns></returns>
        [HttpPost("Match")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> PostMatch([FromBody] CreateMatchDTO matchBody)
        {
            MatchResponseDTO? response = await _matchAppService.CreateMatch(matchBody);
            return DomainResult<MatchResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// Update match
        /// </summary>
        /// <param name="teamToUpdate"></param>
        /// <returns></returns>
        [HttpPut("Match")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> PutMatch([FromBody] UpdateMatchDTO matchToUpdate)
        {
            MatchResponseDTO? response = await _matchAppService.UpdateMatch(matchToUpdate);
            return DomainResult<MatchResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// Delete match
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Match")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> DeleteMatch([FromQuery] long id)
        {
            MatchResponseDTO? response = await _matchAppService.DeleteMatch(id);
            return DomainResult<MatchResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// Get Match
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [HttpGet("MatchId")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> GetMatch([FromQuery] long matchId)
        {
            MatchResponseDTO? response = await _matchAppService.GetMatch(matchId);
            return DomainResult<MatchResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// Get Team matches
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet("Match")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> GetTeamMatches([FromQuery] long teamId)
        {
            List<MatchResponseDTO>? response = await _matchAppService.GetTeamMatches(teamId);
            return DomainResult<List<MatchResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        #endregion

        #region MatchStatistics
        /// <summary>
        /// create MatchStatistics
        /// </summary>
        /// <param name="matchStatisticBody"></param>
        /// <returns></returns>
        [HttpPost("MatchStatistics")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> PostMatchStatistics([FromBody] CreateMatchStatisticDTO matchStatisticBody)
        {
            MatchStatisticResponseDTO? response = await _matchAppService.CreateMatchStatistic(matchStatisticBody);
            return DomainResult<MatchStatisticResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete MatchStatistics
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("MatchStatistics")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> DeleteMatchStatistic([FromQuery] long id)
        {
            MatchStatisticResponseDTO? response = await _matchAppService.DeleteMatchStatistic(id);
            return DomainResult<MatchStatisticResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update MatchStatistics
        /// </summary>
        /// <param name="matchStatisticToUpdate"></param>
        /// <returns></returns>
        [HttpPut("MatchStatistics")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> PutMatchStatistic([FromBody] UpdateMatchStatisticDTO matchStatisticToUpdate)
        {
            MatchStatisticResponseDTO? response = await _matchAppService.UpdateMatchStatistic(matchStatisticToUpdate);
            return DomainResult<MatchStatisticResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get match statistic
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [HttpGet("MatchStatistics/MatchId")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> GetMatchStatisticsFromMatchID([FromQuery] long matchId)
        {
            List<MatchStatisticResponseDTO>? response = await _matchAppService.GetMatchStatisticsFromMatchID(matchId);
            return DomainResult<List<MatchStatisticResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get player statistic
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("MatchStatistics/PlayerId")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> GetPlayerMatchStatistics([FromQuery] long playerId)
        {
            List<MatchStatisticResponseDTO>? response = await _matchAppService.GetPlayerMatchStatistics(playerId);
            return DomainResult<List<MatchStatisticResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get player statistic from a game
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [HttpGet("MatchStatistics")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> GetPlayerMatchStatisticsFromMatchId([FromQuery] long playerId, [FromQuery] long matchId)
        {
            List<MatchStatisticResponseDTO>? response = await _matchAppService.GetPlayerMatchStatisticsFromMatchId(playerId, matchId);
            return DomainResult<List<MatchStatisticResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
        #endregion
    }
}
