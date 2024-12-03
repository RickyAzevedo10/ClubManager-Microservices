using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Repositories.Identity;

namespace Financial.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        //Financial
        IBaseRepository<Revenue> RevenueRepository { get; }
        IBaseRepository<RevenueCategory> RevenueCategoryRepository { get; }
        IBaseRepository<Expense> ExpenseRepository { get; }
        IBaseRepository<ExpenseCategory> ExpenseCategoryRepository { get; }
        IEntityRepository EntityRepository { get; }
        IBaseRepository<User> UserRepository { get; }
        IBaseRepository<Player> PlayerRepository { get; }
        IBaseRepository<ClubMember> ClubMemberRepository { get; }

    }
}