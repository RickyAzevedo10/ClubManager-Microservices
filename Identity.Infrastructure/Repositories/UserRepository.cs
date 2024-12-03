using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Repositories.Identity;
using Identity.Infra.Contexts;
using Identity.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<User?> GetByIdAsync(long userId)
        {
            return await GetEntity()
                .Include(x => x.UserRole)
                .Include(x => x.UserPermission)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await GetEntity().Include(x => x.UserRole).Include(x => x.UserPermission).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>?> GetAllUsersFromInstitution(long idInstitution)
        {
            return await GetEntity().Where(x => x.InstitutionId == idInstitution).ToListAsync();
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await GetEntity()
                .Include(x => x.UserRole)
                .Include(x => x.UserPermission)
                .FirstOrDefaultAsync(user => user.RefreshToken == refreshToken && user.RefreshTokenExpire > DateTime.UtcNow);
        }

        public async Task<User?> GetByPasswordResetTokenAsync(string passwordResetToken)
        {
            return await GetEntity()
                .FirstOrDefaultAsync(user => user.PasswordResetToken == passwordResetToken);
        }

    }
}
