using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Repositories.Identity;
using Identity.Infra.Contexts;
using Identity.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public class UserPermissionsRepository : BaseRepository<UserPermissions>, IUserPermissionsRepository
    {
        public UserPermissionsRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<UserPermissions>?> GetUserPermissions(long userId)
        {
            return await GetEntity().Where(x => x.Users.Id == userId).ToListAsync();
        }

    }
}
