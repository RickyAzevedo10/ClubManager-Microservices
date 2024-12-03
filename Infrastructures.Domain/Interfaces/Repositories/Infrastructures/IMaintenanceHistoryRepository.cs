using Infrastructures.Domain.Entities;

namespace Infrastructures.Domain.Interfaces.Repositories.Identity
{
    public interface IMaintenanceHistoryRepository : IBaseRepository<MaintenanceHistory>
    {
        Task<List<MaintenanceHistory>> GetMaintenanceHistoryByDateRange(DateTime startDate, DateTime endDate);
    }
}