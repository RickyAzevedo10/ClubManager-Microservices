using Identity.Domain.DTOs;

namespace Identity.Domain.Interfaces.Persistence.CachedRepositories
{
    public interface IUserPermissionsCachedRepository
    {
        Task<UserPermissionsCacheInformationDTO?> GetUserPermissionsByUserIdAsync(long userId, CancellationToken cancellationToken = default);
        Task RemoveAsync(long userId, CancellationToken cancellationToken = default);
        Task SetAsync(long userId, UserPermissionsCacheInformationDTO userPermissions, CancellationToken cancellationToken = default);
    }
}
