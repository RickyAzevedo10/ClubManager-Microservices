using Financial.App.Services.Infrastructures;
using Financial.Domain.DTOs;
using Financial.Domain.Interfaces;
using Financial.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RevenueController : ControllerBase
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;
        private readonly IRevenueAppService _revenueAppService;

        public RevenueController(INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, IRevenueAppService revenueAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _revenueAppService = revenueAppService;
        }

        /// <summary>
        /// create Revenue
        /// </summary>
        /// <param name="revenueBody"></param>
        /// <returns></returns>
        [HttpPost("Revenue")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro")]
        public async Task<IActionResult> PostRevenue([FromBody] RevenueDTO revenueBody)
        {
            RevenueResponseDTO? response = await _revenueAppService.CreateRevenue(revenueBody);
            return DomainResult<RevenueResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update Revenue
        /// </summary>
        /// <param name="revenueToUpdate"></param>
        /// <returns></returns>
        [HttpPut("Revenue")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro")]
        public async Task<IActionResult> PutRevenue([FromBody] UpdateRevenueDTO revenueToUpdate)
        {
            RevenueResponseDTO? response = await _revenueAppService.UpdateRevenue(revenueToUpdate);
            return DomainResult<RevenueResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete Revenue
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Revenue")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro")]
        public async Task<IActionResult> DeleteRevenue([FromQuery] long id)
        {
            RevenueResponseDTO? response = await _revenueAppService.DeleteRevenue(id);
            return DomainResult<RevenueResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get Revenue
        /// </summary>
        /// <param name="revenueId"></param>
        /// <returns></returns>
        [HttpGet("RevenueId")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro,Secretário")]
        public async Task<IActionResult> GetRevenue([FromQuery] long revenueId)
        {
            RevenueResponseDTO? response = await _revenueAppService.GetRevenue(revenueId);
            return DomainResult<RevenueResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all revenues
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllRevenue")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro,Secretário")]
        public async Task<IActionResult> GetAllRevenue()
        {
            List<RevenueResponseDTO>? response = await _revenueAppService.GetAllRevenue();
            return DomainResult<List<RevenueResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
    }
}
