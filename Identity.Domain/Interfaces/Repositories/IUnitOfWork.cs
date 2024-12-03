using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Repositories.Identity;

namespace Identity.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        //Identity
        IUserRepository UserRepository { get; }
        IInstitutionRepository InstitutionRepository { get; }
        IBaseRepository<UserRoles> UserRolesRepository { get; }
        IUserPermissionsRepository UserPermissionsRepository { get; }
    }
}