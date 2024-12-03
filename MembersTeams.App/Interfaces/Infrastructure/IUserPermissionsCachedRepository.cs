
using MembersTeams.Domain.DTOs;

namespace MembersTeams.Application.Interfaces.Infrastructure
{
    public interface IUserPermissionsCachedRepository
    {
        Task<UserPermissionsCacheInformationDTO?> GetUserPermissionsByUserIdAsync(CancellationToken cancellationToken = default);
    }
}
