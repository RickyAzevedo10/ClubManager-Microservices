using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Repositories;
using Financial.Domain.Interfaces.Repositories.Identity;
using Financial.Infra.Contexts;
using Financial.Infrastructure.Repositories;

namespace Financial.Infra.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        //Financial
        public IBaseRepository<Revenue> _revenueRepository { get; private set; }
        public IBaseRepository<RevenueCategory> _revenueCategoryRepository { get; private set; }
        public IBaseRepository<Expense> _expenseRepository { get; private set; }
        public IBaseRepository<ExpenseCategory> _expenseCategoryRepository { get; private set; }
        public IEntityRepository _entityRepository { get; private set; }
        public IBaseRepository<User> _userRepository { get; private set; }
        public IBaseRepository<Player> _playerRepository { get; private set; }
        public IBaseRepository<ClubMember> _clubMemberRepository { get; private set; }



        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        #region Financial
        public IBaseRepository<Revenue> RevenueRepository
        {
            get
            {
                if (_revenueRepository == null)
                {
                    _revenueRepository = new BaseRepository<Revenue>(_context);
                }
                return _revenueRepository;
            }
        }

        public IBaseRepository<RevenueCategory> RevenueCategoryRepository
        {
            get
            {
                if (_revenueCategoryRepository == null)
                {
                    _revenueCategoryRepository = new BaseRepository<RevenueCategory>(_context);
                }
                return _revenueCategoryRepository;
            }
        }

        public IBaseRepository<Expense> ExpenseRepository
        {
            get
            {
                if (_expenseRepository == null)
                {
                    _expenseRepository = new BaseRepository<Expense>(_context);
                }
                return _expenseRepository;
            }
        }

        public IBaseRepository<ExpenseCategory> ExpenseCategoryRepository
        {
            get
            {
                if (_expenseCategoryRepository == null)
                {
                    _expenseCategoryRepository = new BaseRepository<ExpenseCategory>(_context);
                }
                return _expenseCategoryRepository;
            }
        }

        public IEntityRepository EntityRepository
        {
            get
            {
                if (_entityRepository == null)
                {
                    _entityRepository = new EntityRepository(_context);
                }
                return _entityRepository;
            }
        }

        public IBaseRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new BaseRepository<User>(_context);
                }
                return _userRepository;
            }
        }

        public IBaseRepository<Player> PlayerRepository
        {
            get
            {
                if (_playerRepository == null)
                {
                    _playerRepository = new BaseRepository<Player>(_context);
                }
                return _playerRepository;
            }
        }

        public IBaseRepository<ClubMember> ClubMemberRepository
        {
            get
            {
                if (_clubMemberRepository == null)
                {
                    _clubMemberRepository = new BaseRepository<ClubMember>(_context);
                }
                return _clubMemberRepository;
            }
        }
        #endregion
    }
}
