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
    public class MembersController : ControllerBase
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;
        private readonly IMembersAppService _membersAppService;

        public MembersController(INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, IMembersAppService membersAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _membersAppService = membersAppService;
        }

        #region Members
        /// <summary>
        /// adding member
        /// </summary>
        /// <param name="memberBody"></param>
        /// <returns></returns>
        [HttpPost("Members")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> Post([FromBody] CreateClubMemberDTO memberBody)
        {
            ClubMemberResponseDTO? response = await _membersAppService.Create(memberBody);
            return DomainResult<ClubMemberResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Members")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            ClubMemberResponseDTO? response = await _membersAppService.Delete(id);
            return DomainResult<ClubMemberResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get member with search method
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        [HttpGet("MembersSearch")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário")]
        public async Task<IActionResult> SearchClubMembers([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            List<ClubMemberResponseDTO>? response = await _membersAppService.SearchClubMembersAsync(firstName, lastName);
            return DomainResult<List<ClubMemberResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all member
        /// </summary>
        /// <returns></returns>
        [HttpGet("Members")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário")]
        public async Task<IActionResult> GetAllClubMembers()
        {
            List<ClubMemberResponseDTO>? response = await _membersAppService.GetAllClubMembers();
            return DomainResult<List<ClubMemberResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update member
        /// </summary>
        /// <param name="memberToUpdate"></param>
        /// <returns></returns>
        [HttpPut("Members")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> Put([FromBody] UpdateClubMemberDTO memberToUpdate)
        {
            ClubMemberResponseDTO? response = await _membersAppService.Update(memberToUpdate);
            return DomainResult<ClubMemberResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
        #endregion

        #region Minor Member
        /// <summary>
        /// adding minor member
        /// </summary>
        /// <param name="memberBody"></param>
        /// <returns></returns>
        [HttpPost("MinorMembers")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PostMinorMembers([FromBody] CreateMinorClubMemberDTO memberBody)
        {
            MinorClubMemberResponseDTO? response = await _membersAppService.CreateMinorClubMembers(memberBody);
            return DomainResult<MinorClubMemberResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// update minor member
        /// </summary>
        /// <param name="minorMemberToUpdate"></param>
        /// <returns></returns>
        [HttpPut("MinorMembers")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> PutMinorMembers([FromBody] UpdateMinorClubMemberDTO minorMemberToUpdate)
        {
            MinorClubMemberResponseDTO? response = await _membersAppService.UpdateMinorMembers(minorMemberToUpdate);
            return DomainResult<MinorClubMemberResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get minor member with search method
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        [HttpGet("MinorMembersSearch")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário")]
        public async Task<IActionResult> SearchMinorMembers([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            List<MinorClubMemberResponseDTO>? response = await _membersAppService.SearchMinorMembersAsync(firstName, lastName);
            return DomainResult<List<MinorClubMemberResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// get all minor member
        /// </summary>
        /// <returns></returns>
        [HttpGet("MinorMembers")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo,Secretário")]
        public async Task<IActionResult> GetAllMinorClubMembers()
        {
            List<MinorClubMemberResponseDTO>? response = await _membersAppService.GetAllMinorClubMembers();
            return DomainResult<List<MinorClubMemberResponseDTO>?>.Ok(response, _notificationContext, _modelErrorsContext);
        }

        /// <summary>
        /// delete minor member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("MinorMembers")]
        [Authorize(Roles = "Admin,Presidente,Diretor Desportivo")]
        public async Task<IActionResult> DeleteMinorClubMembers([FromQuery] long id)
        {
            MinorClubMemberResponseDTO? response = await _membersAppService.DeleteMinorClubMember(id);
            return DomainResult<MinorClubMemberResponseDTO?>.Ok(response, _notificationContext, _modelErrorsContext);
        }
        #endregion
    }
}
