using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Infrastructure.Repositories
{
    public class ClubMemberRepository : BaseRepository<ClubMember>, IClubMemberRepository
    {
        public ClubMemberRepository(DataContext context) : base(context)
        {
        }

        public async Task<ClubMember?> GetByIdAsync(long id)
        {
            return await GetEntity().Include(x => x.UserClubMember).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ClubMember?> GetByEmailAsync(string email)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<ClubMember>> SearchClubMembersAsync(string? firstName, string? lastName)
        {
            var query = GetEntity();

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(m => EF.Functions.Like(m.FirstName, $"%{firstName}%"));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(m => EF.Functions.Like(m.LastName, $"%{lastName}%"));
            }

            return await query.ToListAsync();
        }
    }
}
