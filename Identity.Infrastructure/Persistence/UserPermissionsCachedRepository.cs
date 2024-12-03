using Identity.Domain.DTOs;
using Identity.Domain.Interfaces.Persistence.CachedRepositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Identity.Infrastructure.Persistence.CachedRepositories
{
    public class UserPermissionsCachedRepository : IUserPermissionsCachedRepository
    {   
        private readonly IDistributedCache _distributedCache;

        public UserPermissionsCachedRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<UserPermissionsCacheInformationDTO?> GetUserPermissionsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            string key = $"permissions-{userId}";

            string? user = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(user))
                return null;

            return JsonConvert.DeserializeObject<UserPermissionsCacheInformationDTO>(
                user, 
                new JsonSerializerSettings 
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });
        }

        public async Task SetAsync(long userId, UserPermissionsCacheInformationDTO userPermissions, CancellationToken cancellationToken = default)
        {
            var cacheOptions = new DistributedCacheEntryOptions{};

            string key = $"permissions-{userId}";

            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(userPermissions),
                cacheOptions,
                cancellationToken);
        }

        public async Task RemoveAsync(long userId, CancellationToken cancellationToken = default)
        {
            string key = $"permissions-{userId}";
            await _distributedCache.RemoveAsync(key, cancellationToken);
        } 

    }
}
