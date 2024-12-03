using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Repositories.Identity;
using Financial.Infra.Contexts;
using Financial.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infrastructure.Repositories
{
    public class EntityRepository : BaseRepository<Entity>, IEntityRepository
    {
        public EntityRepository(DataContext context) : base(context)
        {
        }

        public async Task<Entity?> GetEntityByID(long Id)
        {
            return await GetEntity()
                //.Include(x => x.Player) //TODO: VALIDAR ESTE CENÁRIO
                //.Include(x => x.UserClubMember)
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<Entity?> GetExpenseWithEntity(long expenseId)
        {
            return await GetEntity()
                .Include(x => x.Expenses)
                .Where(x => x.Expenses.Any(y => y.Id == expenseId))
                .FirstOrDefaultAsync();
        }

        public async Task<Entity?> GetRevenueWithEntity(long revenueId)
        {
            return await GetEntity()
                .Include(x => x.Revenues)
                .Where(x => x.Revenues.Any(y => y.Id == revenueId))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Entity>?> GetAllExpenseWithEntity()
        {
            return await GetEntity()
                .Include(x => x.Expenses)
                .ToListAsync();
        }

        public async Task<List<Entity>?> GetAllRevenueWithEntity()
        {
            return await GetEntity()
                .Include(x => x.Revenues)
                .ToListAsync();
        }
    }
}
