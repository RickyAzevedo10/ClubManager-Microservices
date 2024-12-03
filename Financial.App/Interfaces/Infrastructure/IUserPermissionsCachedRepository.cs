using Financial.Domain.DTOs;

namespace Financial.Application.Interfaces.Infrastructure
{
    public interface IUserPermissionsCachedRepository
    {
        Task<UserPermissionsCacheInformationDTO?> GetUserPermissionsByUserIdAsync(CancellationToken cancellationToken = default);
    }
}
