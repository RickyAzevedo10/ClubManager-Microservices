using Contracts;
using Infrastructures.Domain.Entities;

namespace Infrastructures.Domain.Interfaces.Financial
{
    public interface IUserService
    {
        Task<User?> Get(long id);
        Task<User?> Create(CreateUpdateUser createUser);
        Task<User?> Update(CreateUpdateUser userToUpdate, User user);
        Task<User?> Delete(User user);
    }
}