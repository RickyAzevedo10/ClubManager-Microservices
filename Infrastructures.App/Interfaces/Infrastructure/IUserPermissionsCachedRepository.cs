
using Infrastructures.Domain.DTOs;

namespace Infrastructures.Application.Interfaces.Infrastructure
{
    public interface IUserPermissionsCachedRepository
    {
        Task<UserPermissionsCacheInformationDTO?> GetUserPermissionsByUserIdAsync(CancellationToken cancellationToken = default);
    }
}
