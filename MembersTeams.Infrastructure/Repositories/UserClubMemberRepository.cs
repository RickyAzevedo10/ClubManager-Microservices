using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Infrastructure.Repositories
{
    public class UserClubMemberRepository : BaseRepository<UserClubMember>, IUserClubMemberRepository
    {
        public UserClubMemberRepository(DataContext context) : base(context)
        {
        }

        public async Task<UserClubMember?> GetByUserIdAsync(long userId)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
