using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Repositories;
using Identity.Domain.Interfaces.Repositories.Identity;
using Identity.Infra.Contexts;
using Identity.Infrastructure.Repositories;

namespace Identity.Infra.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        //Identity
        public IUserRepository _userRepository { get; private set; }
        public IInstitutionRepository _institutionRepository { get; private set; }
        public IBaseRepository<UserRoles> _userRolesRepository { get; private set; }
        public IUserPermissionsRepository _userPermissionsRepository { get; private set; }


        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        #region Identity
        public IInstitutionRepository InstitutionRepository
        {
            get
            {
                if (_institutionRepository == null)
                {
                    _institutionRepository = new InstitutionRepository(_context);
                }
                return _institutionRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IBaseRepository<UserRoles> UserRolesRepository
        {
            get
            {
                if (_userRolesRepository == null)
                {
                    _userRolesRepository = new BaseRepository<UserRoles>(_context);
                }
                return _userRolesRepository;
            }
        }

        public IUserPermissionsRepository UserPermissionsRepository
        {
            get
            {
                if (_userPermissionsRepository == null)
                {
                    _userPermissionsRepository = new UserPermissionsRepository(_context);
                }
                return _userPermissionsRepository;
            }
        }
        #endregion

    }
}
