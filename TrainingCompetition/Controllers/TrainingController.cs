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
    public class TrainingController : ControllerBase
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;
        private readonly ITrainingAppService _trainingAppService;

        public TrainingController(INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, ITrainingAppService trainingAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _trainingAppService = trainingAppService;
        }

        #region TrainingSession
        /// <summary>
        /// create TrainingSession
        /// </summary>
        /// <param name="trainingSessionBody"></param>
        /// <returns></returns>
        [HttpPost("TrainingSession")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> PostTrainingSession([FromBody] CreateTrainingSessionDTO trainingSessionBody)
        {
            TrainingSessionResponseDTO? response = await _trainingAppService.CreateTrainingSession(trainingSessionBody);
            return DomainResult<TrainingSessionResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update TrainingSession
        /// </summary>
        /// <param name="trainingSessionToUpdate"></param>
        /// <returns></returns>
        [HttpPut("TrainingSession")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> PutTrainingSession([FromBody] UpdateTrainingSessionDTO trainingSessionToUpdate)
        {
            TrainingSessionResponseDTO? response = await _trainingAppService.UpdateTrainingSession(trainingSessionToUpdate);
            return DomainResult<TrainingSessionResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete TrainingSession
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("TrainingSession")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> DeleteTrainingSession([FromQuery] long id)
        {
            TrainingSessionResponseDTO? response = await _trainingAppService.DeleteTrainingSession(id);
            return DomainResult<TrainingSessionResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get TrainingSession
        /// </summary>
        /// <param name="trainingSessionId"></param>
        /// <returns></returns>
        [HttpGet("TrainingSessionId")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> GetTrainingSession([FromQuery] long trainingSessionId)
        {
            TrainingSessionResponseDTO? response = await _trainingAppService.GetTrainingSession(trainingSessionId);
            return DomainResult<TrainingSessionResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get TrainingSession from a date interval
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("TrainingSession")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> GetTrainingSessionsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            List<TrainingSessionResponseDTO>? response = await _trainingAppService.GetTrainingSessionsByDateRange(startDate, endDate);
            return DomainResult<List<TrainingSessionResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
        #endregion

        #region TrainingAttendance
        /// <summary>
        /// create TrainingAttendance
        /// </summary>
        /// <param name="trainingAttendanceBody"></param>
        /// <returns></returns>
        [HttpPost("TrainingAttendance")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> PostTrainingAttendance([FromBody] CreateTrainingAttendanceDTO trainingAttendanceBody)
        {
            TrainingAttendanceResponseDTO? response = await _trainingAppService.CreateTrainingAttendance(trainingAttendanceBody);
            return DomainResult<TrainingAttendanceResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update TrainingAttendance
        /// </summary>
        /// <param name="trainingAttendanceToUpdate"></param>
        /// <returns></returns>
        [HttpPut("TrainingAttendance")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> PutTrainingAttendance([FromBody] UpdateTrainingAttendanceDTO trainingAttendanceToUpdate)
        {
            TrainingAttendanceResponseDTO? response = await _trainingAppService.UpdateTrainingAttendance(trainingAttendanceToUpdate);
            return DomainResult<TrainingAttendanceResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete TrainingAttendance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("TrainingAttendance")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> DeleteTrainingAttendance([FromQuery] long id)
        {
            TrainingAttendanceResponseDTO? response = await _trainingAppService.DeleteTrainingAttendance(id);
            return DomainResult<TrainingAttendanceResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all player attendance
        /// </summary>
        /// <param name="trainingAttendanceId"></param>
        /// <returns></returns>
        [HttpGet("TrainingAttendance")]
        [Authorize(Roles = "Admin,Presidente,Treinador")]
        public async Task<IActionResult> GetTrainingAttendance([FromQuery] long trainingAttendanceId)
        {
            List<TrainingAttendanceResponseDTO>? response = await _trainingAppService.GetTrainingAttendance(trainingAttendanceId);
            return DomainResult<List<TrainingAttendanceResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
        #endregion

    }
}
