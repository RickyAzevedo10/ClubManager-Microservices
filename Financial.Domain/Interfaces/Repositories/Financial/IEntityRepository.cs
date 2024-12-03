using Financial.Domain.Entities;

namespace Financial.Domain.Interfaces.Repositories.Identity
{
    public interface IEntityRepository : IBaseRepository<Entity>
    {
        Task<Entity?> GetExpenseWithEntity(long expenseId);
        Task<List<Entity>?> GetAllExpenseWithEntity();
        Task<List<Entity>?> GetAllRevenueWithEntity();
        Task<Entity?> GetRevenueWithEntity(long revenueId);
        Task<Entity?> GetEntityByID(long Id);
    }
}