using Infrastructures.Domain.Entities;
using Infrastructures.Domain.Interfaces.Repositories.Identity;
using Infrastructures.Infra.Contexts;
using Infrastructures.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Infrastructure.Repositories
{
    public class MaintenanceHistoryRepository : BaseRepository<MaintenanceHistory>, IMaintenanceHistoryRepository
    {
        public MaintenanceHistoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<MaintenanceHistory>> GetMaintenanceHistoryByDateRange(DateTime startDate, DateTime endDate)
        {
            return await GetEntity().Where(mh => mh.MaintenanceDate >= startDate && mh.MaintenanceDate <= endDate).ToListAsync();
        }
    }
}
