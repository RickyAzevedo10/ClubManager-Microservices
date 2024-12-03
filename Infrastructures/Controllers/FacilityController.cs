using Infrastructures.Application.Interfaces;
using Infrastructures.Domain.DTOs;
using Infrastructures.Domain.Interfaces;
using Infrastructures.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructures.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacilityController : ControllerBase
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;
        private readonly IFacilityAppService _facilityAppService;

        public FacilityController(INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, IFacilityAppService facilityAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _facilityAppService = facilityAppService;
        }

        #region Facility
        /// <summary>
        /// create Facility
        /// </summary>
        /// <param name="facilityBody"></param>
        /// <returns></returns>
        [HttpPost("Facility")]
        [Authorize(Roles = "Admin,Presidente,Gestor de Infraestruturas")]
        public async Task<IActionResult> PostFacility([FromBody] CreateFacilityDTO facilityBody)
        {
            FacilityResponseDTO? response = await _facilityAppService.CreateFacility(facilityBody);
            return DomainResult<FacilityResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get Facility
        /// </summary>
        /// <param name="facilityId"></param>
        /// <returns></returns>
        [HttpGet("FacilityId")]
        [Authorize(Roles = "Admin,Presidente,Gestor de Infraestruturas,Secretário")]
        public async Task<IActionResult> GetFacility([FromQuery] long facilityId)
        {
            FacilityResponseDTO? response = await _facilityAppService.GetFacility(facilityId);
            return DomainResult<FacilityResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all Facility
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllFacility")]
        [Authorize(Roles = "Admin,Presidente,Gestor de Infraestruturas,Secretário")]
        public async Task<IActionResult> GetAllFacility()
        {
            List<FacilityResponseDTO>? response = await _facilityAppService.GetAllFacility();
            return DomainResult<List<FacilityResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete Facility
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Facility")]
        [Authorize(Roles = "Admin,Presidente,Gestor de Infraestruturas")]
        public async Task<IActionResult> DeleteFacility([FromQuery] long id)
        {
            FacilityResponseDTO? response = await _facilityAppService.DeleteFacility(id);
            return DomainResult<FacilityResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update Facility
        /// </summary>
        /// <param name="facilityToUpdate"></param>
        /// <returns></returns>
        [HttpPut("Facility")]
        [Authorize(Roles = "Admin,Presidente,Gestor de Infraestruturas")]
        public async Task<IActionResult> PutFacility([FromBody] UpdateFacilityDTO facilityToUpdate)
        {
            FacilityResponseDTO? response = await _facilityAppService.UpdateFacility(facilityToUpdate);
            return DomainResult<FacilityResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        #endregion

        #region FacilityReservation
        /// <summary>
        /// create FacilityReservation
        /// </summary>
        /// <param name="facilityReservationBody"></param>
        /// <returns></returns>
        [HttpPost("FacilityReservation")]
        [Authorize(Roles = "Admin,Presidente,Gestor de Infraestruturas")]
        public async Task<IActionResult> PostFacilityReservation([FromBody] CreateFacilityReservationDTO facilityReservationBody)
        {
            FacilityReservationResponseDTO? response = await _facilityAppService.CreateFacilityReservation(facilityReservationBody);
            return DomainResult<FacilityReservationResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get FacilityReservation
        /// </summary>
        /// <param name="facilityReservationId"></param>
        /// <returns></returns>
        [HttpGet("FacilityReservation")]
        [Authorize(Roles = "Admin,Presidente,Gestor de Infraestruturas,Secretário")]
        public async Task<IActionResult> GetFacilityReservation([FromQuery] long facilityReservationId)
        {
            FacilityReservationResponseDTO? response = await _facilityAppService.GetFacilityReservation(facilityReservationId);
            return DomainResult<FacilityReservationResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete FacilityReservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("FacilityReservation")]
        [Authorize(Roles = "Admin,Presidente,Gestor de Infraestruturas")]
        public async Task<IActionResult> DeleteFacilityReservation([FromQuery] long id)
        {
            FacilityReservationResponseDTO? response = await _facilityAppService.DeleteFacilityReservation(id);
            return DomainResult<FacilityReservationResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update FacilityReservation
        /// </summary>
        /// <param name="facilityReservationToUpdate"></param>
        /// <returns></returns>
        [HttpPut("FacilityReservation")]
        [Authorize(Roles = "Admin,Presidente,Gestor de Infraestruturas")]
        public async Task<IActionResult> PutFacilityReservation([FromBody] UpdateFacilityReservationDTO facilityReservationToUpdate)
        {
            FacilityReservationResponseDTO? response = await _facilityAppService.UpdateFacilityReservation(facilityReservationToUpdate);
            return DomainResult<FacilityReservationResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
        #endregion

    }
}
