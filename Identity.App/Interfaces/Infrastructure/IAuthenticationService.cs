using Identity.Domain.DTOs;

namespace Identity.App.Interfaces.Infrastructure
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserCacheInformationDTO user);
        string GenerateRefreshToken();
    }
}
