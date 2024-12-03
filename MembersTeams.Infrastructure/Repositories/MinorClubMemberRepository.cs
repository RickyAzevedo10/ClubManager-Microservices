using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories.Identity;
using MembersTeams.Infra.Contexts;
using MembersTeams.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MembersTeams.Infrastructure.Repositories
{
    public class MinorClubMemberRepository : BaseRepository<MinorClubMember>, IMinorClubMemberRepository
    {
        public MinorClubMemberRepository(DataContext context) : base(context)
        {
        }

        public async Task<MinorClubMember?> GetByIdAsync(long userId)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<MinorClubMember?> GetByEmailAsync(string email)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<MinorClubMember>> SearchMinorClubMemberAsync(string? firstName, string? lastName)
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
