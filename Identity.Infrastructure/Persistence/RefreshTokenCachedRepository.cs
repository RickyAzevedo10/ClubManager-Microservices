using Identity.Domain.DTOs;
using Identity.Domain.Interfaces.Persistence.CachedRepositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Identity.Infrastructure.Persistence.CachedRepositories
{
    public class RefreshTokenCachedRepository : IRefreshTokenCachedRepository
    {   
        private readonly IDistributedCache _distributedCache;

        public RefreshTokenCachedRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<UserCacheInformationDTO?> GetUserClaimsByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            string key = $"refresh-{refreshToken}";

            string? user = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(user))
                return null;

            return JsonConvert.DeserializeObject<UserCacheInformationDTO>(
                user, 
                new JsonSerializerSettings 
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });
        }

        public async Task SetAsync(string refreshToken, UserCacheInformationDTO userClaims, int expirationHours, CancellationToken cancellationToken = default)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(expirationHours)
            };

            string key = $"refresh-{refreshToken}";

            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(userClaims),
                cacheOptions,
                cancellationToken);
        }

        public async Task RemoveAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            string key = $"refresh-{refreshToken}";
            await _distributedCache.RemoveAsync(key, cancellationToken);
        } 

    }
}
