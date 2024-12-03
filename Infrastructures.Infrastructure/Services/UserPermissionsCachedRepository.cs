using Infrastructures.Application.Interfaces.Infrastructure;
using Infrastructures.Application.Services.Infrastructure;
using Infrastructures.Domain.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructures.Infrastructure.Persistence.CachedRepositories
{
    public class UserPermissionsCachedRepository : IUserPermissionsCachedRepository
    {   
        private readonly IDistributedCache _distributedCache;
        private readonly IUserClaimsService _userClaimsService;

        public UserPermissionsCachedRepository(IDistributedCache distributedCache, IUserClaimsService userClaimsService)
        {
            _distributedCache = distributedCache;
            _userClaimsService = userClaimsService;
        }

        public async Task<UserPermissionsCacheInformationDTO?> GetUserPermissionsByUserIdAsync(CancellationToken cancellationToken = default)
        {
            long? userId = _userClaimsService.GetUserId();

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
    }
}
