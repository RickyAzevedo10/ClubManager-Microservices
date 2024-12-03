using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces.Repositories.Identity
{
    public interface IUserPermissionsRepository : IBaseRepository<UserPermissions>
    {
        Task<List<UserPermissions>?> GetUserPermissions(long userId);
    }
}