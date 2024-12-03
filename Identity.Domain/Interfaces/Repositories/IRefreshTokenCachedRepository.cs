using Identity.Domain.DTOs;

namespace Identity.Domain.Interfaces.Persistence.CachedRepositories
{
    public interface IRefreshTokenCachedRepository
    {
        Task<UserCacheInformationDTO?> GetUserClaimsByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
        Task RemoveAsync(string refreshToken, CancellationToken cancellationToken = default);
        Task SetAsync(string refreshToken, UserCacheInformationDTO userClaims, int expirationHours, CancellationToken cancellationToken = default);
    }
}
