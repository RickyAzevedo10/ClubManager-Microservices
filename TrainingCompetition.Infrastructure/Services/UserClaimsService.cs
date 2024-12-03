using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrainingCompetition.Application.Interfaces.Infrastructure;

namespace TrainingCompetition.Infra.Services
{
    public class UserClaimsService : IUserClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserEmail()
        {
            var emailClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email);
            return emailClaim?.Value;
        }

        public long? GetUserId()
        {
            var emailClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            return long.Parse(emailClaim?.Value);
        }
    }
}
