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
    public class EntityController : ControllerBase
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;
        private readonly IEntityAppService _entityAppService;
        public EntityController(INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, IEntityAppService entityAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _entityAppService = entityAppService;
        }

        /// <summary>
        /// create Entity
        /// </summary>
        /// <param name="entityBody"></param>
        /// <returns></returns>
        [HttpPost("Entity")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro")]
        public async Task<IActionResult> PostExpense([FromBody] CreateEntityDTO entityBody)
        {
            EntityResponseDTO? response = await _entityAppService.CreateEntity(entityBody);
            return DomainResult<EntityResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Entity")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro")]
        public async Task<IActionResult> DeleteEntity([FromQuery] long id)
        {
            EntityResponseDTO? response = await _entityAppService.DeleteEntity(id);
            return DomainResult<EntityResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update Entity
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <returns></returns>
        [HttpPut("Entity")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro")]
        public async Task<IActionResult> PutEntity([FromBody] UpdateEntityDTO entityToUpdate)
        {
            EntityResponseDTO? response = await _entityAppService.UpdateEntity(entityToUpdate);
            return DomainResult<EntityResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get Entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet("EntityId")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro,Secretário")]
        public async Task<IActionResult> GetEntity([FromQuery] long entityId)
        {
            EntityResponseDTO? response = await _entityAppService.GetEntity(entityId);
            return DomainResult<EntityResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all Entity
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllEntity")]
        [Authorize(Roles = "Admin,Presidente,Diretor Financeiro,Secretário")]
        public async Task<IActionResult> GetAllEntity()
        {
            List<EntityResponseDTO>? response = await _entityAppService.GetAllEntity();
            return DomainResult<List<EntityResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
    }
}
