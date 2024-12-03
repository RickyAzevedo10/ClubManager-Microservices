using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Repositories.Identity;
using Identity.Infra.Contexts;
using Identity.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public class InstitutionRepository : BaseRepository<Institution>, IInstitutionRepository
    {
        public InstitutionRepository(DataContext context) : base(context)
        {
        }

        public async Task<Institution?> GetByIdAsync(long userId)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<Institution?> GetByNameAsync(string name)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Name == name);
        }
    }
}
